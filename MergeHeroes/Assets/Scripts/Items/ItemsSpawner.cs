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
    /// <param name="itemType">Тип предмет для спавна</param>
    public void SpawnItem(int count, ItemTypes.Items itemType)
    {
        // Ограничиваем максимальное количество предметов для спавна количеством доступных ячеек инвентаря
        Mathf.Clamp(count, 1, Inventory.Slots.Count);

        ItemSlot slotToSpawn;

        for (int i = 0; i < count; i++)
        {
            slotToSpawn = FindEmptySlot();

            if (slotToSpawn)
            {
                switch (itemType)
                {
                    case ItemTypes.Items.Sword:
                        SpawnSword(slotToSpawn);
                        break;

                    case ItemTypes.Items.Armour:
                        SpawnArmour(slotToSpawn);
                        break;

                    case ItemTypes.Items.Potion:
                        SpawnPotion(slotToSpawn);
                        break;

                    default:
                        Debug.Log("No Such Item Type Found!");
                        break;
                }
            }
            else
            {
                Debug.Log("No empty slots available!");
                return;
            }
        }
    }
    
    /// <summary>
    /// Находит первый свободный слот инвентаря и возвращает его. Если свободных слотов нет, то возвращает null
    /// </summary>
    /// <returns></returns>
    public static ItemSlot FindEmptySlot()
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

    #region PRIVATE Methods
    /// <summary>
    /// Спавнит меч в указанной ячейке инвентаря
    /// </summary>
    /// <param name="slotToSpawn">Ячейка инвентаря, в которой нужно заспавнить меч</param>
    private void SpawnSword(ItemSlot slotToSpawn)
    {
        // Спавним предмет в ячейке
        Sword sword = Instantiate(gameSettingsSO.Swords[0], slotToSpawn.transform).GetComponent<Sword>();

        // Делаем предмет ребенком ячейки
        sword.transform.SetParent(slotToSpawn.transform, true);
        sword.transform.position = slotToSpawn.transform.position;

        // Записываем ID родительской ячейки для предмета
        sword.GetComponent<Item>().ParentSlotId = slotToSpawn.ItemSlotID;

        // Отмечаем ячейку как занятую
        slotToSpawn.IsOccupied = true;
    }

    /// <summary>
    /// Спавнит броню в указанной ячейке инвентаря
    /// </summary>
    /// <param name="slotToSpawn">Ячейка инвентаря, в которой нужно заспавнить броню</param>
    private void SpawnArmour(ItemSlot slotToSpawn)
    {
        // Спавним предмет в ячейке
        Armour armour = Instantiate(gameSettingsSO.Armour[0], slotToSpawn.transform).GetComponent<Armour>();

        // Делаем предмет ребенком ячейки
        armour.transform.SetParent(slotToSpawn.transform, true);
        armour.transform.position = slotToSpawn.transform.position;

        // Записываем ID родительской ячейки для предмета
        armour.GetComponent<Item>().ParentSlotId = slotToSpawn.ItemSlotID;

        // Отмечаем ячейку как занятую
        slotToSpawn.IsOccupied = true;
    }

    /// <summary>
    /// Спавнит зелье в указанной ячейке инвентаря
    /// </summary>
    /// <param name="slotToSpawn">Ячейка инвентаря, в которой нужно заспавнить зелье</param>
    private void SpawnPotion(ItemSlot slotToSpawn)
    {
        // Спавним предмет в ячейке
        Potion potion = Instantiate(gameSettingsSO.Potions[0], slotToSpawn.transform).GetComponent<Potion>();

        // Делаем предмет ребенком ячейки
        potion.transform.SetParent(slotToSpawn.transform, true);
        potion.transform.position = slotToSpawn.transform.position;

        // Записываем ID родительской ячейки для предмета
        potion.GetComponent<Item>().ParentSlotId = slotToSpawn.ItemSlotID;

        // Отмечаем ячейку как занятую
        slotToSpawn.IsOccupied = true;
    }
    #endregion




}
