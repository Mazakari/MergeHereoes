// Roman Baranov 24.09.2021

using UnityEngine;
using UnityEngine.UI;

public class Room_UI : MonoBehaviour
{
    #region VARIABLES
    private static Text _roomNameText = null;
    private static Text _roomCounterText = null;
    private static Text _roomWaveCounterText = null;

    private static Slider _roomWaveHealth = null;
    private static Text _roomHealthCounterText = null;

    #endregion

    #region UNITY Methods
    private void Awake()
    {
        _roomNameText = transform.Find("RoomNameText").GetComponent<Text>();
        _roomCounterText = transform.Find("RoomCounterText").GetComponent<Text>();
        _roomWaveCounterText = transform.Find("RoomWaveCounterText").GetComponent<Text>();

        _roomWaveHealth = GetComponent<Slider>();
        _roomHealthCounterText = transform.Find("RoomWaveHPBar").Find("Fill Area").Find("RoomWaveHealthCounterText").GetComponent<Text>();
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

        // Check if boss room loaded right now
        if (Level.BossRoomActive)
        {
            // Shows one of one waved for the boss room
            _roomWaveCounterText.text = $"Wave {Level.MaxBossWavePerRoom} of {Level.MaxBossWavePerRoom}";
            Debug.Log($"UpdateRoomInfo: Wave {Level.CurrentRoom.CurWaveNumber} of {Level.MaxMonsterWavePerRoom}");
        }
        else
        {
            _roomWaveCounterText.text = $"Wave {Level.CurrentRoom.CurWaveNumber} of {Level.MaxMonsterWavePerRoom}";
        }
        
    }

    /// <summary>
    /// Updates current monster wave number in UI
    /// </summary>
    public static void UpdateMonsterWaveInfo()
    {
        if (Level.BossRoomActive)
        {
            _roomWaveCounterText.text = $"Wave {Level.MaxBossWavePerRoom} of {Level.MaxBossWavePerRoom}";
            Debug.Log($"UpdateMonsterWaveInfo: Wave {Level.CurrentRoom.CurWaveNumber} of {Level.MaxMonsterWavePerRoom}");
        }
        else
        {
            _roomWaveCounterText.text = $"Wave {Level.CurrentRoom.CurWaveNumber} of {Level.MaxMonsterWavePerRoom}";
        }
       
    }

    /// <summary>
    /// Update room wave health in UI
    /// </summary>
    public static void UpdateRoomWaveHealthInfo()
    {
        _roomWaveHealth.value = Level.CurrentRoom.CurRoomWaveHealth;

        _roomHealthCounterText.text = $"{_roomWaveHealth.value}";
    }

    /// <summary>
    /// Set room wave health pool amount and its counter to required value
    /// </summary>
    /// <param name="roomWaveHealth">Room wave health value to be set</param>
    public static void SetRoomWaveHealth(float roomWaveHealth)
    {
        _roomWaveHealth.minValue = 0;
        _roomWaveHealth.maxValue = roomWaveHealth;
        _roomWaveHealth.value = _roomWaveHealth.maxValue;

        _roomHealthCounterText.text = $"{_roomWaveHealth.value}";
    }
    #endregion

}
