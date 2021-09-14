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
    void Start() => _buyItemButton.onClick.AddListener(BuyNewItem);
    private void OnDestroy() => _buyItemButton.onClick.RemoveAllListeners();
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
    #endregion
}
