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
        _itemsSpawner.SpawnItem(2, ItemTypes.Items.Sword);
        _itemsSpawner.SpawnItem(2, ItemTypes.Items.Armour);
        _itemsSpawner.SpawnItem(2, ItemTypes.Items.Potion);
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
    #endregion

}
