// Roman Baranov 28.07.2021 

using System;
using UnityEngine;

public class CharactersSpawner : MonoBehaviour
{
    #region VARIABLES
    private GameSettingsSO _gameSettingsSO = null;// Ссылка на SO для получения префабов героев и монстров
    private HeroStatsUI _heroStatsUI = null;// Ссылка на скрипт с отображением статистики героя в UI для обновления значений

    private Vector2 _camWorldPos;// Левая нижняя точка камеры в мировых координатах

    [SerializeField] private Transform _heroesParent = null;// Родительский объект для спавна героев
    [SerializeField] private Transform _monstersParent = null;// Родительский объект для спавна монстров

    private static Monster _monster = null;
    /// <summary>
    /// Монстр, находящийся сейчас на сцене
    /// </summary>
    public static Monster Monster { get { return _monster; } set { _monster = value; } }

    private static Hero _hero = null;
    /// <summary>
    /// Герой, находящийся сейчас на сцене
    /// </summary>
    public static Hero Hero { get { return _hero; } set { _hero = value; } }

    private int _monsterIndexToSpawn = 0;// Индекс монстра в массиве монстров для спавна

    /// <summary>
    /// Событие вызывается в момент спавна монстра
    /// </summary>
    public static event EventHandler OnMonsterSpawn;
    #endregion

    #region UNITY Methods
    private void Awake()
    {
        _gameSettingsSO = Resources.Load<GameSettingsSO>("ScriptableObjects/GameSettingsSO");
        _heroStatsUI = FindObjectOfType<HeroStatsUI>();
    }
    // Start is called before the first frame update
    void Start()
    {
        Monster.OnMonsterDead += Monster_OnMonsterDead;

        _camWorldPos = Camera.main.ViewportToWorldPoint(Camera.main.transform.position);

        SpawnMonster();
        SpawnHero();

        _heroStatsUI.UpdateHeroDamage(_hero.Damage);
        _heroStatsUI.UpdateHeroArmour(_hero.Armour);
        _heroStatsUI.UpdateGoldPerKill(_monster.MonsterGoldPerKill);
    }

    
    #endregion

    #region PRIVATE Methods
    /// <summary>
    /// Спавнит произвольного героя в родительском объекте героев
    /// </summary>
    private void SpawnHero()
    {
        Vector2 spawnPos = new Vector2(_camWorldPos.x / 2, 0);
        int rndIndex = UnityEngine.Random.Range(0, _gameSettingsSO.Heroes.Length);
        GameObject hero = _gameSettingsSO.Heroes[rndIndex];

        GameObject heroClone = Instantiate(hero, spawnPos, Quaternion.identity, _heroesParent);

        // Добавляем заспавленного героя в активные на сцене
        _hero = heroClone.GetComponent<Hero>();

    }
    #endregion

    #region PUBLIC Methods
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
        GameObject monsterClone = Instantiate(monster, spawnPos, Quaternion.identity, _monstersParent);

        // Проверяем можно ли инкрементировать индекс монстра
        if (_monsterIndexToSpawn + 1 < _gameSettingsSO.Monsters.Length)
        {
            // Инкрементируем индекс монстра для следующего спавна
            _monsterIndexToSpawn++;
        }

        // Добавляем заспавленного монстра в активные на сцене
        _monster = monsterClone.GetComponent<Monster>();

        // Обновляем показатель GoldPerKill в LevelProgress
        LevelProgress.GoldPerKill = _monster.MonsterGoldPerKill;

        // Отправляем событие о спавне монстра
        OnMonsterSpawn?.Invoke(this, EventArgs.Empty);
    }
    #endregion

    #region EVENTS
    private void Monster_OnMonsterDead(object sender, EventArgs e)
    {
        // Начисляем игроку золото
        LevelProgress.CurrentGoldAmount += _monster.MonsterGoldPerKill;

        // Обновляем счетчик золота игрока
        PlayerGoldCounterUI.UpdateGoldCounter();

        // Уничтожаем монстра
        Destroy(_monster.gameObject);

        // Спавним нового монстра
        SpawnMonster();
    }
    #endregion
}
