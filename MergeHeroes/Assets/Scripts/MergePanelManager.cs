// Roman Baranov 25.07.2021

using System.Collections.Generic;
using UnityEngine;

public class MergePanelManager : MonoBehaviour
{
    #region VARIABLES
    private List<GameObject> _inventorySlotList = null;
    /// <summary>
    /// —писок €чеек инвентар€ на панели мержа
    /// </summary>
    public List<GameObject> InventorySlotList { get { return _inventorySlotList; } }

    private GameObject _inventoryCellPrefab = null;

    private static int _inventorySize = 8;
    /// <summary>
    /// ћаксимальный размер инвентар€ на панели мержа
    /// </summary>
    public static int InventorySize { get { return _inventorySize; } }

    #endregion

    #region UNITY Methods
    private void Awake()
    {
        _inventoryCellPrefab = Resources.Load<GameObject>("Prefabs/InventorySlot");
        InitInventory();
        MarkUnusedSlots();
    }
    #endregion

    #region PUBLIC Methods
    /// <summary>
    /// ќбнул€ет €чейки инвентар€ дл€ двух выбранных предметов 
    /// </summary>
    /// <param name="item1"></param>
    /// <param name="item2"></param>
    public void ClearSelectedSlots(Item item1, Item item2)
    {
        for (int i = 0; i < _inventorySlotList.Count; i++)
        {
            if (_inventorySlotList[i].GetComponent<InventorySlot>().ItemInSlot == item1.gameObject)
            {
                _inventorySlotList[i].GetComponent<InventorySlot>().ItemInSlot = null;
                item1.OccupiedSlot = null;
            }

            if (_inventorySlotList[i].GetComponent<InventorySlot>().ItemInSlot == item2.gameObject)
            {
                _inventorySlotList[i].GetComponent<InventorySlot>().ItemInSlot = null;
                item2.OccupiedSlot = null;
            }
        }

        MarkUnusedSlots();

    }

    /// <summary>
    /// DEBUG ѕоказывает содержимое €чеек инвентар€ на панели мержа в консоли
    /// </summary>
    public void PrintInventory()
    {
        for (int i = 0; i < _inventorySlotList.Count; i++)
        {
            Debug.Log($"slot: {_inventorySlotList[i].name} with item: {_inventorySlotList[i].GetComponent<InventorySlot>().ItemInSlot}");
        }
    }

    /// <summary>
    /// DEBUG ќтмечает пустые слоты красным, а свободные зеленым
    /// </summary>
    public void MarkUnusedSlots()
    {
        for (int i = 0; i < _inventorySlotList.Count; i++)
        {
            if (_inventorySlotList[i].GetComponent<InventorySlot>().ItemInSlot == null)
            {
                _inventorySlotList[i].GetComponent<SpriteRenderer>().color = Color.green;
                
            }

            else
            {
                _inventorySlotList[i].GetComponent<SpriteRenderer>().color = Color.red;
            }
        }
    }
    #endregion

    #region PRIVATE Methods
    /// <summary>
    /// —павнит €чейки инвентар€ на панели мержа и заполн€ет список всех €чеек
    /// </summary>
    private void InitInventory()
    {
        Vector2 camWorldPos = Camera.main.ViewportToWorldPoint(Camera.main.transform.position);
        Transform parent = transform.Find("InventorySlotsContainer");

        float xOffset = _inventoryCellPrefab.transform.lossyScale.x / 1.23f;
        float yOffset = _inventoryCellPrefab.transform.lossyScale.y / 1.7f;

        parent.transform.localPosition = new Vector2(camWorldPos.x + xOffset, camWorldPos.y + yOffset);

        _inventorySlotList = new List<GameObject>();

        int x = 0;
        int y = 0;

        float cellSize = 1.3f;

        for (int i = 0; i < _inventorySize; i++)
        {
            GameObject cell = Instantiate(_inventoryCellPrefab, parent.localPosition, Quaternion.identity, parent);
            cell.transform.localPosition = new Vector2(x * cellSize, y * cellSize);

            _inventorySlotList.Add(cell);

            x++;
            if (x == _inventorySize / 2)
            {
                x = 0;
                y++;
            }
        }
    }

    
    #endregion





}
