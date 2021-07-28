// Roman Baranov 25.07.2021

using UnityEngine;

public class ItemContainerManager : MonoBehaviour
{
    #region VARIABLES
    private MergePanelManager _mergePanelManager = null;//  ������ �� ������ ��� ��������� ������ ����� ���������

    private GameSettingsSO _gameSettingsSO = null;// ������ �� SO � ���������� ��������� ��� ������

    private static int _spawnedItems = 0;// ���������� ������������ � ������ ������ ���������
    /// <summary>
    /// ���������� ������������ � ������ ������ ���������
    /// </summary>
    public static int SpawnedItems { get { return _spawnedItems; } set { _spawnedItems = value; } }

    //private MergeItem _mergeItem = null;// ������ �� ��������� ������� ��� ��������� ���� � ���� ��������
    #endregion

    #region UNITY Methods
    private void Awake()
    {
        _mergePanelManager = FindObjectOfType<MergePanelManager>();

        _gameSettingsSO = Resources.Load<GameSettingsSO>("ScriptableObjects/GameSettingsSO");
    }

    // Start is called before the first frame update
    void Start()
    {
        SpawnItem(2);
    }

    #endregion

    #region PUBLIC Methods
    /// <summary>
    /// ������� ������� ��� ������ ������� ������
    /// </summary>
    public void SpawnItem(int itemsCount)
    {
        if (itemsCount + _spawnedItems <= MergePanelManager.InventorySize)
        {
            for (int i = 0; i < itemsCount; i++)
            {
                GameObject slot = FindInventorySlotToSpawn();
                GameObject item = Instantiate(_gameSettingsSO.Items[0], transform.position, Quaternion.identity, transform);

                Item mt = item.GetComponent<Item>();

                mt.StartPos = slot.transform.position;
                item.transform.position = mt.StartPos;

                item.GetComponent<Item>().OccupiedSlot = slot;
                slot.GetComponent<InventorySlot>().ItemInSlot = item;

                _spawnedItems++; ;
            }
        }
        else
        {
            Debug.Log($"Incorrect items value {itemsCount}. Inventory max size is {MergePanelManager.InventorySize}");
        }
        
    }

    /// <summary>
    /// ������� ������� � ��������� ����� ���������
    /// </summary>
    /// <param name="mergeItem">������� ��� �������� ����� ������ ��������� ���</param>
    /// <param name="slotToSpawn">���� ��������� �� ������ ����� � ������� ����� ������� ���������� ����</param>
    public void SpawnItem(Item mergeItem, GameObject slotToSpawn)
    {
        GameObject nextTierItem = FindMergeItemToInstance(mergeItem);

        if (nextTierItem != null)
        {
            GameObject item = Instantiate(nextTierItem, slotToSpawn.transform.position, Quaternion.identity, transform);

            Item mt = item.GetComponent<Item>();

            mt.StartPos = slotToSpawn.transform.position;
            item.transform.position = mt.StartPos;

            mt.OccupiedSlot = slotToSpawn;
            slotToSpawn.GetComponent<InventorySlot>().ItemInSlot = item;

            _spawnedItems++;
        }
        else
        {
            Debug.Log($"Spawn failed: nextTierItem {nextTierItem}");
        }
    }

    #endregion

    #region PRIVATE Methods
    /// <summary>
    /// ���� ������� ���� �� ���� � ����� ���� ��� ������
    /// </summary>
    /// /// <param name="mergeItem">������� �������� ���� ��� ���������</param>
    /// <returns>������� ���������� ��� ������</returns>
    private GameObject FindMergeItemToInstance(Item mergeItem)
    {
        for (int i = 0; i < _gameSettingsSO.Items.Length; i++)
        {
            if (_gameSettingsSO.Items[i].GetComponent<Item>().ItemTier == mergeItem.ItemTier + 1 &&
                _gameSettingsSO.Items[i].GetComponent<Item>().MergeItemType == mergeItem.MergeItemType)
            {
                return _gameSettingsSO.Items[i];
            }
        }
        return null;
    }

    /// <summary>
    /// ������� ������ ��������� ���� ��������� �� ������ ����� � ���������� ���. ���� ���� �� ������, �� ���������� null.
    /// </summary>
    /// <returns>GameObject</returns>
    private GameObject FindInventorySlotToSpawn()
    {
        for (int i = 0; i < _mergePanelManager.InventorySlotList.Count; i++)
        {
            if (_mergePanelManager.InventorySlotList[i].GetComponent<InventorySlot>().ItemInSlot == null)
            {
                return _mergePanelManager.InventorySlotList[i];
            }
        }

        return null;
    }
    #endregion
}
