// Roman Baranov 28.07.2021

using UnityEngine;
using UnityEngine.UI;

public class ItemCostCounterUI : MonoBehaviour
{
    #region VARIABLES
    private static Text _itemCostText = null;// Ссылка на компонент с текстом счетчика
    private static Image _buyItemImage = null;// Ссылка на спрайт предмета для покупки
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
    /// Обновляет счетчик с отображением цены покупки предмета
    /// </summary>
    public static void UpdateUtemCost()
    {
        _itemCostText.text = $"{LevelProgress.CurrentItemBuyCost:F2}";
    }

    /// <summary>
    /// Обновляет спрайт предмета для покупки текущего предмета
    /// </summary>
    /// <param name="itemSprite">Спрайт нового предмета</param>
    public static void UpdateItemSprite(Sprite itemSprite)
    {
        _buyItemImage.sprite = itemSprite;
    }
}
