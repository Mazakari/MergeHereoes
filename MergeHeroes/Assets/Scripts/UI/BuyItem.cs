// Roman Baranov 27.07.2021

using UnityEngine;
using UnityEngine.UI;

public class BuyItem : MonoBehaviour
{
    #region VARIABLES
    private Button _buyItemButton = null;

    private ItemsSpawner _itemsSpawner = null;

    #endregion

    #region UNITY Methods
    private void Awake()
    {
        _buyItemButton = GetComponent<Button>();

        _itemsSpawner = FindObjectOfType<ItemsSpawner>();
    }
    // Start is called before the first frame update
    void Start()
    {
        _buyItemButton.onClick.AddListener(BuyNewItem);

        // Обновляем текущую стоимость предмета в LevelProgress
        LevelProgress.CurrentItemBuyCost = GetItemByTier(LevelProgress.CurrentTierToBuy).ItemCost;

        // Обновить счетчик стоимости предмета
        ItemCostCounterUI.UpdateUtemCost();
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
        // Проверить достаточно ли у игрока золота для покупки
        if (LevelProgress.CurrentGoldAmount >= LevelProgress.CurrentItemBuyCost)
        {
            // Проверить есть ли свободные слоты в инвентаре
            if (ItemsSpawner.FindEmptySlot())
            {
                // Вычесть деньги за предмет у игрока
                LevelProgress.CurrentGoldAmount -= LevelProgress.CurrentItemBuyCost;

                // Умножить цену покупки на множитель предмета из LevelProgress
                LevelProgress.CurrentItemBuyCost *= LevelProgress.ItemCostMultiplier;

                // Обновить счетчик стоимости предмета
                ItemCostCounterUI.UpdateUtemCost();

                // Заспавнить предмет в первой свободной ячейке
                _itemsSpawner.SpawnItem(1);

                // Обновить счетчик золота у игрока
                PlayerGoldCounterUI.UpdateGoldCounter();
            }
            else
            {
                Debug.Log("No empty slots avaiable");
            }
        }
        else
        {
            Debug.Log("Not enough money");
        }
    }

    /// <summary>
    /// Находит предмет по заданному тиру и возвращает его. Иначе возвращает null
    /// </summary>
    /// <param name="itemTier">Тир для поиска предмета</param>
    /// <returns>Item</returns>
    private Item GetItemByTier(int itemTier)
    {
        for (int i = 0; i < ItemsSpawner.gameSettingsSO.Items.Length; i++)
        {
            if (ItemsSpawner.gameSettingsSO.Items[i].GetComponent<Item>().ItemTier == itemTier)
            {
                return ItemsSpawner.gameSettingsSO.Items[i].GetComponent<Item>();
            }
        }

        return null;
    }
    #endregion
}
