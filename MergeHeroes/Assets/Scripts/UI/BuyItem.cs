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
        LevelProgress.CurrentSwordBuyCost = GetISwordByTier(LevelProgress.CurrentSwordTierToBuy).GetComponent<Item>().Cost;

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
        if (LevelProgress.CurrentGoldAmount >= LevelProgress.CurrentSwordBuyCost)
        {
            // Проверить есть ли свободные слоты в инвентаре
            if (ItemsSpawner.FindEmptySlot())
            {
                // Вычесть деньги за предмет у игрока
                LevelProgress.CurrentGoldAmount -= LevelProgress.CurrentSwordBuyCost;

                // Умножить цену покупки на множитель предмета из LevelProgress
                LevelProgress.CurrentSwordBuyCost *= LevelProgress.SwordCostMultiplier;

                // Обновить счетчик стоимости предмета
                ItemCostCounterUI.UpdateUtemCost();

                // Заспавнить предмет в первой свободной ячейке
                _itemsSpawner.SpawnItem(1, ItemTypes.Items.Sword);

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
    /// Находит меч по заданному тиру и возвращает его. Иначе возвращает null
    /// </summary>
    /// <param name="itemTier">Тир меча для поиска</param>
    /// <returns>Item</returns>
    private Sword GetISwordByTier(int itemTier)
    {
        for (int i = 0; i < ItemsSpawner.gameSettingsSO.Swords.Length; i++)
        {
            if (ItemsSpawner.gameSettingsSO.Swords[i].GetComponent<Item>().Tier == itemTier)
            {
                return ItemsSpawner.gameSettingsSO.Swords[i].GetComponent<Sword>();
            }
        }

        return null;
    }

    /// <summary>
    /// Находит броню по заданному тиру и возвращает его. Иначе возвращает null
    /// </summary>
    /// <param name="itemTier">Тир брони для поиска</param>
    /// <returns></returns>
    private Armour GetArmourByTier(int itemTier)
    {
        for (int i = 0; i < ItemsSpawner.gameSettingsSO.Armour.Length; i++)
        {
            if (ItemsSpawner.gameSettingsSO.Armour[i].GetComponent<Item>().Tier == itemTier)
            {
                return ItemsSpawner.gameSettingsSO.Armour[i].GetComponent<Armour>();
            }
        }

        return null;
    }

    /// <summary>
    /// Находит зелье по заданному тиру и возвращает его. Иначе возвращает null
    /// </summary>
    /// <param name="itemTier">Тир зелья для поиска</param>
    /// <returns></returns>
    private Potion GetPotionByTier(int itemTier)
    {
        for (int i = 0; i < ItemsSpawner.gameSettingsSO.Potions.Length; i++)
        {
            if (ItemsSpawner.gameSettingsSO.Potions[i].GetComponent<Item>().Tier == itemTier)
            {
                return ItemsSpawner.gameSettingsSO.Potions[i].GetComponent<Potion>();
            }
        }

        return null;
    }
    #endregion
}
