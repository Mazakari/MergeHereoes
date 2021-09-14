// Roman Baranov 14.09.2021

using UnityEngine;

public class Room
{
    // тип комнаты

    // награда за комнату

    private string _roomName = "";// Название комнаты

    private int _curRoomNumber = 0;// Текущий номер комнаты

    private float _roomHealth = 100f;// Пул здоровья для монстров в комнате

    private int _curMonsterWave = 0;// Текущее количество волн монстров в комнате

    private int _curWaveNumber = 0;// Номер текущей волны монстров

    private int _curMonstersInWave = 0;// Текущее количество монстров в текущей волне

    private Sprite _roomBackgroundSprite = null;// Спрайт фона комнаты

    /// <summary>
    /// Конструктор комнаты
    /// </summary>
    /// <param name="roomName">Название комнаты</param>
    /// <param name="curRoomNumber">Текущий номер комнаты</param>
    /// <param name="roomHealth">Пул здоровья для монстров в комнате</param>
    /// <param name="curMonsterWave">Текущее количество волн монстров в комнате</param>
    /// <param name="curMonstersInWave">Текущее количество монстров в текущей волне</param>
    /// <param name="roomBackgroundSprite">Спрайт фона комнаты</param>
    public Room(string roomName, int curRoomNumber, float roomHealth, int curMonsterWave, int curMonstersInWave, Sprite roomBackgroundSprite)
    {
        _roomName = roomName;
        _curRoomNumber = curRoomNumber;

        _roomHealth = roomHealth;

        _curMonsterWave = curMonsterWave;
        _curWaveNumber = 0;
        _curMonstersInWave = curMonstersInWave;

        _roomBackgroundSprite = roomBackgroundSprite;
    }
}
