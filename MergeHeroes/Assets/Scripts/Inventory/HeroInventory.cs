// Roman Baranov 22.08.2021

using System.Collections.Generic;
using UnityEngine;

public class HeroInventory : MonoBehaviour
{
    #region VARIABLES
    private Transform _heroInventorySlotsContainer = null;// Контейнер в котором хранятся все слоты инвентаря героя

    private static List<HeroItemSlot> _slots = null;// Коллекция слотов инвентаря
    /// <summary>
    /// Коллекция слотов инвентаря
    /// </summary>
    public static List<HeroItemSlot> Slots { get { return _slots; } }
    #endregion

    #region UNITY Methods
    private void Awake()
    {
        _heroInventorySlotsContainer = transform.Find("HeroInventorySlotsContainer");
    }

    private void Start()
    {
        SetInventorySlots();
    }
    #endregion

    #region PRIVATE Methods
    /// <summary>
    /// Создает список ячеек инвентаря и присваивает каждой ячейке уникальный ID
    /// </summary>
    private void SetInventorySlots()
    {
        _slots = new List<HeroItemSlot>();

        int childsCount = _heroInventorySlotsContainer.childCount;

        for (int i = 0; i < childsCount; i++)
        {
            _slots.Add(_heroInventorySlotsContainer.GetChild(i).GetComponent<HeroItemSlot>());
            _slots[i].ItemSlotID = 10 + i;
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
