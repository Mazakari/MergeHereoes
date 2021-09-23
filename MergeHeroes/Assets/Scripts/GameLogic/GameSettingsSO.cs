// Roman Baranov 20.07.2021

using UnityEngine;

[CreateAssetMenu(fileName ="GameSettingsSO", menuName ="Game Settings SO", order = 1)]
public class GameSettingsSO : ScriptableObject
{
    #region VARIABLES
    [Header("-----ПРЕДМЕТЫ----")]
    [Header("Коллекция префабов мечей для мержа")]
    [SerializeField] private GameObject[] _swords = null;

    /// <summary>
    /// Коллекция префабов мечей для мержа
    /// </summary>
    public GameObject[] Swords { get { return _swords; } }

    [Header("Коллекция префабов брони для мержа")]
    [SerializeField] private GameObject[] _armour = null;

    /// <summary>
    /// Коллекция префабов брони для мержа
    /// </summary>
    public GameObject[] Armour { get { return _armour; } }

    [Header("Коллекция префабов зелий для мержа")]
    [SerializeField] private GameObject[] _potions = null;

    /// <summary>
    /// Коллекция префабов зелий для мержа
    /// </summary>
    public GameObject[] Potions { get { return _potions; } }

    [Header("-----ПЕРСОНАЖИ-----")]
    [Header("Коллекция префабов героев доступных в игре")]
    [SerializeField] private GameObject[] _heroes = null;

    /// <summary>
    /// Коллекция префабов героев доступных в игре
    /// </summary>
    public GameObject[] Heroes { get { return _heroes; } }

    [Header("Коллекция префабов монстров доступных в игре")]
    [SerializeField] private GameObject[] _monsters = null;

    /// <summary>
    /// Коллекция префабов монстров доступных в игре
    /// </summary>
    public GameObject[] Monsters { get { return _monsters; } }

    /// <summary>
    /// Режимы сложности игры
    /// </summary>
    public enum GameMode
    {
        Easy,
        Normal,
        Hard,
        VeryHard
    }

    private static GameMode _curGameMode;
    /// <summary>
    /// Текущий режим сложности игры
    /// </summary>
    public static GameMode CurGameMode { get { return _curGameMode; } set { _curGameMode = value; } }

    [Header("-----КОМНАТЫ-----")]
    [Header("Коллекция спрайтов для фонов комнат доступных в игре")]
    [SerializeField] private Sprite[] _roomSprites = null;
    /// <summary>
    /// Коллекция спрайтов для фонов комнат доступных в игре
    /// </summary>
    public Sprite[] RoomSprites { get { return _roomSprites; } }

    [Header("Коллекция названий для комнат")]
    [SerializeField] private string[] _roomNames = null;
    /// <summary>
    /// Коллекция названий для комнат
    /// </summary>
    public string[] RoomNames { get { return _roomNames; } }

    [Header("Изображения для кнопок комнат")]
    [SerializeField] private Sprite[] _roomButtonSprites = null;
    /// <summary>
    /// Изображения для кнопок комнат
    /// </summary>
    public Sprite[] RoomButtonSprites { get { return _roomButtonSprites; } }

    [Header("PLACEHOLDER! Модификаторы для комнат")]
    [SerializeField] private string[] _roomModifiers = null;
    /// <summary>
    /// Модификаторы для комнат
    /// </summary>
    public string[] RoomModifiers { get { return _roomModifiers; } }
    #endregion
}
