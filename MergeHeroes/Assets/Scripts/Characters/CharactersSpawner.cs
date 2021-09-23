// Roman Baranov 28.07.2021 

using System;
using System.Collections.Generic;
using UnityEngine;

public class CharactersSpawner : MonoBehaviour
{
    #region VARIABLES
    private GameSettingsSO _gameSettingsSO = null;// Ссылка на SO для получения префабов героев и монстров
    private HeroStatsUI _heroStatsUI = null;// Ссылка на скрипт с отображением статистики героя в UI для обновления значений

    private Vector2 _camWorldPos;// Левая нижняя точка камеры в мировых координатах

    private Vector2[] _monsterSpawnPos;// Точки спавна монстров
    private Vector2 _heroSpawnPos;// Точка спавна героя

    [SerializeField] private Transform _heroesParent = null;// Родительский объект для спавна героев
    [SerializeField] private Transform _monstersParent = null;// Родительский объект для спавна монстров

    private static List<Monster> _monsters = null;
    /// <summary>
    /// Монстр, находящийся сейчас на сцене
    /// </summary>
    public static List<Monster> Monsters { get { return _monsters; } set { _monsters = value; } }

    private static Hero _hero = null;
    /// <summary>
    /// Герой, находящийся сейчас на сцене
    /// </summary>
    public static Hero Hero { get { return _hero; } set { _hero = value; } }

    private int _monsterIndexToSpawn = 0;// Индекс монстра в общем массиве монстров для спавна

    /// <summary>
    /// Событие вызывается в момент спавна монстра
    /// </summary>
    public static event EventHandler<Monster> OnMonsterSpawn;

    /// <summary>
    /// Событие вызывается, когда все волны монстров в комнате были зачищены
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
    /// Вычисляет точки спавна для героя и монстров
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
    /// Спавнит произвольного героя в родительском объекте героев
    /// </summary>
    private void SpawnHero()
    {
        int rndIndex = UnityEngine.Random.Range(0, _gameSettingsSO.Heroes.Length);
        GameObject hero = _gameSettingsSO.Heroes[rndIndex];

        GameObject heroClone = Instantiate(hero, _heroSpawnPos, Quaternion.identity, _heroesParent);

        // Добавляем заспавленного героя в активные на сцене
        _hero = heroClone.GetComponent<Hero>();

    }
    #endregion

    #region PUBLIC Methods
    /// <summary>
    /// Спасвнит монстров в волне
    /// </summary>
    public void SpawnMonsters()
    {
        for (int i = 0; i < Level.MaxMonstersPerWave; i++)
        {
            SpawnMonster();
        }
    }

    /// <summary>
    /// Спавнит произвольного монстра в родительском объекте монстров
    /// </summary>
    public void SpawnMonster()
    {
        // Выбираем позицию монстра для спавна
        Vector2 spawnPos = new Vector2(-_camWorldPos.x / 2, 0);

        // Выбираем монстра для спавна
        GameObject monster = _gameSettingsSO.Monsters[_monsterIndexToSpawn];
        
        // Спавним монстра
        GameObject monsterClone = Instantiate(monster, _monsterSpawnPos[_monsters.Count], Quaternion.identity, _monstersParent);

        // Проверяем можно ли инкрементировать индекс монстра
        if (_monsterIndexToSpawn + 1 < _gameSettingsSO.Monsters.Length)
        {
            // Инкрементируем индекс монстра для следующего спавна
            _monsterIndexToSpawn++;
        }

        // Добавляем заспавленного монстра в активные на сцене
        _monsters.Add(monsterClone.GetComponent<Monster>());

        // Обновляем показатель GoldPerKill в LevelProgress
        // TO DO Подумать как расчитать GPK для 3 монстров
        LevelProgress.GoldPerKill = monsterClone.GetComponent<Monster>().MonsterGoldPerKill;
        //Debug.Log($"SpawnMonster - Monster {_monster} spawned!");

        // Подписываемся на событие смерти монстра
        //monsterClone.GetComponent<Monster>().OnMonsterDead += Monster_OnMonsterDead;

        // Отправляем событие о спавне монстра
        OnMonsterSpawn?.Invoke(this, monsterClone.GetComponent<Monster>());
    }
    #endregion

    #region EVENTS
    private void Monster_OnMonsterDead(object sender, Monster e)
    {
        // Начисляем игроку золото
        LevelProgress.CurrentGoldAmount += e.MonsterGoldPerKill;

        // Обновляем счетчик золота игрока
        PlayerGoldCounterUI.UpdateGoldCounter();

        // Удаляем монстра из текущей волны
        _monsters.Remove(e);

        // Уменьшаем счетчик монстров в текущей комнате
        Level.CurrentRoom.CurMonstersInWave--;

        // Уничтожаем монстра
        Destroy(e.gameObject);

        // Проверяем остались ли монстры в текущей волне, если нет, то спавним новую волну
        if (Level.CurrentRoom.CurMonstersInWave == 0 &&
            Level.CurrentRoom.CurWaveNumber < Level.MaxMonsterWavePerRoom)
        {
            // Обновляем счетчик монстров в текущей комнате
            Level.CurrentRoom.CurMonstersInWave = 3;

            // Увеличиваем счетчик волны
            Level.CurrentRoom.CurWaveNumber++;

            // Спавним новую волну монстров
            SpawnMonsters();

            // Обновляем информацию о комнате в UI
            //LevelInfo_UI.UpdateRoomInfoText();
        }
        else if (Level.CurrentRoom.CurMonstersInWave == 0 &&
                 Level.CurrentRoom.CurWaveNumber >= Level.MaxMonsterWavePerRoom)
        {
            // Все волны в комнате зачищены, меняем комнату
            OnRoomCleared?.Invoke(this, EventArgs.Empty);

            _monsterIndexToSpawn = 0;
            SpawnMonsters();
        }
    }
    #endregion
}
