// Roman Baranov 11.08.2021

using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    #region VARIABLES
    private int _itemSlotID = -1;
    /// <summary>
    /// ������������� ������ ���������
    /// </summary>
    public int ItemSlotID { get { return _itemSlotID; } set { _itemSlotID = value; } }

    private bool _isOccupied = false;
    /// <summary>
    /// ������ �� ������ ���������
    /// </summary>
    public bool IsOccupied { get { return _isOccupied; } set { _isOccupied = value; } }
    #endregion

    #region EVENTS
    /// <summary>
    /// ������ ������� �������� ������� ������ � ������������� IsOccupied � true
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            // �������� ��������� �������� � ����������
            Item itemBeingDragged = eventData.pointerDrag.GetComponent<Item>();

            // ��������� ������ �� ������, � ������� ����� �������� �������
            if (!_isOccupied)
            {
                // ��������� ���������� �� ������, � ������� ���� ������� �� �������� ������ ��������
                if (FindItemParentSlot(itemBeingDragged).ItemSlotID != _itemSlotID)
                {
                    PutItemInSlot(itemBeingDragged);
                }
            }
            else if(_isOccupied)
            {
                Item thisItem = GetThisItemSlotChild();

                // ��������� ���� �� � ������� ������ �������
                if (thisItem)
                {
                    // ��������� �� ���� �� � ��� �� ��� �������
                    if (thisItem.ParentSlotId != itemBeingDragged.ParentSlotId)
                    {
                        // ���������, ��������� �� ���� � ���� ���������
                        if (thisItem.CurItemType == itemBeingDragged.CurItemType && thisItem.ItemTier == itemBeingDragged.ItemTier)
                        {
                            // ���� ��, �� ������ ��
                            Merge(itemBeingDragged, thisItem);
                        }
                        else
                        {
                            ResetItemPosition(itemBeingDragged);
                        }
                    }
                    else
                    {
                        ResetItemPosition(itemBeingDragged);
                    }
                }
            }
            else
            {
                ResetItemPosition(itemBeingDragged);
            }
        }
        Debug.Log("OnDrop");
    }
    #endregion

    #region PRIVATE Methods
    /// <summary>
    /// ������������� ������� � ������ ���������
    /// </summary>
    /// <param name="Item">������� ��� ������������ � ������</param>
    private void PutItemInSlot(Item item)
    {
        // ����������� ���� IsDragging �������� � false
        item.GetComponent<DragDrop>().IsDragging = false;

        // ����������� ���������� ������ �������� (isOccupied = false)
        FindItemParentSlot(item).IsOccupied = false;

        // ������ ������� �������� ������� ������ � �������� ������� ��������
        item.gameObject.transform.SetParent(gameObject.transform, true);
        item.gameObject.transform.position = gameObject.transform.position;

        // �������� ������� ������ ��� �������
        _isOccupied = true;

        // ������ Parent ID � �������� �� �����
        item.ParentSlotId = ItemSlotID;
    }

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

    /// <summary>
    /// ���������� ������� ������� ������ ��������� ���� Item. ���� ������� ���, �� ���������� null
    /// </summary>
    /// <returns>Item</returns>
    private Item GetThisItemSlotChild()
    {
        // ��������� ���� �� � ������� ������ ��������� ���� � ���������� �������, ���� ��� ����.
        // ����� ���������� null
        if (transform.childCount > 0)
        {
            return transform.GetChild(0).GetComponent<Item>();
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// ������� 2 ���������� �������� � ������� ����� � ���� ������
    /// </summary>
    /// <param name="itemBeingDragged">�������, ������� ���������� �� ��� ������</param>
    /// <param name="thisItemSlotItem">�������, ����������� � ���� ������</param>
    private void Merge(Item itemBeingDragged, Item thisItemSlotItem)
    {
        // ����������� ������ � ����� ���������
        FindItemParentSlot(itemBeingDragged).IsOccupied = false;
        _isOccupied = false;

        // ������� ������� ����� ���� � ������ ��������, �� ������� ���������� � ������ ��� ������ ��������� �������� ����� ����
        SpawnNextTierItem(thisItemSlotItem.ItemTier);

        // ������� ��� ��������
        Destroy(itemBeingDragged.gameObject);
        Destroy(thisItemSlotItem.gameObject);
    }

    /// <summary>
    /// ������� ������� �� 1 ��� ���� ���������� � ������� ������� ����� ���� � ���� ������
    /// </summary>
    /// <param name="currentItemTier">������� ��� ��������</param>
    private void SpawnNextTierItem(int currentItemTier)
    {
        for (int i = 0; i < ItemsSpawner.gameSettingsSO.Items.Length; i++)
        {
            // ���� ������� �� 1 ��� ���� ��������
            if (ItemsSpawner.gameSettingsSO.Items[i].GetComponent<Item>().ItemTier == currentItemTier + 1)
            {
                // ������� ������� � ���� ������
                Item nextTieriIem = Instantiate(ItemsSpawner.gameSettingsSO.Items[i], transform).GetComponent<Item>();

                // ������ ������� �������� ������
                nextTieriIem.transform.SetParent(transform, true);
                nextTieriIem.transform.position = transform.position;

                // ���������� ID ������������ ������ ��� ��������
                nextTieriIem.ParentSlotId = _itemSlotID;

                // �������� ������ ��� �������
                _isOccupied = true;

                return;
            }
           
        }

        // ���� ����� ������� �� ��� ������, �� ������� ������� ���������� ������������� ����
        // ������� ������� � ���� ������
        Item item = Instantiate(ItemsSpawner.gameSettingsSO.Items[currentItemTier], transform).GetComponent<Item>();

        // ������ ������� �������� ������
        item.transform.SetParent(transform, true);
        item.transform.position = transform.position;

        // ���������� ID ������������ ������ ��� ��������
        item.ParentSlotId = _itemSlotID;

        // �������� ������ ��� �������
        _isOccupied = true;

        Debug.Log($"T{currentItemTier + 1} item not exist!");
    }
    #endregion
}
