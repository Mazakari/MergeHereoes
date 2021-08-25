// Roman Baranov 10.08.2021

using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region VARIABLES
    private Transform _inventorySlotsContainer = null;// ��������� � ������� �������� ��� ����� ���������
    private static List<ItemSlot> _slots = null;// ��������� ������ ���������
    /// <summary>
    /// ��������� ������ ���������
    /// </summary>
    public static List<ItemSlot> Slots { get { return _slots; } }

    private ItemsSpawner _itemsSpawner = null;// ������ �� ������ ��� ������ ��������� � ������� ���������
    #endregion

    #region UNITY Methods
    private void Awake()
    {
        _inventorySlotsContainer = transform.Find("InventorySlotsContainer");
        _itemsSpawner = FindObjectOfType<ItemsSpawner>();
    }

    private void Start()
    {
        SetInventorySlots();
        _itemsSpawner.SpawnItem(2, ItemTypes.Items.Sword);
        _itemsSpawner.SpawnItem(2, ItemTypes.Items.Armour);
        _itemsSpawner.SpawnItem(2, ItemTypes.Items.Potion);
    }
    #endregion

    #region PRIVATE Methods
    /// <summary>
    /// ������� ������ ����� ��������� � ����������� ������ ������ ���������� ID
    /// </summary>
    private void SetInventorySlots()
    {
        _slots = new List<ItemSlot>();

        int childsCount = _inventorySlotsContainer.childCount;

        for (int i = 0; i < childsCount; i++)
        {
            _slots.Add(_inventorySlotsContainer.GetChild(i).GetComponent<ItemSlot>());
            _slots[i].ItemSlotID = i;
        }
    }
    #endregion

}
