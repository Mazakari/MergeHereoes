// Roman Baranov 27.07.2021

using UnityEngine;
using UnityEngine.UI;

public class BuyItem : MonoBehaviour
{
    #region VARIABLES
    private Button _buyItemButton = null;

    [SerializeField] private ItemTypes.Items _buttonType;
    /// <summary>
    /// тип кнопки
    /// </summary>
    public ItemTypes.Items ButtonType { get { return _buttonType; } }


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

        UpdateItemsCosts();
       
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
        if (CheckItemCost(_buttonType))
        {
            // Проверить есть ли свободные слоты в инвентаре
            if (ItemsSpawner.FindEmptySlot())
            {
                switch (_buttonType)
                {
                    case ItemTypes.Items.Sword:
                        BuySword();
                        break;

                    case ItemTypes.Items.Armour:
                        BuyArmour();
                        break;

                    case ItemTypes.Items.Potion:
                        BuyPotion();
                        break;

                    default:
                        Debug.Log("There is no such Item Type found!");
                        break;
                }
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
    /// Проверяет достаточно ли у игрока денег на покупку выбранного типа предмета.
    /// Если хватает, возвращает true, если нет, то возвращает false
    /// </summary>
    /// <param name="itemType">Тип предмета, цену которого нужно проверить</param>
    /// <returns>bool</returns>
    private bool CheckItemCost(ItemTypes.Items itemType)
    {
        switch (itemType)
        {
            case ItemTypes.Items.Sword:
                if (LevelProgress.CurrentGoldAmount >= LevelProgress.CurrentSwordBuyCost)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            case ItemTypes.Items.Armour:
                if (LevelProgress.CurrentGoldAmount >= LevelProgress.CurrentArmourBuyCost)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            case ItemTypes.Items.Potion:
                if (LevelProgress.CurrentGoldAmount >= LevelProgress.CurrentPotionBuyCost)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            default:
                Debug.Log("No Such Item Type Found!");
                return false;
        }
    }

    /// <summary>
    /// Покупает предмет "меч" и спавнит его в свободной ячейке
    /// </summary>
    private void BuySword()
    {
        // Вычесть деньги за меч у игрока
        LevelProgress.CurrentGoldAmount -= LevelProgress.CurrentSwordBuyCost;

        // Умножить цену покупки на множитель стоимости меча из LevelProgress
        LevelProgress.CurrentSwordBuyCost *= LevelProgress.SwordCostMultiplier;

        // Обновить счетчик стоимости меча
        ItemCostCounterUI.UpdateSwordCostUI();

        // Заспавнить меч в первой свободной ячейке инвентаря
        _itemsSpawner.SpawnItem(1, ItemTypes.Items.Sword);

        // Обновить счетчик золота у игрока
        PlayerGoldCounterUI.UpdateGoldCounter();
    }

    /// <summary>
    /// Покупает предмет "броня" и спавнит его в свободной ячейке
    /// </summary>
    private void BuyArmour()
    {
        // Вычесть деньги за броню у игрока
        LevelProgress.CurrentGoldAmount -= LevelProgress.CurrentArmourBuyCost;

        // Умножить цену покупки брони на множитель предмета из LevelProgress
        LevelProgress.CurrentArmourBuyCost *= LevelProgress.ArmourCostMultiplier;

        // Обновить счетчик стоимости брони
        ItemCostCounterUI.UpdateArmourCostUI();

        // Заспавнить броню в первой свободной ячейке инвентаря
        _itemsSpawner.SpawnItem(1, ItemTypes.Items.Armour);

        // Обновить счетчик золота у игрока
        PlayerGoldCounterUI.UpdateGoldCounter();
    }

    /// <summary>
    /// Покупает предмет "зелье" и спавнит его в свободной ячейке
    /// </summary>
    private void BuyPotion()
    {
        // Вычесть деньги за зелье у игрока
        LevelProgress.CurrentGoldAmount -= LevelProgress.CurrentPotionBuyCost;

        // Умножить цену покупки зелья на множитель предмета из LevelProgress
        LevelProgress.CurrentPotionBuyCost *= LevelProgress.PotionCostMultiplier;

        // Обновить счетчик стоимости зелья
        ItemCostCounterUI.UpdatePotionCostUI();

        // Заспавнить зелье в первой свободной ячейке
        _itemsSpawner.SpawnItem(1, ItemTypes.Items.Potion);

        // Обновить счетчик золота у игрока
        PlayerGoldCounterUI.UpdateGoldCounter();
    }

    /// <summary>
    /// Находит меч по заданному тиру и возвращает его. Иначе возвращает null
    /// </summary>
    /// <param name="itemTier">Тир меча для поиска</param>
    /// <returns>Item</returns>
    private Sword GetSwordByTier(int itemTier)
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

    /// <summary>
    /// Обновляет стоимость всех предметов
    /// </summary>
    private void UpdateItemsCosts()
    {
        UpdateSwordCost();
        UpdateArmourCost();
        UpdatePotionCost();

        // Обновить счетчик стоимости зелья в UI
        ItemCostCounterUI.UpdateItemsCostUI();
    }

    /// <summary>
    /// Обновляет стоимость меча
    /// </summary>
    private void UpdateSwordCost()
    {
        // Обновляем текущую стоимость меча в LevelProgress
        LevelProgress.CurrentSwordBuyCost = GetSwordByTier(LevelProgress.CurrentSwordTierToBuy).GetComponent<Item>().Cost;
    }

    /// <summary>
    /// Обновляет стоимость брони
    /// </summary>
    private void UpdateArmourCost()
    {
        // Обновляем текущую стоимость брони в LevelProgress
        LevelProgress.CurrentArmourBuyCost = GetArmourByTier(LevelProgress.CurrentArmourTierToBuy).GetComponent<Item>().Cost;
    }

    /// <summary>
    /// Обновляет стоимость зелья
    /// </summary>
    private void UpdatePotionCost()
    {
        // Обновляем текущую стоимость зелья в LevelProgress
        LevelProgress.CurrentPotionBuyCost = GetPotionByTier(LevelProgress.CurrentPotionTierToBuy).GetComponent<Item>().Cost;
    }
    #endregion
}
