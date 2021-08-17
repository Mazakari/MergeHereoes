// Roman Baranov 11.08.2021

using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    #region VARIABLES
    private int _itemSlotID = -1;
    /// <summary>
    /// »дентификатор €чейки инвентар€
    /// </summary>
    public int ItemSlotID { get { return _itemSlotID; } set { _itemSlotID = value; } }

    private bool _isOccupied = false;
    /// <summary>
    /// «ан€та ли €чейка предметом
    /// </summary>
    public bool IsOccupied { get { return _isOccupied; } set { _isOccupied = value; } }
    #endregion

    #region EVENTS
    /// <summary>
    /// ƒелает предмет ребенком текущей €чейки и устанавливает IsOccupied в true
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            //  эшируем компонент предмета в переменную
            Item itemBeingDragged = eventData.pointerDrag.GetComponent<Item>();

            // ѕровер€ем зан€та ли €чейка, в которую хотим положить предмет
            if (!_isOccupied)
            {
                // ѕровер€ем отличаетс€ ли €чейка, в которую упал предмет от исходной €чейки предмета
                if (FindItemParentSlot(itemBeingDragged).ItemSlotID != _itemSlotID)
                {
                    PutItemInSlot(itemBeingDragged);
                }
            }
            else if(_isOccupied)
            {
                Item thisItem = GetThisItemSlotChild();

                // ѕровер€ем есть ли в текущей €чейке предмет
                if (thisItem)
                {
                    // ѕровер€ем, совпадают ли типы и тиры предметов
                    if (thisItem.CurItemType == itemBeingDragged.CurItemType && thisItem.ItemTier == itemBeingDragged.ItemTier)
                    {
                        // ≈сли да, то мержим их
                        Merge(itemBeingDragged, thisItem);
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
    /// ”станавливает предмет в €чейку инвентар€
    /// </summary>
    /// <param name="Item">ѕредмет дл€ расположени€ в €чейке</param>
    private void PutItemInSlot(Item item)
    {
        // ќсвобождаем предыдущую €чейку предмета (isOccupied = false)
        FindItemParentSlot(item).IsOccupied = false;

        // ƒелаем предмет ребенком текущей €чейки и обнул€ем позицию предмета
        item.gameObject.transform.SetParent(gameObject.transform, true);
        item.gameObject.transform.position = gameObject.transform.position;

        // ќтмечаем текущую €чейку как зан€тую
        _isOccupied = true;

        // ћен€ем Parent ID у предмета на новый
        item.ParentSlotId = ItemSlotID;
    }

    /// <summary>
    /// ¬озвращает предмет в свою исходную €чейку
    /// </summary>
    /// <param name="item">ѕредмет, который нужно вернуть в свою исходную €чейку</param>
    private void ResetItemPosition(Item item)
    {
        item.gameObject.transform.position = FindItemParentSlot(item).gameObject.transform.position;
    }

    /// <summary>
    /// Ќаходит родительскую €чейку дл€ заданного предмета и возвращает ее. 
    /// ≈сли €чейка не найдена, возвращает null
    /// </summary>
    /// <param name="item">ѕредмет, родительскую €чейку которого наужно найти</param>
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
    /// ¬озвращает ребенка текущей €чейки инвентар€ типа Item. ≈сли ребенка нет, то возвращает null
    /// </summary>
    /// <returns>Item</returns>
    private Item GetThisItemSlotChild()
    {
        // ѕровер€ем есть ли у текущей €чейки инвентар€ дети и возвращаем первого, если они есть.
        // »наче возвращаем null
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
    /// ”дал€ет 2 одинаковых предмета и спавнит новый в этой €чейке
    /// </summary>
    /// <param name="itemBeingDragged">ѕредмет, который перетащили на эту €чейку</param>
    /// <param name="thisItemSlotItem">ѕредмет, наход€щийс€ в этой €чейке</param>
    private void Merge(Item itemBeingDragged, Item thisItemSlotItem)
    {
        // ќсвобождаем €чейки у обоих предметов
        FindItemParentSlot(itemBeingDragged).IsOccupied = false;
        _isOccupied = false;

        // —павним предмет тиром выше в €чейке предмета, на который перетащили и делаем эту €чейку родителем предмета тиром выше
        SpawnNextTierItem(thisItemSlotItem.ItemTier);

        // ”дал€ем оба предмета
        Destroy(itemBeingDragged.gameObject);
        Destroy(thisItemSlotItem.gameObject);
    }

    /// <summary>
    /// Ќаходит предмет на 1 тир выше указанного и спавнит предмет тиром выше в этой €чейке
    /// </summary>
    /// <param name="currentItemTier">“екущий тир предмета</param>
    private void SpawnNextTierItem(int currentItemTier)
    {
        for (int i = 0; i < ItemsSpawner.gameSettingsSO.Items.Length; i++)
        {
            // »щем предмет на 1 тир выше текущего
            if (ItemsSpawner.gameSettingsSO.Items[i].GetComponent<Item>().ItemTier == currentItemTier + 1)
            {
                // —павним предмет в этой €чейке
                Item nextTieriIem = Instantiate(ItemsSpawner.gameSettingsSO.Items[i], transform).GetComponent<Item>();

                // ƒелаем предмет ребенком €чейки
                nextTieriIem.transform.SetParent(transform, true);
                nextTieriIem.transform.position = transform.position;

                // «аписываем ID родительской €чейки дл€ предмета
                nextTieriIem.ParentSlotId = _itemSlotID;

                // ќтмечаем €чейку как зан€тую
                _isOccupied = true;

                return;
            }
           
        }

        // ≈сли такой предмет не был найден, то спавним предмет последнего существующего тира
        // —павним предмет в этой €чейке
        Item item = Instantiate(ItemsSpawner.gameSettingsSO.Items[currentItemTier], transform).GetComponent<Item>();

        // ƒелаем предмет ребенком €чейки
        item.transform.SetParent(transform, true);
        item.transform.position = transform.position;

        // «аписываем ID родительской €чейки дл€ предмета
        item.ParentSlotId = _itemSlotID;

        // ќтмечаем €чейку как зан€тую
        _isOccupied = true;

        Debug.Log($"T{currentItemTier + 1} item not exist!");
    }
    #endregion
}
