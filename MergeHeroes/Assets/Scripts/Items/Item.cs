// Roman Baranov 10.09.2021

using UnityEngine;

public class Item : MonoBehaviour
{
    #region VARIABLES
    [SerializeField] private ItemTypes.Items _type;
    /// <summary>
    /// ��� ��������
    /// </summary>
    public ItemTypes.Items Type { get { return _type; } }

    [SerializeField] private int _tier = 0;
    /// <summary>
    /// ��� ��������
    /// </summary>
    public int Tier { get { return _tier; } }

    [SerializeField] private float _cost = 1f;
    /// <summary>
    /// ��������� ��������
    /// </summary>
    public float Cost { get { return _cost; } }

    private int _parentSlotId = -1;
    /// <summary>
    /// ID ������������ ������ ���������
    /// </summary>
    public int ParentSlotId { get { return _parentSlotId; } set { _parentSlotId = value; } }

    private bool _isEquipped = false;

    /// <summary>
    /// ���������� �� ������� �� �����
    /// </summary>
    public bool IsEquipped { get { return _isEquipped; } set { _isEquipped = value; } }
    #endregion
}
