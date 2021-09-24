// Roman Baranov 14.09.2021

using UnityEngine;

public class Room
{
    #region VARIABLES
    // тип комнаты

    // награда за комнату

    private string _roomName = "";// Название комнаты
    /// <summary>
    /// Название комнаты
    /// </summary>
    public string RoomName { get { return _roomName; } }

    private RoomModificator.Modificator _RoomModificator;// Модификатор комнаты

    private int _curRoomNumber = 0;
    /// <summary>
    /// Текущий номер комнаты
    /// </summary>
    public int CurRoomNumber { get { return _curRoomNumber; } }

    private float _roomHealth = 100f;
    /// <summary>
    /// Общий нул здоровья для монстров в комнате
    /// </summary>
    public float RoomHealth { get { return _roomHealth; } }

    private int _curWaveNumber = 0;
    /// <summary>
    /// Номер текущей волны монстров
    /// </summary>
    public int CurWaveNumber { get { return _curWaveNumber; } set { _curWaveNumber = value; } }

    private int _curMonstersInWave = 0;
    /// <summary>
    /// Текущее количество монстров в текущей волне
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
    /// <param name="roomHealth">Room health pool for monsters</param>
    /// <param name="curMonsterWave">Current monster wave in room</param>
    /// <param name="curMonstersInWave">Current monsterd count in current wave</param>
    /// <param name="roomBackgroundSprite">Room background sprite</param>
    /// <param name="roomModificator">Room modifier</param>
    public Room(string roomName, int curRoomNumber, float roomHealth, int curMonstersInWave, Sprite roomBackgroundSprite, RoomModificator.Modificator roomModificator)
    {
        _roomName = roomName;
        _curRoomNumber = curRoomNumber;

        _roomHealth = roomHealth;

        _curWaveNumber = 1;
        _curMonstersInWave = curMonstersInWave;

        _roomBackgroundSprite = roomBackgroundSprite;

        _RoomModificator = roomModificator;
    }
    #endregion
}
