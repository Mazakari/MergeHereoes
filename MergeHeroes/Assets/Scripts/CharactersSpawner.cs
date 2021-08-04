// Roman Baranov 28.07.2021 

using UnityEngine;

public class CharactersSpawner : MonoBehaviour
{
    #region VARIABLES
    private GameSettingsSO _gameSettingsSO = null;// Ссылка на SO для получения префабов героев и монстров

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

    #endregion

    #region UNITY Methods
    private void Awake()
    {
        _gameSettingsSO = Resources.Load<GameSettingsSO>("ScriptableObjects/GameSettingsSO");
    }
    // Start is called before the first frame update
    void Start()
    {
        Monster.OnMonsterDead += Monster_OnMonsterDead;

        _camWorldPos = Camera.main.ViewportToWorldPoint(Camera.main.transform.position);

        SpawnMonster();
        SpawnHero();
    }

    
    #endregion

    #region PRIVATE Methods
    /// <summary>
    /// Спавнит произвольного героя в родительском объекте героев
    /// </summary>
    private void SpawnHero()
    {
        Vector2 spawnPos = new Vector2(_camWorldPos.x / 2, 0);
        int rndIndex = Random.Range(0, _gameSettingsSO.Heroes.Length);
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
        Vector2 spawnPos = new Vector2(-_camWorldPos.x / 2, 0);
        int rndIndex = Random.Range(0, _gameSettingsSO.Monsters.Length);
        GameObject monster = _gameSettingsSO.Monsters[rndIndex];

        GameObject monsterClone = Instantiate(monster, spawnPos, Quaternion.identity, _monstersParent);

        // Добавляем заспавленного монстра в активные на сцене
        _monster = monsterClone.GetComponent<Monster>();
    }
    #endregion

    #region EVENTS
    private void Monster_OnMonsterDead(object sender, System.EventArgs e)
    {
        // Убираем текущего монстра
        Destroy(_monster.gameObject);
        
        // Спавним нового монстра
        SpawnMonster();
    }
    #endregion
}
