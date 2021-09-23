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

    private Sprite _roomBackgroundSprite = null;// Спрайт фона комнаты
    #endregion

    #region CONSTRUCTOR
    /// <summary>
    /// Конструктор комнаты
    /// </summary>
    /// <param name="roomName">Название комнаты</param>
    /// <param name="curRoomNumber">Текущий номер комнаты</param>
    /// <param name="roomHealth">Пул здоровья для монстров в комнате</param>
    /// <param name="curMonsterWave">Текущее количество волн монстров в комнате</param>
    /// <param name="curMonstersInWave">Текущее количество монстров в текущей волне</param>
    /// <param name="roomBackgroundSprite">Спрайт фона комнаты</param>
    /// <param name="roomModificator">Модификатор комнаты</param>
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
