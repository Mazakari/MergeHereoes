// Roman Baranov 14.09.2021

using UnityEngine;

public class Room
{
    #region VARIABLES
    // тип комнаты

    // награда за комнату

    private string _roomName = "";
    /// <summary>
    /// Room name
    /// </summary>
    public string RoomName { get { return _roomName; } }

    private RoomModificator.Modificator _RoomModificator;// Room modifier

    private int _curRoomNumber = 0;
    /// <summary>
    /// Current room number
    /// </summary>
    public int CurRoomNumber { get { return _curRoomNumber; } }

    private float _maxRoomHealth = 100f;
    /// <summary>
    /// Max room health pool
    /// </summary>
    public float MaxRoomHealth { get { return _maxRoomHealth; } }

    private float _curRoomHealth;
    /// <summary>
    /// Current room health pool
    /// </summary>
    public float CurRoomHealth { get { return _curRoomHealth; } set { _curRoomHealth = value; } }

    private int _curWaveNumber = 0;
    /// <summary>
    /// Current monster wave number
    /// </summary>
    public int CurWaveNumber { get { return _curWaveNumber; } set { _curWaveNumber = value; } }

    private int _curMonstersInWave = 0;
    /// <summary>
    /// Monsters amount in current wave
    /// </summary>
    public int CurMonstersInWave { get { return _curMonstersInWave; } set { _curMonstersInWave = value; } }

    private Sprite _roomBackgroundSprite = null;// Room background sprite

    
    #endregion

    #region CONSTRUCTOR
    /// <summary>
    /// Room constructor
    /// </summary>
    /// <param name="roomName">Room name</param>
    /// <param name="curRoomNumber">Current room number</param>
    /// <param name="maxRoomHealth">Room health pool for monsters</param>
    /// <param name="curMonsterWave">Current monster wave in room</param>
    /// <param name="curMonstersInWave">Current monsterd count in current wave</param>
    /// <param name="roomBackgroundSprite">Room background sprite</param>
    /// <param name="roomModificator">Room modifier</param>
    public Room(string roomName, int curRoomNumber, float maxRoomHealth, int curMonstersInWave, Sprite roomBackgroundSprite, RoomModificator.Modificator roomModificator)
    {
        _roomName = roomName;
        _curRoomNumber = curRoomNumber;

        _maxRoomHealth = maxRoomHealth;
        _curRoomHealth = _maxRoomHealth;

        _curWaveNumber = 1;
        _curMonstersInWave = curMonstersInWave;

        _roomBackgroundSprite = roomBackgroundSprite;

        _RoomModificator = roomModificator;
    }
    #endregion
}
