// Roman Baranov 25.07.2021

using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    #region VARIABLES
    private GameObject _itemInSlot = null;// �������, ����������� � ���� ������

    /// <summary>
    /// �������, ����������� � ���� ������
    /// </summary>
    public GameObject ItemInSlot { get { return _itemInSlot; } set { _itemInSlot = value; } }
    #endregion
}
