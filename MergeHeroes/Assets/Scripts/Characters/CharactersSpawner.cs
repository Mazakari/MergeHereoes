// Roman Baranov 28.07.2021 

using System;
using UnityEngine;

public class CharactersSpawner : MonoBehaviour
{
    #region VARIABLES
    private GameSettingsSO _gameSettingsSO = null;// ������ �� SO ��� ��������� �������� ������ � ��������
    private HeroStatsUI _heroStatsUI = null;// ������ �� ������ � ������������ ���������� ����� � UI ��� ���������� ��������

    private Vector2 _camWorldPos;// ����� ������ ����� ������ � ������� �����������
    private Vector2 _monsterSpawnPos;// ����� ������ �������
    private Vector2 _heroSpawnPos;// ����� ������ �����

    [SerializeField] private Transform _heroesParent = null;// ������������ ������ ��� ������ ������
    [SerializeField] private Transform _monstersParent = null;// ������������ ������ ��� ������ ��������

    private static Monster _monster = null;
    /// <summary>
    /// ������, ����������� ������ �� �����
    /// </summary>
    public static Monster Monster { get { return _monster; } set { _monster = value; } }

    private static Hero _hero = null;
    /// <summary>
    /// �����, ����������� ������ �� �����
    /// </summary>
    public static Hero Hero { get { return _hero; } set { _hero = value; } }

    private int _monsterIndexToSpawn = 0;// ������ ������� � ������� �������� ��� ������

    /// <summary>
    /// ������� ���������� � ������ ������ �������
    /// </summary>
    public static event EventHandler OnMonsterSpawn;
    #endregion

    #region UNITY Methods
    private void Awake()
    {
        Debug.Log($"_heroesParent = {_heroesParent.name}");
        Debug.Log($"_monstersParent = {_monstersParent.name}");

        Monster.OnMonsterDead += Monster_OnMonsterDead;

        _gameSettingsSO = Resources.Load<GameSettingsSO>("ScriptableObjects/GameSettingsSO");
        _heroStatsUI = FindObjectOfType<HeroStatsUI>();

        _camWorldPos = Camera.main.ViewportToWorldPoint(Camera.main.transform.position);
        _monsterSpawnPos = new Vector2(-_camWorldPos.x / 2, 0);
        _heroSpawnPos = new Vector2(_camWorldPos.x / 2, 0);
    }
    // Start is called before the first frame update
    void Start()
    {
        SpawnMonster();
        SpawnHero();

        _heroStatsUI.UpdateHeroDamage(_hero.Damage);
        _heroStatsUI.UpdateHeroArmour(_hero.Armour);
        _heroStatsUI.UpdateGoldPerKill(_monster.MonsterGoldPerKill);
    }
    #endregion

    #region PRIVATE Methods
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
        //Debug.Log($"Hero {_hero} spawned!");
    }
    #endregion

    #region PUBLIC Methods
    /// <summary>
    /// ������� ������������� ������� � ������������ ������� ��������
    /// </summary>
    public void SpawnMonster()
    {
        // �������� ������� ��� ������
        GameObject monster = _gameSettingsSO.Monsters[_monsterIndexToSpawn];

        
        // ������� �������
        GameObject monsterClone = Instantiate(monster, _monsterSpawnPos, Quaternion.identity, _monstersParent);

        // ��������� ����� �� ���������������� ������ �������
        if (_monsterIndexToSpawn + 1 < _gameSettingsSO.Monsters.Length)
        {
            // �������������� ������ ������� ��� ���������� ������
            _monsterIndexToSpawn++;
        }

        // ��������� ������������� ������� � �������� �� �����
        _monster = monsterClone.GetComponent<Monster>();

        // ��������� ���������� GoldPerKill � LevelProgress
        LevelProgress.GoldPerKill = _monster.MonsterGoldPerKill;

        //Debug.Log($"SpawnMonster - Monster {_monster} spawned!");


        // ���������� ������� � ������ �������
        OnMonsterSpawn?.Invoke(this, EventArgs.Empty);
    }
    #endregion

    #region EVENTS
    private void Monster_OnMonsterDead(object sender, EventArgs e)
    {
        // ��������� ������ ������
        LevelProgress.CurrentGoldAmount += _monster.MonsterGoldPerKill;

        // ��������� ������� ������ ������
        PlayerGoldCounterUI.UpdateGoldCounter();

        // ���������� �������
        Destroy(_monster.gameObject);

        // ������� ������ �������
        SpawnMonster();
    }
    #endregion
}
