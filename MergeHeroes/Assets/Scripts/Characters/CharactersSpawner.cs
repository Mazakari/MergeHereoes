// Roman Baranov 28.07.2021 

using System;
using System.Collections.Generic;
using UnityEngine;

public class CharactersSpawner : MonoBehaviour
{
    #region VARIABLES
    private GameSettingsSO _gameSettingsSO = null;
    private HeroStatsUI _heroStatsUI = null;

    private Vector2 _camWorldPos;// Left bottom camera point in world coordinates

    private Vector2[] _monsterSpawnPos;
    private Vector2 _heroSpawnPos;

    [SerializeField] private Transform _heroesParent = null;
    [SerializeField] private Transform _monstersParent = null;

    private static List<Monster> _monsters = null;
    /// <summary>
    /// Active monsters collection
    /// </summary>
    public static List<Monster> Monsters { get { return _monsters; } set { _monsters = value; } }

    private static Hero _hero = null;
    /// <summary>
    /// Active hero
    /// </summary>
    public static Hero Hero { get { return _hero; } set { _hero = value; } }

    private int _monsterIndexToSpawn = 0;// Current monster index to spawn from total monsters prefabs collection

    /// <summary>
    /// Callback on monster spawn
    /// </summary>
    public static event EventHandler<Monster> OnMonsterSpawn;

    /// <summary>
    /// Callback on all monsters in room were killed
    /// </summary>
    public static event EventHandler OnRoomCleared;
    #endregion

    #region UNITY Methods
    private void Awake()
    {
        _monsters = new List<Monster>();

        _gameSettingsSO = Resources.Load<GameSettingsSO>("ScriptableObjects/GameSettingsSO");
        _heroStatsUI = FindObjectOfType<HeroStatsUI>();

        _camWorldPos = Camera.main.ViewportToWorldPoint(Camera.main.transform.position);
        
    }

    // Start is called before the first frame update
    void Start()
    {
        SetSpawnPoints();
        SpawnHero();

        _heroStatsUI.UpdateHeroArmour(_hero.Armour);
        _heroStatsUI.UpdateHeroDamage(_hero.Damage);
    }
    #endregion

    #region PRIVATE Methods
    /// <summary>
    /// Set monster wave and hero spawn points
    /// </summary>
    private void SetSpawnPoints()
    {
        Vector2 startSpawnPoint = new Vector2(-_camWorldPos.x / 2, 0.5f);
        float yOffset = 0.8f;

        _monsterSpawnPos = new Vector2[Level.MaxMonstersPerWave];

        for (int i = 0; i < _monsterSpawnPos.Length; i++)
        {
            _monsterSpawnPos[i] = new Vector2(startSpawnPoint.x, startSpawnPoint.y);
            startSpawnPoint.y -= yOffset;
        }

        _heroSpawnPos = new Vector2(_camWorldPos.x / 2, _monsterSpawnPos[1].y);
    }

    /// <summary>
    /// Spawn hero in heroes parent object
    /// </summary>
    private void SpawnHero()
    {
        int rndIndex = UnityEngine.Random.Range(0, _gameSettingsSO.Heroes.Length);
        GameObject hero = _gameSettingsSO.Heroes[rndIndex];

        GameObject heroClone = Instantiate(hero, _heroSpawnPos, Quaternion.identity, _heroesParent);

        // Add spawned hero reference
        _hero = heroClone.GetComponent<Hero>();
    }
    #endregion

    #region PUBLIC Methods
    /// <summary>
    /// Spawn a monsters wave 
    /// </summary>
    public void SpawnMonsters()
    {
        for (int i = 0; i < Level.MaxMonstersPerWave; i++)
        {
            SpawnMonster();
        }
    }

    /// <summary>
    /// Spawn monster in monsters parent object
    /// </summary>
    public void SpawnMonster()
    {
        // Get monster to spawn
        GameObject monster = _gameSettingsSO.Monsters[_monsterIndexToSpawn];

        // Spawn monster
        GameObject monsterClone = Instantiate(monster, _monsterSpawnPos[_monsters.Count], Quaternion.identity, _monstersParent);

        // Check index out of range 
        if (_monsterIndexToSpawn + 1 < _gameSettingsSO.Monsters.Length)
        {
            // Increment monster index for the next spawn
            _monsterIndexToSpawn++;
        }

        // Add spawned monster to current monster wave collection
        _monsters.Add(monsterClone.GetComponent<Monster>());

        // ��������� ���������� GoldPerKill � LevelProgress
        // TO DO �������� ��� ��������� GPK ��� 3 ��������
        LevelProgress.GoldPerKill = monsterClone.GetComponent<Monster>().MonsterGoldPerKill;
        //Debug.Log($"SpawnMonster - Monster {_monster} spawned!");

        // ������������� �� ������� ������ �������
        monsterClone.GetComponent<Monster>().OnMonsterDead += Monster_OnMonsterDead;

        // ���������� ������� � ������ �������
        OnMonsterSpawn?.Invoke(this, monsterClone.GetComponent<Monster>());
    }
    #endregion

    #region EVENTS
    /// <summary>
    /// Handle monster spawn and room loading on monster dead
    /// </summary>
    /// <param name="sender">Callback sender</param>
    /// <param name="e">Additional callback agruments</param>
    private void Monster_OnMonsterDead(object sender, Monster e)
    {
        // ��������� ������ ������
        LevelProgress.CurrentGoldAmount += e.MonsterGoldPerKill;

        // ��������� ������� ������ ������
        PlayerGoldCounterUI.UpdateGoldCounter();

        // ������� ������� �� ������� �����
        _monsters.Remove(e);

        // ��������� ������� �������� � ������� �������
        Level.CurrentRoom.CurMonstersInWave--;

        // ���������� �������
        Destroy(e.gameObject);

        // ��������� �������� �� ������� � ������� �����, ���� ���, �� ������� ����� �����
        if (Level.CurrentRoom.CurMonstersInWave == 0 &&
            Level.CurrentRoom.CurWaveNumber < Level.MaxMonsterWavePerRoom)
        {
            // ��������� ������� �������� � ������� �������
            Level.CurrentRoom.CurMonstersInWave = 3;

            // ����������� ������� �����
            Level.CurrentRoom.CurWaveNumber++;

            // ������� ����� ����� ��������
            SpawnMonsters();

            // Update monster wave number in UI
            Room_UI.UpdateMonsterWaveInfo();
        }
        else if (Level.CurrentRoom.CurMonstersInWave == 0 &&
                 Level.CurrentRoom.CurWaveNumber >= Level.MaxMonsterWavePerRoom)
        {
            // ��� ����� � ������� ��������, ������ �������
            OnRoomCleared?.Invoke(this, EventArgs.Empty);

            _monsterIndexToSpawn = 0;
        }
    }
    #endregion
}
