// Roman Baranov 08.09.2021

using System;
using UnityEngine;
public class Level : MonoBehaviour
{
    #region VARIABLES
    #region ROOM
    private static int _bossRoomInterval = 4;

    private static int _maxRooms = 0;
    /// <summary>
    /// Max rooms on level
    /// </summary>
    public static int MaxRooms { get { return _maxRooms; } }

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

    private static int[] _bossRooms = null;
    #endregion

    #region MONSTERS
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

    private static int _maxBossWavePerRoom = 1; 
    /// <summary>
    /// Max boss waves per room
    /// </summary>
    public static int MaxBossWavePerRoom { get { return _maxBossWavePerRoom; } }

    private static float _maxBossHealth = 0f;
    /// <summary>
    /// Max boss health
    /// </summary>
    public static float MaxBossHealth { get { return _maxBossHealth; } }

    private static float _goldReward = 0; 

    /// <summary>
    /// Current gold reward for room completion
    /// </summary>
    public static float GoldReward { get { return _goldReward; } }
    #endregion

    private static CharactersSpawner _characterSpawner = null;
    #endregion

    #region UNITY Methods
    private void Awake()
    {
        SetGameModeSettings();
        _characterSpawner = FindObjectOfType<CharactersSpawner>();
    }

    private void Start()
    {
        LoadFirstRoom();
        SetBossRooms(_bossRoomInterval);

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
            // Check if next room is a boss room
            if (IsBossRoom(freeRoomIndex))
            {
                //Choose random room name
                int rnd = UnityEngine.Random.Range(0, ItemsSpawner.gameSettingsSO.RoomNames.Length - 1);

                int rndSprite = UnityEngine.Random.Range(0, ItemsSpawner.gameSettingsSO.RoomSprites.Length - 1);

                _rooms[freeRoomIndex] = new Room(ItemsSpawner.gameSettingsSO.RoomNames[rnd], _currentRoom.CurRoomNumber + 1, _currentRoom.MaxRoomWaveHealth * (_currentRoom.CurRoomNumber + 1), 1, ItemsSpawner.gameSettingsSO.RoomSprites[rndSprite], roomModificator);

                // Cache room reference
                _currentRoom = _rooms[freeRoomIndex];

                // Set room wave to maximum
                _currentRoom.CurWaveNumber = _maxMosterWavePerRoom;

                // Set room reward
                _goldReward = _currentRoom.RoomGoldReward * _currentRoom.CurRoomNumber;

                // Set room type
                _currentRoom.CurRoomType = Room.RoomType.Boss;

                // Update wave counter
                Room_UI.UpdateMonsterWaveInfo();

                // Set boss health
                SetBossHealth();

                //Spawn first monsters wave
                _characterSpawner.SpawnBoss();

                // Set room health pool
                Room_UI.SetRoomWaveHealth(_currentRoom.MaxRoomWaveHealth);

                // Increment room's gold per kill
                _currentRoom.BossRoomGoldPerKill *= _currentRoom.CurRoomNumber;

                // Update GoldPerKill value in UI
                PlayerGoldCounterUI.UpdateGoldPerKill();
            }
            else
            {
                //Choose random room name
                int rnd = UnityEngine.Random.Range(0, ItemsSpawner.gameSettingsSO.RoomNames.Length - 1);

                int rndSprite = UnityEngine.Random.Range(0, ItemsSpawner.gameSettingsSO.RoomSprites.Length - 1);

                _rooms[freeRoomIndex] = new Room(ItemsSpawner.gameSettingsSO.RoomNames[rnd], _currentRoom.CurRoomNumber + 1, _currentRoom.MaxRoomWaveHealth * (_currentRoom.CurRoomNumber + 1), 3, ItemsSpawner.gameSettingsSO.RoomSprites[rndSprite], roomModificator);

                // Cache room reference
                _currentRoom = _rooms[freeRoomIndex];

                // Set room reward
                _goldReward = _currentRoom.RoomGoldReward * _currentRoom.CurRoomNumber;

                // Set room type
                _currentRoom.CurRoomType = Room.RoomType.Monsters;

                // Set monster health
                SetMonsterHealth();

                //Spawn first monsters wave
                _characterSpawner.SpawnMonsters();

                // Set room health pool
                Room_UI.SetRoomWaveHealth(_currentRoom.MaxRoomWaveHealth);

                // Increment room's gold per kill
                _currentRoom.RoomGoldPerKill *= _currentRoom.CurRoomNumber;

                // Update GoldPerKill value in UI
                PlayerGoldCounterUI.UpdateGoldPerKill();
            }
        }
    }

    /// <summary>
    /// DEBUG Damage room wave health by given amount
    /// </summary>
    /// <param name="incomingRoomWaveDamage"></param>
    public static void DamageRoomWaveHealth(float incomingRoomWaveDamage)
    {
        if (_currentRoom.CurRoomWaveHealth > 0.0001f)
        {
            _currentRoom.CurRoomWaveHealth -= incomingRoomWaveDamage;
        }
        else
        {
            _currentRoom.CurRoomWaveHealth = 0f;
            Debug.Log($"DamageRoomWaveHealth: _currentRoom.CurRoomWaveHealth = {_currentRoom.CurRoomWaveHealth}");
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

        _rooms[0] = new Room(ItemsSpawner.gameSettingsSO.RoomNames[rnd], 1, 10f, 3, ItemsSpawner.gameSettingsSO.RoomSprites[0], RoomModificator.Modificator.None);

        // Cache room reference
        _currentRoom = _rooms[0];

        // Set room reward
        _goldReward = _currentRoom.RoomGoldReward * _currentRoom.CurRoomNumber;

        // Set room type
        _currentRoom.CurRoomType = Room.RoomType.Monsters;

        // Set monster health
        SetMonsterHealth();

        //Spawn first monsters wave
        _characterSpawner.SpawnMonsters();

        // Set room health pool
        Room_UI.SetRoomWaveHealth(_currentRoom.MaxRoomWaveHealth);

        // Update GoldPerKill value in UI
        PlayerGoldCounterUI.UpdateGoldPerKill();
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
        _perMonsterHealth = _currentRoom.MaxRoomWaveHealth / _maxMonstersPerWave;
    }

    /// <summary>
    /// Set boss health amount in room
    /// </summary>
    private static void SetBossHealth()
    {
        _maxBossHealth = _currentRoom.MaxRoomWaveHealth;
    }

    /// <summary>
    /// Fills boss rooms array with boss room numbers
    /// </summary>
    /// <param name="bossRoomInterval">Boss room interval</param>
    private void SetBossRooms(int bossRoomInterval)
    {
        _bossRooms = new int[_bossesCount];

        int bossRoom = bossRoomInterval;
        int index = 0;

        for (int i = 0; i < _maxRooms; i++)
        {
            if (bossRoom == 1)
            {
                _bossRooms[index] = i + 1;
                bossRoom = bossRoomInterval;
                index++;
            }
            else
            {
                bossRoom--;
            }
        }
    }

    /// <summary>
    /// Check if given room is a boss room
    /// </summary>
    /// <param name="roomIndexToCheck"></param>
    /// <returns></returns>
    private static bool IsBossRoom(int roomIndexToCheck)
    {
        for (int i = 0; i < _bossRooms.Length; i++)
        {
            if (_bossRooms[i] == roomIndexToCheck + 1)
            {
                return true;
            }
        }

        return false;
    }
    #endregion
}
