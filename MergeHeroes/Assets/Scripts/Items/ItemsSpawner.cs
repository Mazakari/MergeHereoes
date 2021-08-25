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
    /// ������� ������� � ��������� ������ ���������
    /// </summary>
    /// <param name="count">���������� ��������� ��� ������</param>
    /// <param name="itemType">��� ������� ��� ������</param>
    public void SpawnItem(int count, ItemTypes.Items itemType)
    {
        // ������������ ������������ ���������� ��������� ��� ������ ����������� ��������� ����� ���������
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
    /// ������� ������ ��������� ���� ��������� � ���������� ���. ���� ��������� ������ ���, �� ���������� null
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
    /// ������� ��� � ��������� ������ ���������
    /// </summary>
    /// <param name="slotToSpawn">������ ���������, � ������� ����� ���������� ���</param>
    private void SpawnSword(ItemSlot slotToSpawn)
    {
        // ������� ������� � ������
        Sword sword = Instantiate(gameSettingsSO.Swords[0], slotToSpawn.transform).GetComponent<Sword>();

        // ������ ������� �������� ������
        sword.transform.SetParent(slotToSpawn.transform, true);
        sword.transform.position = slotToSpawn.transform.position;

        // ���������� ID ������������ ������ ��� ��������
        sword.GetComponent<Item>().ParentSlotId = slotToSpawn.ItemSlotID;

        // �������� ������ ��� �������
        slotToSpawn.IsOccupied = true;
    }

    /// <summary>
    /// ������� ����� � ��������� ������ ���������
    /// </summary>
    /// <param name="slotToSpawn">������ ���������, � ������� ����� ���������� �����</param>
    private void SpawnArmour(ItemSlot slotToSpawn)
    {
        // ������� ������� � ������
        Armour armour = Instantiate(gameSettingsSO.Armour[0], slotToSpawn.transform).GetComponent<Armour>();

        // ������ ������� �������� ������
        armour.transform.SetParent(slotToSpawn.transform, true);
        armour.transform.position = slotToSpawn.transform.position;

        // ���������� ID ������������ ������ ��� ��������
        armour.GetComponent<Item>().ParentSlotId = slotToSpawn.ItemSlotID;

        // �������� ������ ��� �������
        slotToSpawn.IsOccupied = true;
    }

    /// <summary>
    /// ������� ����� � ��������� ������ ���������
    /// </summary>
    /// <param name="slotToSpawn">������ ���������, � ������� ����� ���������� �����</param>
    private void SpawnPotion(ItemSlot slotToSpawn)
    {
        // ������� ������� � ������
        Potion potion = Instantiate(gameSettingsSO.Potions[0], slotToSpawn.transform).GetComponent<Potion>();

        // ������ ������� �������� ������
        potion.transform.SetParent(slotToSpawn.transform, true);
        potion.transform.position = slotToSpawn.transform.position;

        // ���������� ID ������������ ������ ��� ��������
        potion.GetComponent<Item>().ParentSlotId = slotToSpawn.ItemSlotID;

        // �������� ������ ��� �������
        slotToSpawn.IsOccupied = true;
    }
    #endregion




}
