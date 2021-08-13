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
    public void SpawnItem(int count)
    {
        // ������������ ������������ ���������� ��������� ��� ������ ����������� ��������� ����� ���������
        Mathf.Clamp(count, 1, Inventory.Slots.Count);

        ItemSlot slotToSpawn;

        for (int i = 0; i < count; i++)
        {
            slotToSpawn = FindEmptySlot();

            if (slotToSpawn)
            {
                // ������� ������� � ������
                Item item = Instantiate(gameSettingsSO.Items[1], slotToSpawn.transform).GetComponent<Item>();

                // ������ ������� �������� ������
                item.transform.SetParent(slotToSpawn.transform, true);
                item.transform.position = slotToSpawn.transform.position;

                // ���������� ID ������������ ������ ��� ��������
                item.ParentSlotId = slotToSpawn.ItemSlotID;

                // �������� ������ ��� �������
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
    /// ������� ������ ��������� ���� ��������� � ���������� ���. ���� ��������� ������ ���, �� ���������� null
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
