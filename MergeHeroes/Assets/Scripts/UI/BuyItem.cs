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

        // ��������� ������� ��������� �������� � LevelProgress
        LevelProgress.CurrentSwordBuyCost = GetISwordByTier(LevelProgress.CurrentSwordTierToBuy).GetComponent<Item>().Cost;

        // �������� ������� ��������� ��������
        ItemCostCounterUI.UpdateUtemCost();
    }

    private void OnDestroy()
    {
        _buyItemButton.onClick.RemoveAllListeners();
    }
    #endregion

    #region PRIVATE Methods
    /// <summary>
    /// ������������ ������� �������� ��� ������� �� ������
    /// </summary>
    private void BuyNewItem()
    {
        // ��������� ���������� �� � ������ ������ ��� �������
        if (LevelProgress.CurrentGoldAmount >= LevelProgress.CurrentSwordBuyCost)
        {
            // ��������� ���� �� ��������� ����� � ���������
            if (ItemsSpawner.FindEmptySlot())
            {
                // ������� ������ �� ������� � ������
                LevelProgress.CurrentGoldAmount -= LevelProgress.CurrentSwordBuyCost;

                // �������� ���� ������� �� ��������� �������� �� LevelProgress
                LevelProgress.CurrentSwordBuyCost *= LevelProgress.SwordCostMultiplier;

                // �������� ������� ��������� ��������
                ItemCostCounterUI.UpdateUtemCost();

                // ���������� ������� � ������ ��������� ������
                _itemsSpawner.SpawnItem(1, ItemTypes.Items.Sword);

                // �������� ������� ������ � ������
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
    /// ������� ��� �� ��������� ���� � ���������� ���. ����� ���������� null
    /// </summary>
    /// <param name="itemTier">��� ���� ��� ������</param>
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
    /// ������� ����� �� ��������� ���� � ���������� ���. ����� ���������� null
    /// </summary>
    /// <param name="itemTier">��� ����� ��� ������</param>
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
    /// ������� ����� �� ��������� ���� � ���������� ���. ����� ���������� null
    /// </summary>
    /// <param name="itemTier">��� ����� ��� ������</param>
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
