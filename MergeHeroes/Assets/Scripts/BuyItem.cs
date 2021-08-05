// Roman Baranov 27.07.2021

using UnityEngine;
using UnityEngine.UI;

public class BuyItem : MonoBehaviour
{
    #region VARIABLES
    private Button _buyItemButton = null;// Ссылку на кнопку покупки предмета

    private ItemContainerManager _itemContainerManager = null;// Ссылка на скрипт для вызова метода спавна предмета

    #endregion

    #region UNITY Methods
    private void Awake()
    {
        _buyItemButton = GetComponent<Button>();

        _itemContainerManager = FindObjectOfType<ItemContainerManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        _buyItemButton.onClick.AddListener(BuyNewItem);
    }

    private void OnDestroy()
    {
        _buyItemButton.onClick.RemoveAllListeners();
    }
    #endregion

    #region PRIVATE Methods
    /// <summary>
    /// Обрабатывает покупку предмета при нажатии на кнопку
    /// </summary>
    private void BuyNewItem()
    {
        // Проверить стоимость предмета и золото игрока
        if (ItemsManagerSO.CurrentItemBuyCost <= PlayerSettingsSO.CurrentGoldAmount && !ItemContainerManager.InventoryFull)
        {
            // Вычесть стоимость предмета из стоимости игрока
            PlayerSettingsSO.CurrentGoldAmount -= ItemsManagerSO.CurrentItemBuyCost;

            // Увеличить стоимость предмета на множитель
            ItemsManagerSO.CurrentItemBuyCost *= ItemsManagerSO.ItemCostMultiplier;

            // Обновить счетчик золота игрока
            PlayerGoldCounter.UpdateGoldCounter();

            // Обновить счетчик стоимости предмета
            ItemCostCounter.UpdateUtemCost();

            //Заспавнить 1 предмет
            _itemContainerManager.SpawnItem(1);
        }
        else
        {
            Debug.Log($"Not enough gold! Item cost is {ItemsManagerSO.CurrentItemBuyCost}, player have {PlayerSettingsSO.CurrentGoldAmount} gold.");
            Debug.Log($"Inventory is Full {ItemContainerManager.SpawnedItems} / {MergePanelManager.InventorySize}");
        }
        
    }

    #endregion
}
