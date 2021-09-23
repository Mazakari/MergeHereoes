// Roman Baranov 20.07.2021

using UnityEngine;

[CreateAssetMenu(fileName ="GameSettingsSO", menuName ="Game Settings SO", order = 1)]
public class GameSettingsSO : ScriptableObject
{
    #region VARIABLES
    [Header("-----��������----")]
    [Header("��������� �������� ����� ��� �����")]
    [SerializeField] private GameObject[] _swords = null;

    /// <summary>
    /// ��������� �������� ����� ��� �����
    /// </summary>
    public GameObject[] Swords { get { return _swords; } }

    [Header("��������� �������� ����� ��� �����")]
    [SerializeField] private GameObject[] _armour = null;

    /// <summary>
    /// ��������� �������� ����� ��� �����
    /// </summary>
    public GameObject[] Armour { get { return _armour; } }

    [Header("��������� �������� ����� ��� �����")]
    [SerializeField] private GameObject[] _potions = null;

    /// <summary>
    /// ��������� �������� ����� ��� �����
    /// </summary>
    public GameObject[] Potions { get { return _potions; } }

    [Header("-----���������-----")]
    [Header("��������� �������� ������ ��������� � ����")]
    [SerializeField] private GameObject[] _heroes = null;

    /// <summary>
    /// ��������� �������� ������ ��������� � ����
    /// </summary>
    public GameObject[] Heroes { get { return _heroes; } }

    [Header("��������� �������� �������� ��������� � ����")]
    [SerializeField] private GameObject[] _monsters = null;

    /// <summary>
    /// ��������� �������� �������� ��������� � ����
    /// </summary>
    public GameObject[] Monsters { get { return _monsters; } }

    /// <summary>
    /// ������ ��������� ����
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
    /// ������� ����� ��������� ����
    /// </summary>
    public static GameMode CurGameMode { get { return _curGameMode; } set { _curGameMode = value; } }

    [Header("-----�������-----")]
    [Header("��������� �������� ��� ����� ������ ��������� � ����")]
    [SerializeField] private Sprite[] _roomSprites = null;
    /// <summary>
    /// ��������� �������� ��� ����� ������ ��������� � ����
    /// </summary>
    public Sprite[] RoomSprites { get { return _roomSprites; } }

    [Header("��������� �������� ��� ������")]
    [SerializeField] private string[] _roomNames = null;
    /// <summary>
    /// ��������� �������� ��� ������
    /// </summary>
    public string[] RoomNames { get { return _roomNames; } }

    [Header("����������� ��� ������ ������")]
    [SerializeField] private Sprite[] _roomButtonSprites = null;
    /// <summary>
    /// ����������� ��� ������ ������
    /// </summary>
    public Sprite[] RoomButtonSprites { get { return _roomButtonSprites; } }

    [Header("PLACEHOLDER! ������������ ��� ������")]
    [SerializeField] private string[] _roomModifiers = null;
    /// <summary>
    /// ������������ ��� ������
    /// </summary>
    public string[] RoomModifiers { get { return _roomModifiers; } }
    #endregion
}
