// Roman Baranov 27.07.2021

using UnityEngine;
using UnityEngine.UI;

public class BuyItem : MonoBehaviour
{
    #region VARIABLES
    private Button _buyItemButton = null;// ������ �� ������ ������� ��������

    private ItemContainerManager _itemContainerManager = null;// ������ �� ������ ��� ������ ������ ������ ��������

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
    /// ������������ ������� �������� ��� ������� �� ������
    /// </summary>
    private void BuyNewItem()
    {
        // ��������� ��������� �������� � ������ ������
        if (ItemsManagerSO.CurrentItemBuyCost <= PlayerSettingsSO.CurrentGoldAmount && !ItemContainerManager.InventoryFull)
        {
            // ������� ��������� �������� �� ��������� ������
            PlayerSettingsSO.CurrentGoldAmount -= ItemsManagerSO.CurrentItemBuyCost;

            // ��������� ��������� �������� �� ���������
            ItemsManagerSO.CurrentItemBuyCost *= ItemsManagerSO.ItemCostMultiplier;

            // �������� ������� ������ ������
            PlayerGoldCounter.UpdateGoldCounter();

            // �������� ������� ��������� ��������
            ItemCostCounter.UpdateUtemCost();

            //���������� 1 �������
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
