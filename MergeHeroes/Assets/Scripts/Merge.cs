//Roman Baranov 22.07.2021

using UnityEngine;

public class Merge : MonoBehaviour
{
    #region VARIABLE
    private ItemContainerManager _itemContainerManager = null;// Ссылка на скрипт для вызова функции спавна при мерже предметов

    private MergePanelManager _mergePanelManager = null;// Ссылка на скрипт для вызова функции очистки ячеек инвентаря при мерже

    #endregion

    #region UNITY Methods
    private void Awake()
    {
        _itemContainerManager = FindObjectOfType<ItemContainerManager>();

        _mergePanelManager = FindObjectOfType<MergePanelManager>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        DoMerge(collision);
    }
    #endregion

    #region PUBLIC Methods
    /// <summary>
    /// Обрабатывает мерж 2 одинаковых предметов
    /// </summary>
    /// <param name="collision">Коллаидер объекта с которым происходит контакт</param>
    /// <param name="spawnPoint">Место спавна предмета следующего тира</param>
    /// <param name="parent">Родительский объект в котором будет заспавнен предмет следующего тира</param>
    public void DoMerge(Collider2D collision)
    {
        if (collision.GetComponent<Item>())
        {
            Item itemToMergeWith = GetComponent<Item>();  
            Item thisItem = collision.GetComponent<Item>();

            if (thisItem.ItemTier == itemToMergeWith.ItemTier &&
                thisItem.MergeItemType == itemToMergeWith.MergeItemType &&
                TouchManager.IsMergable)
            {
                TouchManager.IsMergable = false;

                //Сохраняем слот для спавна объекта
                GameObject slotToSpawn = thisItem.OccupiedSlot;

                // Очищаем слоты для двух предметов
                _mergePanelManager.ClearSelectedSlots(itemToMergeWith, thisItem);

                // Спавним новый предмет на месте текущего
                _itemContainerManager.SpawnItem(itemToMergeWith, slotToSpawn);

                // Уменьшаем количество запавненных предметов на 1
                ItemContainerManager.SpawnedItems--;

                Destroy(collision.gameObject);
                Destroy(gameObject);
            }
            else if (thisItem.ItemTier != itemToMergeWith.ItemTier &&
                thisItem.MergeItemType == itemToMergeWith.MergeItemType &&
                TouchManager.IsMergable)
            {
                Debug.Log("Incorrect Item Tier");
                TouchManager.IsMergable = false;
                gameObject.transform.position = itemToMergeWith.StartPos;
                TouchManager.IsMergable = true;
            }
        }
        else
        {
            Debug.Log("Incorrect item to merge");
        }
    }
    #endregion
}
