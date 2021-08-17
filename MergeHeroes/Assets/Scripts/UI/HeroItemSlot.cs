// Roman Baranov 16.08.2021

using UnityEngine;
using UnityEngine.EventSystems;

public class HeroItemSlot : MonoBehaviour, IDropHandler
{
    #region EVENTS
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            // �������� ��������� �������� � ����������
            Item itemBeingDragged = eventData.pointerDrag.GetComponent<Item>();

            // �������� ����� � ����������
            Hero hero = FindObjectOfType<Hero>();

            // // ���� ������� � ������ ����� ���� ���������������
            if (hero.EquippedItem.ItemTier < itemBeingDragged.ItemTier)
            {
                // ������� ������ �������� � ���������
                FindItemParentSlot(itemBeingDragged).IsOccupied = false;

                // �� ��������� ������� �� �����
                hero.EquipItem(itemBeingDragged);
            }
            else
            {
                // ���� ������� � ������ ����� ���� ���������������, �� ���������� ������� � ���� ������
                ResetItemPosition(itemBeingDragged);
            }
        }
        Debug.Log("OnDrop");
    }
    #endregion

    #region PRIVATE Methods
    /// <summary>
    /// ���������� ������� � ���� �������� ������
    /// </summary>
    /// <param name="item">�������, ������� ����� ������� � ���� �������� ������</param>
    private void ResetItemPosition(Item item)
    {
        item.gameObject.transform.position = FindItemParentSlot(item).gameObject.transform.position;
    }

    /// <summary>
    /// ������� ������������ ������ ��� ��������� �������� � ���������� ��. 
    /// ���� ������ �� �������, ���������� null
    /// </summary>
    /// <param name="item">�������, ������������ ������ �������� ������ �����</param>
    /// <returns>ItemSlot</returns>
    private ItemSlot FindItemParentSlot(Item item)
    {
        for (int i = 0; i < Inventory.Slots.Count; i++)
        {
            if (Inventory.Slots[i].ItemSlotID == item.ParentSlotId)
            {
                return Inventory.Slots[i];
            }
        }

        return null;
    }
    #endregion

}
