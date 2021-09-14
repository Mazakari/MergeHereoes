// Roman Baranov 05.08.2021

using UnityEngine;
using UnityEngine.UI;

public class HeroStatsUI : MonoBehaviour
{
    #region VARIABLES
    private Text _heroDamageText = null;// Текущий урон героя
    private Text _heroArmourText = null;// Текущий показатель брони героя
    private Text _goldPerKillText = null;// Текущий доход героя за убийство монстра
    #endregion

    #region UNITY Methods
    private void Awake()
    {
        _heroDamageText = transform.Find("HeroDamageText").GetComponent<Text>();
        _heroArmourText = transform.Find("HeroArmourText").GetComponent<Text>();
        _goldPerKillText = transform.Find("GoldPerKillText").GetComponent<Text>();

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
        if (_heroDamageText != null)
        {
            _heroDamageText.text = $"Damage: {itemDamage:F2}";
        }
        
       
    }

    /// <summary>
    /// Обновляет показатель брони героя
    /// </summary>
    /// <param name="itemArmour">Новый показатель брони героя</param>
    public void UpdateHeroArmour(float itemArmour)
    {
        if (_heroArmourText != null)
        {
            _heroArmourText.text = $"Armour: {itemArmour:F2}";
        }
       
    }

    /// <summary>
    /// Обновляет значение золота за убийство монстра
    /// </summary>
    /// <param name="goldPerKill">Новый доход героя за убийство монстра</param>
    public void UpdateGoldPerKill(float goldPerKill)
    {
        if (_goldPerKillText != null)
        {
            _goldPerKillText.text = $"Gold per kill: {goldPerKill:F2}";
        }
    }
    #endregion

    #region EVENTS
    /// <summary>
    /// Обновляет счетчик золота за убийство монстра при спавне нового монстра
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void CharactersSpawner_OnMonsterSpawn(object sender, System.EventArgs e) => UpdateGoldPerKill(CharactersSpawner.Monster.MonsterGoldPerKill);
    #endregion
}
