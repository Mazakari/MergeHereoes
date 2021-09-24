// Roman Baranov 28.07.2021

using UnityEngine;
using UnityEngine.UI;

public class PlayerGoldCounterUI : MonoBehaviour
{
    #region VARIABLES
    private static Text _counterText = null;// Ссылка на компонент текста счетчика
    private Text _goldPerKillText = null;//Gold per kill text reference

    #endregion

    #region UNITY Methods
    private void Awake()
    {
        _counterText = transform.Find("GoldCounterText").GetComponent<Text>();
        _goldPerKillText = transform.Find("GoldPerKillText").GetComponent<Text>();

        CharactersSpawner.OnMonsterSpawn += CharactersSpawner_OnMonsterSpawn;
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateGoldCounter();
    }
    #endregion

    #region PUBLIC Methods
    /// <summary>
    /// Обновляет значение счетчика золота игрока
    /// </summary>
    public static void UpdateGoldCounter()
    {
        if (_counterText != null)
        {
            _counterText.text = $"{LevelProgress.CurrentGoldAmount:F2}";
        }
    }
    #endregion

    #region PRIVATE Methods
    /// <summary>
    /// Обновляет счетчик золота за убийство монстра
    /// </summary>
    private void UpdateGoldPerKill(Monster monster)
    {
        if (_goldPerKillText != null)
        {
            _goldPerKillText.text = $"Gold per kill: {monster.MonsterGoldPerKill:F2}";
        }
    }
    #endregion

    #region EVENTS
    /// <summary>
    /// Обновляет счетчик золота за убийство монстра при спавне нового монстра
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void CharactersSpawner_OnMonsterSpawn(object sender, Monster e) => UpdateGoldPerKill(e);
    #endregion

}
