// Roman Baranov 22.08.2021

using System.Collections.Generic;
using UnityEngine;

public class HeroInventory : MonoBehaviour
{
    #region VARIABLES
    private Transform _heroInventorySlotsContainer = null;// ��������� � ������� �������� ��� ����� ��������� �����

    private static List<HeroItemSlot> _slots = null;// ��������� ������ ���������
    /// <summary>
    /// ��������� ������ ���������
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
    /// ������� ������ ����� ��������� � ����������� ������ ������ ���������� ID
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
