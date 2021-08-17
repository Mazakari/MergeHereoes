// Roman Baranov 11.08.2021

using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    #region VARIABLES
    private int _itemSlotID = -1;
    /// <summary>
    /// Идентификатор ячейки инвентаря
    /// </summary>
    public int ItemSlotID { get { return _itemSlotID; } set { _itemSlotID = value; } }

    private bool _isOccupied = false;
    /// <summary>
    /// Занята ли ячейка предметом
    /// </summary>
    public bool IsOccupied { get { return _isOccupied; } set { _isOccupied = value; } }
    #endregion

    #region EVENTS
    /// <summary>
    /// Делает предмет ребенком текущей ячейки и устанавливает IsOccupied в true
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            // Кэшируем компонент предмета в переменную
            Item itemBeingDragged = eventData.pointerDrag.GetComponent<Item>();

            // Проверяем занята ли ячейка, в которую хотим положить предмет
            if (!_isOccupied)
            {
                // Проверяем отличается ли ячейка, в которую упал предмет от исходной ячейки предмета
                if (FindItemParentSlot(itemBeingDragged).ItemSlotID != _itemSlotID)
                {
                    PutItemInSlot(itemBeingDragged);
                }
            }
            else if(_isOccupied)
            {
                Item thisItem = GetThisItemSlotChild();

                // Проверяем есть ли в текущей ячейке предмет
                if (thisItem)
                {
                    // Проверяем не один ли и тот же это предмет
                    if (thisItem.ParentSlotId != itemBeingDragged.ParentSlotId)
                    {
                        // Проверяем, совпадают ли типы и тиры предметов
                        if (thisItem.CurItemType == itemBeingDragged.CurItemType && thisItem.ItemTier == itemBeingDragged.ItemTier)
                        {
                            // Если да, то мержим их
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
    /// Устанавливает предмет в ячейку инвентаря
    /// </summary>
    /// <param name="Item">Предмет для расположения в ячейке</param>
    private void PutItemInSlot(Item item)
    {
        // Переключаем флаг IsDragging предмета в false
        item.GetComponent<DragDrop>().IsDragging = false;

        // Освобождаем предыдущую ячейку предмета (isOccupied = false)
        FindItemParentSlot(item).IsOccupied = false;

        // Делаем предмет ребенком текущей ячейки и обнуляем позицию предмета
        item.gameObject.transform.SetParent(gameObject.transform, true);
        item.gameObject.transform.position = gameObject.transform.position;

        // Отмечаем текущую ячейку как занятую
        _isOccupied = true;

        // Меняем Parent ID у предмета на новый
        item.ParentSlotId = ItemSlotID;
    }

    /// <summary>
    /// Возвращает предмет в свою исходную ячейку
    /// </summary>
    /// <param name="item">Предмет, который нужно вернуть в свою исходную ячейку</param>
    private void ResetItemPosition(Item item)
    {
        item.gameObject.transform.position = FindItemParentSlot(item).gameObject.transform.position;
    }

    /// <summary>
    /// Находит родительскую ячейку для заданного предмета и возвращает ее. 
    /// Если ячейка не найдена, возвращает null
    /// </summary>
    /// <param name="item">Предмет, родительскую ячейку которого наужно найти</param>
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
    /// Возвращает ребенка текущей ячейки инвентаря типа Item. Если ребенка нет, то возвращает null
    /// </summary>
    /// <returns>Item</returns>
    private Item GetThisItemSlotChild()
    {
        // Проверяем есть ли у текущей ячейки инвентаря дети и возвращаем первого, если они есть.
        // Иначе возвращаем null
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
    /// Удаляет 2 одинаковых предмета и спавнит новый в этой ячейке
    /// </summary>
    /// <param name="itemBeingDragged">Предмет, который перетащили на эту ячейку</param>
    /// <param name="thisItemSlotItem">Предмет, находящийся в этой ячейке</param>
    private void Merge(Item itemBeingDragged, Item thisItemSlotItem)
    {
        // Освобождаем ячейки у обоих предметов
        FindItemParentSlot(itemBeingDragged).IsOccupied = false;
        _isOccupied = false;

        // Спавним предмет тиром выше в ячейке предмета, на который перетащили и делаем эту ячейку родителем предмета тиром выше
        SpawnNextTierItem(thisItemSlotItem.ItemTier);

        // Удаляем оба предмета
        Destroy(itemBeingDragged.gameObject);
        Destroy(thisItemSlotItem.gameObject);
    }

    /// <summary>
    /// Находит предмет на 1 тир выше указанного и спавнит предмет тиром выше в этой ячейке
    /// </summary>
    /// <param name="currentItemTier">Текущий тир предмета</param>
    private void SpawnNextTierItem(int currentItemTier)
    {
        for (int i = 0; i < ItemsSpawner.gameSettingsSO.Items.Length; i++)
        {
            // Ищем предмет на 1 тир выше текущего
            if (ItemsSpawner.gameSettingsSO.Items[i].GetComponent<Item>().ItemTier == currentItemTier + 1)
            {
                // Спавним предмет в этой ячейке
                Item nextTieriIem = Instantiate(ItemsSpawner.gameSettingsSO.Items[i], transform).GetComponent<Item>();

                // Делаем предмет ребенком ячейки
                nextTieriIem.transform.SetParent(transform, true);
                nextTieriIem.transform.position = transform.position;

                // Записываем ID родительской ячейки для предмета
                nextTieriIem.ParentSlotId = _itemSlotID;

                // Отмечаем ячейку как занятую
                _isOccupied = true;

                return;
            }
           
        }

        // Если такой предмет не был найден, то спавним предмет последнего существующего тира
        // Спавним предмет в этой ячейке
        Item item = Instantiate(ItemsSpawner.gameSettingsSO.Items[currentItemTier], transform).GetComponent<Item>();

        // Делаем предмет ребенком ячейки
        item.transform.SetParent(transform, true);
        item.transform.position = transform.position;

        // Записываем ID родительской ячейки для предмета
        item.ParentSlotId = _itemSlotID;

        // Отмечаем ячейку как занятую
        _isOccupied = true;

        Debug.Log($"T{currentItemTier + 1} item not exist!");
    }
    #endregion
}
