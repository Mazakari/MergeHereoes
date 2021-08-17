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
        LevelProgress.CurrentItemBuyCost = GetItemByTier(LevelProgress.CurrentTierToBuy).ItemCost;

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
        if (LevelProgress.CurrentGoldAmount >= LevelProgress.CurrentItemBuyCost)
        {
            // ��������� ���� �� ��������� ����� � ���������
            if (ItemsSpawner.FindEmptySlot())
            {
                // ������� ������ �� ������� � ������
                LevelProgress.CurrentGoldAmount -= LevelProgress.CurrentItemBuyCost;

                // �������� ���� ������� �� ��������� �������� �� LevelProgress
                LevelProgress.CurrentItemBuyCost *= LevelProgress.ItemCostMultiplier;

                // �������� ������� ��������� ��������
                ItemCostCounterUI.UpdateUtemCost();

                // ���������� ������� � ������ ��������� ������
                _itemsSpawner.SpawnItem(1);

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
    /// ������� ������� �� ��������� ���� � ���������� ���. ����� ���������� null
    /// </summary>
    /// <param name="itemTier">��� ��� ������ ��������</param>
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
