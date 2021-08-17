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
            //  эшируем компонент предмета в переменную
            Item itemBeingDragged = eventData.pointerDrag.GetComponent<Item>();

            //  эшируем геро€ в переменную
            Hero hero = FindObjectOfType<Hero>();

            // // ≈сли предмет в €чейке тиром ниже перет€гиваемого
            if (hero.EquippedItem.ItemTier < itemBeingDragged.ItemTier)
            {
                // ќчищаем €чейку предмета в инвентаре
                FindItemParentSlot(itemBeingDragged).IsOccupied = false;

                // то экипируем предмет на геро€
                hero.EquipItem(itemBeingDragged);
            }
            else
            {
                // ≈сли предмет в €чейке тиром выше перет€гиваемого, то возвращаем предмет в свою €чейку
                ResetItemPosition(itemBeingDragged);
            }
        }
        Debug.Log("OnDrop");
    }
    #endregion

    #region PRIVATE Methods
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
    #endregion

}
