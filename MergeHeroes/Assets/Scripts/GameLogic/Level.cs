// Roman Baranov 08.09.2021

using System;
using UnityEngine;
public class Level : MonoBehaviour
{
    #region VARIABLES
    private static int _maxRooms = 0;
    /// <summary>
    /// Max rooms on level
    /// </summary>
    public static int MaxRooms { get { return _maxRooms; } }

    private static int _maxMosterWavePerRoom = 0;
    /// <summary>
    /// Max monster waves per room
    /// </summary>
    public static int MaxMonsterWavePerRoom { get { return _maxMosterWavePerRoom; } }

    private static int _maxMonstersPerWave = 0;
    /// <summary>
    /// Max monsters in single wave
    /// </summary>
    public static int MaxMonstersPerWave { get { return _maxMonstersPerWave; } }

    private static float _perMonsterHealth = 0;

    /// <summary>
    /// Health amount per monster in current wave
    /// </summary>
    public static float PerMonsterHealth { get { return _perMonsterHealth; } }

    private static int _bossesCount = 0;// Bosses amount on level

    private static Room[] _rooms = null;
    /// <summary>
    /// Level rooms collection
    /// </summary>
    public static Room[] Rooms { get { return _rooms; } }

    private static Room _currentRoom = null;
    /// <summary>
    /// Current room reference
    /// </summary>
    public static Room CurrentRoom { get { return _currentRoom; } }

    private static CharactersSpawner _characterSpawner = null;
    #endregion

    #region UNITY Methods

    // Start is called before the first frame update
    private void Awake()
    {
        SetGameModeSettings();
        _characterSpawner = FindObjectOfType<CharactersSpawner>();
    }

    private void Start()
    {
        LoadFirstRoom();
        
        // Update new room UI
        Room_UI.UpdateRoomInfo();
    }

    #endregion

    #region PUBLUC Methods
    /// <summary>
    /// Adds a new room on first free room index in Rooms collection
    /// </summary>
    /// <param name="roomModificator">Room modifier</param>
    public static void AddRoom(RoomModificator.Modificator roomModificator)
    {
        int freeRoomIndex = FindEmptyRoomIndex();
        if (freeRoomIndex >= 0 && freeRoomIndex < _maxRooms)
        {
            //Choose random room name
            int rnd = UnityEngine.Random.Range(0, ItemsSpawner.gameSettingsSO.RoomNames.Length - 1);

            int rndSprite = UnityEngine.Random.Range(0, ItemsSpawner.gameSettingsSO.RoomSprites.Length - 1);

            _rooms[freeRoomIndex] = new Room(ItemsSpawner.gameSettingsSO.RoomNames[rnd], _currentRoom.CurRoomNumber + 1, 100f * freeRoomIndex + 1, 3, ItemsSpawner.gameSettingsSO.RoomSprites[rndSprite], roomModificator);

            // Cache room reference
            _currentRoom = _rooms[freeRoomIndex];

            //Debug.Log($"AddRoom: _currentRoom.MaxRoomHealth {_currentRoom.MaxRoomHealth}");
            //Debug.Log($"AddRoom: _currentRoom.CurRoomHealth {_currentRoom.CurRoomHealth}");

            // Set monster health
            SetMonsterHealth();

            //Spawn first monsters wave
            _characterSpawner.SpawnMonsters();

            // Set room health pool
            Room_UI.SetRoomHealth(_currentRoom.MaxRoomHealth);
        }
    }
    #endregion

    #region PRIVATE Methods
    /// <summary>
    /// Loads firs room on level start
    /// </summary>
    private void LoadFirstRoom()
    {
        //Choose random room name
        int rnd = UnityEngine.Random.Range(0, ItemsSpawner.gameSettingsSO.RoomNames.Length);

        _rooms[0] = new Room(ItemsSpawner.gameSettingsSO.RoomNames[rnd], 1, 100f, 3, ItemsSpawner.gameSettingsSO.RoomSprites[0], RoomModificator.Modificator.None);

        // Cache room reference
        _currentRoom = _rooms[0];

        //Debug.Log($"LoadFirstRoom: _currentRoom.MaxRoomHealth {_currentRoom.MaxRoomHealth}");
        //Debug.Log($"LoadFirstRoom: _currentRoom.CurRoomHealth {_currentRoom.CurRoomHealth}");

        // Set monster health
        SetMonsterHealth();

        //Spawn first monsters wave
        _characterSpawner.SpawnMonsters();

        // Set room health pool
        Room_UI.SetRoomHealth(_currentRoom.MaxRoomHealth);
    }

    /// <summary>
    /// Return first empty room index.
    /// Return -1 no free indexes found.
    /// </summary>
    /// <returns>int</returns>
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
    /// Set level settings in accordance with selected game mode
    /// </summary>
    private void SetGameModeSettings()
    {
        switch (GameSettingsSO.CurGameMode)
        {
            case GameSettingsSO.GameMode.Easy:
                GetMode(4, 3, 3, 4 / 4);
                break;

            case GameSettingsSO.GameMode.Normal:
                GetMode(8, 3, 3, 8 / 4);
                break;

            case GameSettingsSO.GameMode.Hard:
                GetMode(12, 3, 3, 12 / 4);
                break;

            case GameSettingsSO.GameMode.VeryHard:
                GetMode(16, 3, 3, 16 / 4);
                break;

            default:
                Debug.Log("LevelSettings.LoadGameMode() - No Such Game Mode Found");
                break;
        }
    }

    /// <summary>
    /// Get game mode settings
    /// </summary>
    private void GetMode(int maxRooms, int maxMosterWavePerRoom, int maxMonstersPerWave, int bossesCount)
    {
        _maxRooms = maxRooms;
        _maxMosterWavePerRoom = maxMosterWavePerRoom;
        _maxMonstersPerWave = maxMonstersPerWave;
        _bossesCount = bossesCount;

        _rooms = new Room[_maxRooms];
    }

    /// <summary>
    /// Set monster health amount in room
    /// </summary>
    private static void SetMonsterHealth()
    {
        int totalMonstersInRoom = _maxMosterWavePerRoom * _maxMonstersPerWave;

        _perMonsterHealth = _currentRoom.MaxRoomHealth / totalMonstersInRoom;
        //Debug.Log($"SetMonsterHealth: totalMonstersInRoom = {totalMonstersInRoom}");
        //Debug.Log($"SetMonsterHealth: _perMonsterHealth = {_perMonsterHealth}");
    }
    #endregion
}
