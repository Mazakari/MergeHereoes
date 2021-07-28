// Roman Baranov 25.07.2021

using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    #region VARIABLES
    private GameObject _itemInSlot = null;// Предмет, находящийся в этой ячейке

    /// <summary>
    /// Предмет, находящийся в этой ячейке
    /// </summary>
    public GameObject ItemInSlot { get { return _itemInSlot; } set { _itemInSlot = value; } }
    #endregion
}
