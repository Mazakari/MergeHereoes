// Roman Baranov 28.07.2021

using UnityEngine;
using UnityEngine.UI;

public class ItemCostCounterUI : MonoBehaviour
{
    #region VARIABLES
    // Мечи
    private static Text _swordCostText = null;// Ссылка на компонент с текстом счетчика
    private static Image _swordImage = null;// Ссылка на спрайт предмета для покупки

    // Броня
    private static Text _armourCostText = null;// Ссылка на компонент с текстом счетчика
    private static Image _armourImage = null;// Ссылка на спрайт предмета для покупки

    // Зелья
    private static Text _potionCostText = null;// Ссылка на компонент с текстом счетчика
    private static Image _potionImage = null;// Ссылка на спрайт предмета для покупки
    #endregion

    #region UNITY Methods
    private void Awake()
    {
        // Мечи
        _swordCostText = transform.Find("BuySwordButton").Find("SwordCostCounterText").GetComponent<Text>();
        _swordImage = transform.Find("BuySwordButton").Find("SwordImage").GetComponent<Image>();

        // Броня
        _armourCostText = transform.Find("BuyArmourButton").Find("ArmourCostCounterText").GetComponent<Text>();
        _armourImage = transform.Find("BuyArmourButton").Find("ArmourImage").GetComponent<Image>();

        // Зелья
        _potionCostText = transform.Find("BuyPotionButton").Find("PotionCostCounterText").GetComponent<Text>();
        _potionImage = transform.Find("BuyPotionButton").Find("PotionImage").GetComponent<Image>();
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateItemsCostUI();
        UpdateItemsSpriteUI();
    }
    #endregion

    #region PUBLIC Methods
    /// <summary>
    /// Обновляет счетчик с отображением цены покупки всех предметов
    /// </summary>
    public static void UpdateItemsCostUI()
    {
        _swordCostText.text = $"{LevelProgress.CurrentSwordBuyCost:F2}";
        _armourCostText.text = $"{LevelProgress.CurrentArmourBuyCost:F2}";
        _potionCostText.text = $"{LevelProgress.CurrentPotionBuyCost:F2}";
    }

    /// <summary>
    /// Обновляет счетчик с отображением цены покупки меча
    /// </summary>
    public static void UpdateSwordCostUI() => _swordCostText.text = $"{LevelProgress.CurrentSwordBuyCost:F2}";

    /// <summary>
    /// Обновляет счетчик с отображением цены покупки брони
    /// </summary>
    public static void UpdateArmourCostUI() => _armourCostText.text = $"{LevelProgress.CurrentArmourBuyCost:F2}";

    /// <summary>
    /// Обновляет счетчик с отображением цены покупки зелья
    /// </summary>
    public static void UpdatePotionCostUI() => _potionCostText.text = $"{LevelProgress.CurrentPotionBuyCost:F2}";
    #endregion

    #region PRIVATE Methods
    /// <summary>
    /// Обновляет спрайты всех предметов
    /// </summary>
    private void UpdateItemsSpriteUI()
    {
        _swordImage.sprite = ItemsSpawner.gameSettingsSO.Swords[LevelProgress.CurrentSwordTierToBuy - 1].GetComponent<Image>().sprite;
        _armourImage.sprite = ItemsSpawner.gameSettingsSO.Armour[LevelProgress.CurrentArmourTierToBuy - 1].GetComponent<Image>().sprite;
        _potionImage.sprite = ItemsSpawner.gameSettingsSO.Potions[LevelProgress.CurrentPotionTierToBuy - 1].GetComponent<Image>().sprite;
    }
    #endregion

}
