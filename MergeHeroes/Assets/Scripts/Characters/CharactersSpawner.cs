// Roman Baranov 28.07.2021 

using System;
using System.Collections.Generic;
using UnityEngine;

public class CharactersSpawner : MonoBehaviour
{
    #region VARIABLES
    private GameSettingsSO _gameSettingsSO = null;// ������ �� SO ��� ��������� �������� ������ � ��������
    private HeroStatsUI _heroStatsUI = null;// ������ �� ������ � ������������ ���������� ����� � UI ��� ���������� ��������

    private Vector2 _camWorldPos;// ����� ������ ����� ������ � ������� �����������

    private Vector2[] _monsterSpawnPos;// ����� ������ ��������
    private Vector2 _heroSpawnPos;// ����� ������ �����

    [SerializeField] private Transform _heroesParent = null;// ������������ ������ ��� ������ ������
    [SerializeField] private Transform _monstersParent = null;// ������������ ������ ��� ������ ��������

    private static List<Monster> _monsters = null;
    /// <summary>
    /// ������, ����������� ������ �� �����
    /// </summary>
    public static List<Monster> Monsters { get { return _monsters; } set { _monsters = value; } }

    private static Hero _hero = null;
    /// <summary>
    /// �����, ����������� ������ �� �����
    /// </summary>
    public static Hero Hero { get { return _hero; } set { _hero = value; } }

    private int _monsterIndexToSpawn = 0;// ������ ������� � ����� ������� �������� ��� ������

    /// <summary>
    /// ������� ���������� � ������ ������ �������
    /// </summary>
    public static event EventHandler<Monster> OnMonsterSpawn;

    /// <summary>
    /// ������� ����������, ����� ��� ����� �������� � ������� ���� ��������
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
        SetSpawnPoints();
    }

    // Start is called before the first frame update
    void Start()
    {
        Monster.OnMonsterDead += Monster_OnMonsterDead;

        _camWorldPos = Camera.main.ViewportToWorldPoint(Camera.main.transform.position);

        SpawnMonsters();
        SpawnHero();

        _heroStatsUI.UpdateHeroArmour(_hero.Armour);
        _heroStatsUI.UpdateHeroDamage(_hero.Damage);
    }
    #endregion

    #region PRIVATE Methods
    /// <summary>
    /// ��������� ����� ������ ��� ����� � ��������
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
    /// ������� ������������� ����� � ������������ ������� ������
    /// </summary>
    private void SpawnHero()
    {
        int rndIndex = UnityEngine.Random.Range(0, _gameSettingsSO.Heroes.Length);
        GameObject hero = _gameSettingsSO.Heroes[rndIndex];

        GameObject heroClone = Instantiate(hero, _heroSpawnPos, Quaternion.identity, _heroesParent);

        // ��������� ������������� ����� � �������� �� �����
        _hero = heroClone.GetComponent<Hero>();

    }
    #endregion

    #region PUBLIC Methods
    /// <summary>
    /// �������� �������� � �����
    /// </summary>
    public void SpawnMonsters()
    {
        for (int i = 0; i < Level.MaxMonstersPerWave; i++)
        {
            SpawnMonster();
        }
    }

    /// <summary>
    /// ������� ������������� ������� � ������������ ������� ��������
    /// </summary>
    public void SpawnMonster()
    {
        // �������� ������� ������� ��� ������
        Vector2 spawnPos = new Vector2(-_camWorldPos.x / 2, 0);

        // �������� ������� ��� ������
        GameObject monster = _gameSettingsSO.Monsters[_monsterIndexToSpawn];
        
        // ������� �������
        GameObject monsterClone = Instantiate(monster, _monsterSpawnPos[_monsters.Count], Quaternion.identity, _monstersParent);

        // ��������� ����� �� ���������������� ������ �������
        if (_monsterIndexToSpawn + 1 < _gameSettingsSO.Monsters.Length)
        {
            // �������������� ������ ������� ��� ���������� ������
            _monsterIndexToSpawn++;
        }

        // ��������� ������������� ������� � �������� �� �����
        _monsters.Add(monsterClone.GetComponent<Monster>());

        // ��������� ���������� GoldPerKill � LevelProgress
        // TO DO �������� ��� ��������� GPK ��� 3 ��������
        LevelProgress.GoldPerKill = monsterClone.GetComponent<Monster>().MonsterGoldPerKill;
        //Debug.Log($"SpawnMonster - Monster {_monster} spawned!");

        // ������������� �� ������� ������ �������
        //monsterClone.GetComponent<Monster>().OnMonsterDead += Monster_OnMonsterDead;

        // ���������� ������� � ������ �������
        OnMonsterSpawn?.Invoke(this, monsterClone.GetComponent<Monster>());
    }
    #endregion

    #region EVENTS
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

            // ��������� ���������� � ������� � UI
            //LevelInfo_UI.UpdateRoomInfoText();
        }
        else if (Level.CurrentRoom.CurMonstersInWave == 0 &&
                 Level.CurrentRoom.CurWaveNumber >= Level.MaxMonsterWavePerRoom)
        {
            // ��� ����� � ������� ��������, ������ �������
            OnRoomCleared?.Invoke(this, EventArgs.Empty);

            _monsterIndexToSpawn = 0;
            SpawnMonsters();
        }
    }
    #endregion
}
