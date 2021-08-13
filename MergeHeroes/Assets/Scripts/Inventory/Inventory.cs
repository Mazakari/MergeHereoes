// Roman Baranov 10.08.2021

using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region VARIABLES
    private Transform _inventorySlotsContainer = null;// Контейнер в котором хранятся все слоты инвентаря
    private static List<ItemSlot> _slots = null;// Коллекция слотов инвентаря
    /// <summary>
    /// Коллекция слотов инвентаря
    /// </summary>
    public static List<ItemSlot> Slots { get { return _slots; } }

    private ItemsSpawner _itemsSpawner = null;// Ссылка на скрипт для спавна предметов в ячейках инвентаря
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
    /// Создает список ячеек инвентаря и присваивает каждой ячейке уникальный ID
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
    /// DEBUG Выводит имена и ID всех существующих ячеек инвентаря в консоль
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
