// Roman Baranov 24.09.2021

using UnityEngine;
using UnityEngine.UI;

public class Room_UI : MonoBehaviour
{
    #region VARIABLES
    private static Text _roomNameText = null;
    private static Text _roomCounterText = null;
    private static Text _roomWaveCounterText = null;

    #endregion

    #region UNITY Methods
    private void Awake()
    {
        _roomNameText = transform.Find("RoomNameText").GetComponent<Text>();
        _roomCounterText = transform.Find("RoomCounterText").GetComponent<Text>();
        _roomWaveCounterText = transform.Find("RoomWaveCounterText").GetComponent<Text>();
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
    #endregion

}
