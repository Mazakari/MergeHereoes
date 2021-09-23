// Roman Baranov 05.08.2021

using UnityEngine;
using UnityEngine.UI;

public class HeroStatsUI : MonoBehaviour
{
    #region VARIABLES
    private Text _heroDamageText = null;// Hero damage text reference
    private Text _heroArmourText = null;// Hero armour text reference
    
    #endregion

    #region UNITY Methods
    private void Awake()
    {
        _heroDamageText = transform.Find("HeroDamageText").GetComponent<Text>();
        _heroArmourText = transform.Find("HeroArmourText").GetComponent<Text>();
    }
    #endregion

    #region PUBLIC Methods
    /// <summary>
    /// Update hero damage in UI
    /// </summary>
    /// <param name="itemDamage"></param>
    public void UpdateHeroDamage(float itemDamage)
    {
        if (_heroDamageText != null)
        {
            _heroDamageText.text = $"Damage: {itemDamage:F2}";
        }
    }

    /// <summary>
    /// Update hero armour in UI
    /// </summary>
    /// <param name="itemArmour"></param>
    public void UpdateHeroArmour(float itemArmour)
    {
        if (_heroArmourText != null)
        {
            _heroArmourText.text = $"Armour: {itemArmour:F2}";
        }
    }
    #endregion
}
