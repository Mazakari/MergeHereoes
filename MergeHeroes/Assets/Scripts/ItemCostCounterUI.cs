// Roman Baranov 28.07.2021

using UnityEngine;
using UnityEngine.UI;

public class ItemCostCounterUI : MonoBehaviour
{
    #region VARIABLES
    private Image _itemCounterImage = null;

    private static Text _itemCostText = null;

    private GameSettingsSO _gameSettingsSO = null;// Ссылка на SO с коллекцией предметов для спавна
    #endregion

    #region UNITY Methods
    private void Awake()
    {
        _itemCounterImage = transform.Find("BuyItemImage").GetComponent<Image>();

        _itemCostText = transform.Find("ItemCostCounterText").GetComponent<Text>();

        _gameSettingsSO = Resources.Load<GameSettingsSO>("ScriptableObjects/GameSettingsSO");
    }
   
    // Start is called before the first frame update
    void Start()
    {
        _itemCounterImage.sprite = FindItemSprite();

        UpdateUtemCost();
    }
    #endregion

    #region PRIVATE Methods
    /// <summary>
    /// Находит спрайт текущего проедмета для покупки
    /// </summary>
    /// <returns>Sprite</returns>
    private Sprite FindItemSprite()
    {
        for (int i = 0; i < _gameSettingsSO.Items.Length; i++)
        {
            if (_gameSettingsSO.Items[i].GetComponent<Item>().ItemTier == GameSettingsSO.CurrentTierToBuy)
            {
                return _gameSettingsSO.Items[i].GetComponent<SpriteRenderer>().sprite;
            }
        }

        return null;
    }
    #endregion

    /// <summary>
    /// Обновляет счетчик с отображением цены покупки предмета
    /// </summary>
    public static void UpdateUtemCost()
    {
        _itemCostText.text = $"{GameSettingsSO.CurrentItemBuyCost.ToString("F2")}";
    }
}
