//Roman Baranov 22.07.2021

using UnityEngine;

public class Merge : MonoBehaviour
{
    #region VARIABLE
    private ItemContainerManager _itemContainerManager = null;// ������ �� ������ ��� ������ ������� ������ ��� ����� ���������

    private MergePanelManager _mergePanelManager = null;// ������ �� ������ ��� ������ ������� ������� ����� ��������� ��� �����

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
    /// ������������ ���� 2 ���������� ���������
    /// </summary>
    /// <param name="collision">��������� ������� � ������� ���������� �������</param>
    /// <param name="spawnPoint">����� ������ �������� ���������� ����</param>
    /// <param name="parent">������������ ������ � ������� ����� ��������� ������� ���������� ����</param>
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

                //��������� ���� ��� ������ �������
                GameObject slotToSpawn = thisItem.OccupiedSlot;

                // ������� ����� ��� ���� ���������
                _mergePanelManager.ClearSelectedSlots(itemToMergeWith, thisItem);

                // ������� ����� ������� �� ����� ��������
                _itemContainerManager.SpawnItem(itemToMergeWith, slotToSpawn);

                // ��������� ���������� ����������� ��������� �� 1
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
