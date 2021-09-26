// Roman Baranov 24.09.2021

using UnityEngine;
using UnityEngine.UI;

public class Room_UI : MonoBehaviour
{
    #region VARIABLES
    private static Text _roomNameText = null;
    private static Text _roomCounterText = null;
    private static Text _roomWaveCounterText = null;

    private static Slider _roomHealth = null;
    private static Text _roomHealthCounterText = null;

    #endregion

    #region UNITY Methods
    private void Awake()
    {
        _roomNameText = transform.Find("RoomNameText").GetComponent<Text>();
        _roomCounterText = transform.Find("RoomCounterText").GetComponent<Text>();
        _roomWaveCounterText = transform.Find("RoomWaveCounterText").GetComponent<Text>();

        _roomHealth = GetComponent<Slider>();
        _roomHealthCounterText = transform.Find("RoomHPBar").Find("Fill Area").Find("RoomHealthCounterText").GetComponent<Text>();
    }
    #endregion

    #region PUBLIC Methods
    /// <summary>
    /// Update all room info for a new room
    /// </summary>
    public static void UpdateRoomInfo()
    {
        _roomNameText.text = Level.CurrentRoom.RoomName;
        _roomCounterText.text = $"Room {Level.CurrentRoom.CurRoomNumber} of {Level.MaxRooms}";
        _roomWaveCounterText.text = $"Wave {Level.CurrentRoom.CurWaveNumber} of {Level.MaxMonsterWavePerRoom}";
    }

    /// <summary>
    /// Updates current monster wave number
    /// </summary>
    public static void UpdateMonsterWaveInfo()
    {
        _roomWaveCounterText.text = $"Wave {Level.CurrentRoom.CurWaveNumber} of {Level.MaxMonsterWavePerRoom}";
    }

    /// <summary>
    /// Decrease room health by given damage amount
    /// </summary>
    /// <param name="damageAmout">Incoming damage amount</param>
    public static void GetRoomHealthDamage(float damageAmout)
    {
        _roomHealth.value -= damageAmout;

        _roomHealthCounterText.text = $"{_roomHealth.value}";

        Debug.Log($"GetRoomHealthDamage: _roomHealth.value = {_roomHealth.value}");
        Debug.Log($"GetRoomHealthDamage: _roomHealthCounterText.text = {_roomHealthCounterText.text}");
        
    }

    /// <summary>
    /// Set room health pool amount and its counter to required value
    /// </summary>
    /// <param name="roomHealth">Room health value to be set</param>
    public static void SetRoomHealth(float roomHealth)
    {
        _roomHealth.minValue = 0;
        _roomHealth.maxValue = roomHealth;
        _roomHealth.value = _roomHealth.maxValue;

        _roomHealthCounterText.text = $"{_roomHealth.value}";

        Debug.Log($"SetRoomHealth: _roomHealth.value = {_roomHealth.value}");
        Debug.Log($"SetRoomHealth: _roomHealthCounterText.text = {_roomHealthCounterText.text}");
    
    }
    #endregion

}
