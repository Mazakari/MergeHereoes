// Roman Baranov 05.08.2021

using UnityEngine;
using UnityEngine.UI;

public class HeroStatsUI : MonoBehaviour
{
    #region VARIABLES
    [Header("Hero Stats Section")]
    [SerializeField] private Text _heroDamageText = null;// Текущий урон героя
    [SerializeField] private Text _heroArmourText = null;// Текущий показатель брони героя
    [SerializeField] private Text _goldPerKillText = null;// Текущий доход героя за убийство монстра
    #endregion

    #region UNITY Methods
    // Start is called before the first frame update
    void Start()
    {
        CharactersSpawner.OnMonsterSpawn += CharactersSpawner_OnMonsterSpawn;
    }
    #endregion

    #region PUBLIC Methods
    /// <summary>
    /// Обновляет отображаемую статистику героя в интерфейсе
    /// </summary>
    /// <param name="itemDamage">Новый урон героя</param>
    public void UpdateHeroDamage(float itemDamage)
    {
        _heroDamageText.text = $"Damage: {itemDamage:F2}";
       
    }

    /// <summary>
    /// Обновляет показатель брони героя
    /// </summary>
    /// <param name="itemArmour">Новый показатель брони героя</param>
    public void UpdateHeroArmour(float itemArmour)
    {
        _heroArmourText.text = $"Armour: {itemArmour:F2}";
    }

    /// <summary>
    /// Обновляет значение золота за убийство монстра
    /// </summary>
    /// <param name="goldPerKill">Новый доход героя за убийство монстра</param>
    public void UpdateGoldPerKill(float goldPerKill)
    {
        _goldPerKillText.text = $"Gold per kill: {goldPerKill:F2}";
    }
    #endregion


    #region PRIVATE Methods

    /// <summary>
    /// Обновляет счетчик золота за убийство монстра
    /// </summary>
    private void UpdateGoldPerKill()
    {
        _goldPerKillText.text = $"Gold per kill: {CharactersSpawner.Monster.MonsterGoldPerKill:F2}";
    }
    #endregion

    #region EVENTS
    /// <summary>
    /// Обновляет счетчик золота за убийство монстра при спавне нового монстра
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void CharactersSpawner_OnMonsterSpawn(object sender, System.EventArgs e)
    {
        UpdateGoldPerKill();
    }
    #endregion
}
