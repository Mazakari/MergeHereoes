// Roman Baranov 28.07.2021 

using UnityEngine;

public class CharactersSpawner : MonoBehaviour
{
    #region VARIABLES
    private GameSettingsSO _gameSettingsSO = null;// Ссылка на SO для получения префабов героев и монстров

    private Vector2 _camWorldPos;// Левая нижняя точка камеры в мировых координатах

    [SerializeField] private Transform _heroesParent = null;// Родительский объект для спавна героев
    [SerializeField] private Transform _monstersParent = null;// Родительский объект для спавна монстров

    #endregion

    #region UNITY Methods
    private void Awake()
    {
        _gameSettingsSO = Resources.Load<GameSettingsSO>("ScriptableObjects/GameSettingsSO");
    }
    // Start is called before the first frame update
    void Start()
    {
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

        Instantiate(hero, spawnPos, Quaternion.identity, _heroesParent);
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

        Instantiate(monster, spawnPos, Quaternion.identity, _monstersParent);
    }
    #endregion
}
