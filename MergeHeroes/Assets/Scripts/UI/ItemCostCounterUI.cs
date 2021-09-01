// Roman Baranov 28.07.2021

using UnityEngine;
using UnityEngine.UI;

public class ItemCostCounterUI : MonoBehaviour
{
    #region VARIABLES
    // ����
    private static Text _swordCostText = null;// ������ �� ��������� � ������� ��������
    private static Image _swordImage = null;// ������ �� ������ �������� ��� �������

    // �����
    private static Text _armourCostText = null;// ������ �� ��������� � ������� ��������
    private static Image _armourImage = null;// ������ �� ������ �������� ��� �������

    // �����
    private static Text _potionCostText = null;// ������ �� ��������� � ������� ��������
    private static Image _potionImage = null;// ������ �� ������ �������� ��� �������
    #endregion

    #region UNITY Methods
    private void Awake()
    {
        // ����
        _swordCostText = transform.Find("BuySwordButton").Find("SwordCostCounterText").GetComponent<Text>();
        _swordImage = transform.Find("BuySwordButton").Find("SwordImage").GetComponent<Image>();

        // �����
        _armourCostText = transform.Find("BuyArmourButton").Find("ArmourCostCounterText").GetComponent<Text>();
        _armourImage = transform.Find("BuyArmourButton").Find("ArmourImage").GetComponent<Image>();

        // �����
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
    /// ��������� ������� � ������������ ���� ������� ���� ���������
    /// </summary>
    public static void UpdateItemsCostUI()
    {
        _swordCostText.text = $"{LevelProgress.CurrentSwordBuyCost:F2}";
        _armourCostText.text = $"{LevelProgress.CurrentArmourBuyCost:F2}";
        _potionCostText.text = $"{LevelProgress.CurrentPotionBuyCost:F2}";
    }

    /// <summary>
    /// ��������� ������� � ������������ ���� ������� ����
    /// </summary>
    public static void UpdateSwordCostUI() => _swordCostText.text = $"{LevelProgress.CurrentSwordBuyCost:F2}";

    /// <summary>
    /// ��������� ������� � ������������ ���� ������� �����
    /// </summary>
    public static void UpdateArmourCostUI() => _armourCostText.text = $"{LevelProgress.CurrentArmourBuyCost:F2}";

    /// <summary>
    /// ��������� ������� � ������������ ���� ������� �����
    /// </summary>
    public static void UpdatePotionCostUI() => _potionCostText.text = $"{LevelProgress.CurrentPotionBuyCost:F2}";
    #endregion

    #region PRIVATE Methods
    /// <summary>
    /// ��������� ������� ���� ���������
    /// </summary>
    private void UpdateItemsSpriteUI()
    {
        _swordImage.sprite = ItemsSpawner.gameSettingsSO.Swords[LevelProgress.CurrentSwordTierToBuy - 1].GetComponent<Image>().sprite;
        _armourImage.sprite = ItemsSpawner.gameSettingsSO.Armour[LevelProgress.CurrentArmourTierToBuy - 1].GetComponent<Image>().sprite;
        _potionImage.sprite = ItemsSpawner.gameSettingsSO.Potions[LevelProgress.CurrentPotionTierToBuy - 1].GetComponent<Image>().sprite;
    }
    #endregion

}
