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
        _itemsSpawner.SpawnItem(8);
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

    /// <summary>
    /// DEBUG ������� ����� � ID ���� ������������ ����� ��������� � �������
    /// </summary>
    private void PrintSlots()
    {
        for (int i = 0; i < _slots.Count; i++)
        {
            Debug.Log($"Slot {_slots[i].name} has ID {_slots[i].ItemSlotID}");
        }
    }
    #endregion

}
