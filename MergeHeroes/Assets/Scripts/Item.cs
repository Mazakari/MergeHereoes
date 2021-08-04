// Roman Baranov 20.07.2021

using UnityEngine;

public class Item : MonoBehaviour
{
    #region VARIABLES
    [SerializeField] private int _itemTier = 0;// ��� ��������
    /// <summary>
    /// ��� ��������
    /// </summary>
    public int ItemTier { get { return _itemTier; } }

    [SerializeField] private ItemType _mergeItemType;// ��� ��������
    /// <summary>
    /// ��� ��������
    /// </summary>
    public ItemType MergeItemType { get { return _mergeItemType; } }

    /// <summary>
    /// ������ ����� ���������
    /// </summary>
    public enum ItemType
    {
        WeaponMelee,
        WeaponProjectile,
        WeaponMagic,
        Accessory,
        ArmorHead,
        ArmorChest,
        ArmorArms,
        ArmorBoots,
        ArmorShield
    }

    [SerializeField] private float _itemCost = 1.0f;

    private GameObject _occupiedSlot = null;

    /// <summary>
    /// ����, ������� ���� ���������
    /// </summary>
    public GameObject OccupiedSlot { get { return _occupiedSlot; } set { _occupiedSlot = value; } }

    private Vector2 _startPos;
    /// <summary>
    /// ��������� ���������� ��������
    /// </summary>
    public Vector2 StartPos { get { return _startPos; } set { _startPos = value; } }

    [SerializeField] private float _damageMultiplyer = 1.5f;// ��������� ����� � ������� ��������
    /// <summary>
    /// ��������� ����� � ������� ��������
    /// </summary>
    public float DamageMultiplyer { get { return _damageMultiplyer; } }

    /// <summary>
    /// ��������� ������ ��������
    /// </summary>
    [SerializeField] private float _goldMultiplyer = 1.5f;// ��������� ������ ��������
    public float GoldMultiplyer { get { return _goldMultiplyer; } }
    #endregion
}
