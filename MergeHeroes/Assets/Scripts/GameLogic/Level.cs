// Roman Baranov 08.09.2021

using UnityEngine;
public class Level : MonoBehaviour
{
    #region VARIABLES
    private static int _maxRooms = 4;
    /// <summary>
    /// ������������ ���������� ������ �� ������
    /// </summary>
    public static int MaxRooms { get { return _maxRooms; } }

    private static int _maxMosterWavePerRoom = 3;
    /// <summary>
    /// ������������ ���������� ���� �������� � �������
    /// </summary>
    public static int MaxMonsterWavePerRoom { get { return _maxMosterWavePerRoom; } }

    private static int _maxMonstersPerWave = 3;
    /// <summary>
    /// ������������ ���������� �������� � �����
    /// </summary>
    public static int MaxMonstersPerWave { get { return _maxMonstersPerWave; } }

    private static int _bossesCount = 1;// ���������� ������ �� ������

    private static Room[] _rooms = null;
    /// <summary>
    /// ������� � ���� ������
    /// </summary>
    public static Room[] Rooms { get { return _rooms; } }

    private static Room _currentRoom = null;
    /// <summary>
    /// ������ �� ������� ������� ������
    /// </summary>
    public static Room CurrentRoom { get { return _currentRoom; } }
    #endregion

    #region UNITY Methods

    // Start is called before the first frame update
    private void Awake()
    {
        GetGameModeSettings();
    }

    private void Start()
    {
        LoadFirstRoom();
    }

    #endregion

    #region PUBLUC Methods
    /// <summary>
    /// ��������� ����� ������� � ������ ��������� ���� ������������ ��������� ������ �� ������
    /// </summary>
    /// <param name="roomModificator">����������� �������</param>
    public static void AddRoom(RoomModificator.Modificator roomModificator)
    {
        int freeRoomIndex = FindEmptyRoomIndex();
        if (freeRoomIndex >= 0 && freeRoomIndex < _maxRooms)
        {
            //�������� ������������ �������� ��� ����� �������
            int rnd = Random.Range(0, ItemsSpawner.gameSettingsSO.RoomNames.Length);

            _rooms[freeRoomIndex] = new Room(ItemsSpawner.gameSettingsSO.RoomNames[rnd], _currentRoom.CurRoomNumber + 1, 100f * freeRoomIndex + 1, 3, ItemsSpawner.gameSettingsSO.RoomSprites[freeRoomIndex], roomModificator);

            // ��������� ������ �� ������� �������
            _currentRoom = _rooms[freeRoomIndex];

            //// ��������� ���������� � ����� ������� � UI
            //LevelInfo_UI.UpdateRoomNameText(_rooms[freeRoomIndex].RoomName);
            //LevelInfo_UI.UpdateRoomInfoText();
        }
    }
    #endregion

    #region PRIVATE Methods
    /// <summary>
    /// Loads firs room on level start
    /// </summary>
    private void LoadFirstRoom()
    {
        _rooms = new Room[_maxRooms];

        //�������� ������������ �������� ��� ����� �������
        int rnd = Random.Range(0, ItemsSpawner.gameSettingsSO.RoomNames.Length);

        _rooms[0] = new Room(ItemsSpawner.gameSettingsSO.RoomNames[rnd], 0, 100f, 3, ItemsSpawner.gameSettingsSO.RoomSprites[0], RoomModificator.Modificator.None);

        // ��������� ������ �� ������� �������
        _currentRoom = _rooms[0];
    }

    /// <summary>
    /// ��������� ������ � ��������� � ���������� ������ ��������� ������.
    /// ���� ��� ������ ������, ��������� 0
    /// </summary>
    /// <returns></returns>
    private static int FindEmptyRoomIndex()
    {
        for (int i = 0; i < _rooms.Length; i++)
        {
            if (_rooms[i] == null)
            {
                return i;
            }
        }

        return -1;
    }

    /// <summary>
    /// ��������� ��������� ������ � ������ � ����������� �� ��������� ���������
    /// </summary>
    private void GetGameModeSettings()
    {
        switch (GameSettingsSO.CurGameMode)
        {
            case GameSettingsSO.GameMode.Easy:
                SetMode(4, 3, 3, 4 / 4);
                break;

            case GameSettingsSO.GameMode.Normal:
                SetMode(8, 3, 3, 8 / 4);
                break;

            case GameSettingsSO.GameMode.Hard:
                SetMode(12, 3, 3, 12 / 4);
                break;

            case GameSettingsSO.GameMode.VeryHard:
                SetMode(16, 3, 3, 16 / 4);
                break;

            default:
                Debug.Log("LevelSettings.LoadGameMode() - No Such Game Mode Found");
                break;
        }
    }

    /// <summary>
    /// ��������� ��������� ��� ������ ���������
    /// </summary>
    private void SetMode(int maxRooms, int maxMosterWavePerRoom, int maxMonstersPerWave, int bossesCount)
    {
        _maxRooms = maxRooms;
        _maxMosterWavePerRoom = maxMosterWavePerRoom;
        _maxMonstersPerWave = maxMonstersPerWave;
        _bossesCount = bossesCount;
    }
    #endregion
}
