// Roman Baranov 28.07.2021 

using UnityEngine;

public class CharactersSpawner : MonoBehaviour
{
    #region VARIABLES
    private GameSettingsSO _gameSettingsSO = null;// ������ �� SO ��� ��������� �������� ������ � ��������

    private Vector2 _camWorldPos;// ����� ������ ����� ������ � ������� �����������

    [SerializeField] private Transform _heroesParent = null;// ������������ ������ ��� ������ ������
    [SerializeField] private Transform _monstersParent = null;// ������������ ������ ��� ������ ��������

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
    /// ������� ������������� ����� � ������������ ������� ������
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
    /// ������� ������������� ������� � ������������ ������� ��������
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
