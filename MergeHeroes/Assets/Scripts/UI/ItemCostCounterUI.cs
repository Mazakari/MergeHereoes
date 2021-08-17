// Roman Baranov 28.07.2021

using UnityEngine;
using UnityEngine.UI;

public class ItemCostCounterUI : MonoBehaviour
{
    #region VARIABLES
    private static Text _itemCostText = null;// ������ �� ��������� � ������� ��������
    private static Image _buyItemImage = null;// ������ �� ������ �������� ��� �������
    #endregion

    #region UNITY Methods
    private void Awake()
    {
        _itemCostText = transform.Find("BuyItemButton").Find("ItemCostCounterText").GetComponent<Text>();
        _buyItemImage = transform.Find("BuyItemButton").Find("BuyItemImage").GetComponent<Image>();
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateUtemCost();
        UpdateItemSprite(ItemsSpawner.gameSettingsSO.Items[1].GetComponent<Image>().sprite);
    }
    #endregion

    #region PRIVATE Methods
   
    #endregion

    /// <summary>
    /// ��������� ������� � ������������ ���� ������� ��������
    /// </summary>
    public static void UpdateUtemCost()
    {
        _itemCostText.text = $"{LevelProgress.CurrentItemBuyCost:F2}";
    }

    /// <summary>
    /// ��������� ������ �������� ��� ������� �������� ��������
    /// </summary>
    /// <param name="itemSprite">������ ������ ��������</param>
    public static void UpdateItemSprite(Sprite itemSprite)
    {
        _buyItemImage.sprite = itemSprite;
    }
}
