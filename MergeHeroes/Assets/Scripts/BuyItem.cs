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
        // ��������� ��������� ��������, ������� ������ � ������ � ������������� ���������
        if (GameSettingsSO.CurrentItemBuyCost <= LevelProgress.CurrentGoldAmount && !ItemContainerManager.InventoryFull)
        {
            // ������� ��������� �������� �� ��������� ������
            LevelProgress.CurrentGoldAmount -= GameSettingsSO.CurrentItemBuyCost;

            // ��������� ��������� �������� �� ���������
            GameSettingsSO.CurrentItemBuyCost *= GameSettingsSO.ItemCostMultiplier;

            // �������� ������� ������ ������
            PlayerGoldCounterUI.UpdateGoldCounter();

            // �������� ������� ��������� ��������
            ItemCostCounterUI.UpdateUtemCost();

            //���������� 1 �������
            _itemContainerManager.SpawnItem(1);
        }
        else
        {
            //Debug.Log($"Not enough gold! Item cost is {GameSettingsSO.CurrentItemBuyCost}, player have {LevelProgress.CurrentGoldAmount} gold.");
            //Debug.Log($"Inventory is Full {ItemContainerManager.SpawnedItems} / {MergePanelManager.InventorySize}");
        }
        
    }

    #endregion
}
