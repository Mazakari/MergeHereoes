// Roman Baranov 05.08.2021

using UnityEngine;
using UnityEngine.UI;

public class HeroStatsUI : MonoBehaviour
{
    #region VARIABLES
    [Header("Hero Item Section")]
    [SerializeField] private Sprite _defaultItemTier = null;// Спрайт для предмета по умолчаню
    public Sprite DefaultItemSprite { get { return _defaultItemTier; } }

    [SerializeField] private Image _heroItemImage = null;// Изображение текущего снаряженного предмета на герое
    [SerializeField] private Text _heroItemTierText = null;// Описание текущего снаряженного предмета на герое

    [Space(3)]
    [Header("Hero Stats Section")]
    [SerializeField] private Text _heroDamageText = null;// Текущий урон героя
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
    /// <param name="itemSprite">Спрайт нового предмета</param>
    /// <param name="itemTier">Тир нового предмета</param>
    /// <param name="itemDamage">Новый урон героя</param>
    /// <param name="heroGoldPerKill">Новый доход героя за убийство монстра</param>
    public void UpdateHeroStats(Sprite itemSprite, int itemTier, float itemDamage, float goldPerKill)
    {
        _heroItemImage.sprite = itemSprite;
        _heroItemTierText.text = $"Tier {itemTier}";

        _heroDamageText.text = $"Damage: {itemDamage.ToString("F2")}";
        _goldPerKillText.text = $"Gold per kill: {goldPerKill.ToString("F2")}";
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
