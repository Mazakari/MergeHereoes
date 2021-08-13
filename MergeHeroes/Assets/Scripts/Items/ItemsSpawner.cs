// Roman Baranov 13.08.2021

using UnityEngine;

public class ItemsSpawner : MonoBehaviour
{
    #region VARIABLES
    public static GameSettingsSO gameSettingsSO = null;
    #endregion

    #region UNITY Methods
    private void Awake()
    {
        gameSettingsSO = Resources.Load<GameSettingsSO>("ScriptableObjects/GameSettingsSO");
    }
    #endregion

    #region PUBLIC Methods
    /// <summary>
    /// Спавнит предмет в свободной ячейке инвентаря
    /// </summary>
    /// <param name="count">Количество предметов для спавна</param>
    public void SpawnItem(int count)
    {
        // Ограничиваем максимальное количество предметов для спавна количеством доступных ячеек инвентаря
        Mathf.Clamp(count, 1, Inventory.Slots.Count);

        ItemSlot slotToSpawn;

        for (int i = 0; i < count; i++)
        {
            slotToSpawn = FindEmptySlot();

            if (slotToSpawn)
            {
                // Спавним предмет в ячейке
                Item item = Instantiate(gameSettingsSO.Items[1], slotToSpawn.transform).GetComponent<Item>();

                // Делаем предмет ребенком ячейки
                item.transform.SetParent(slotToSpawn.transform, true);
                item.transform.position = slotToSpawn.transform.position;

                // Записываем ID родительской ячейки для предмета
                item.ParentSlotId = slotToSpawn.ItemSlotID;

                // Отмечаем ячейку как занятую
                slotToSpawn.IsOccupied = true;
            }
            else
            {
                Debug.Log("No empty slots available!");
                return;
            }
        }
    }
    #endregion

    #region PRIVATE Methods
    /// <summary>
    /// Находит первый свободный слот инвентаря и возвращает его. Если свободных слотов нет, то возвращает null
    /// </summary>
    /// <returns></returns>
    private ItemSlot FindEmptySlot()
    {
        for (int i = 0; i < Inventory.Slots.Count; i++)
        {
            if (!Inventory.Slots[i].IsOccupied)
            {
                return Inventory.Slots[i];
            }
        }

        return null;
    }
    #endregion




}
