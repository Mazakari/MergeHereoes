// Roman Baranov 10.09.2021

using UnityEngine;

public class Item : MonoBehaviour
{
    #region VARIABLES
    /// <summary>
    /// Список типов предметов
    /// </summary>
    public enum ItemType
    {
        BareHands,
        Sword_T1,
        Sword_T2,
        Sword_T3,
        Sword_T4,
        Sword_T5,
        Sword_T6,
        Sword_T7,
        Sword_T8,
        Sword_T9,
        Sword_T10,
        Sword_T11,
        Sword_T12,
        Sword_T13,
        Sword_T14,
        Sword_T15,

    }

    [Header("Item Parameters")]
    [SerializeField] private ItemType _curItemType;
    /// <summary>
    /// Тип предмета
    /// </summary>
    public ItemType CurItemType { get { return _curItemType; } }

    [SerializeField] private int _itemTier = 0;
    /// <summary>
    /// Тир предмета
    /// </summary>
    public int ItemTier { get { return _itemTier; } }

    [SerializeField] private float _itemDamage = 0;
    /// <summary>
    /// Тир предмета
    /// </summary>
    public float ItemDamage { get { return _itemDamage; } }

    [SerializeField] private float _itemCost = 1f;
    /// <summary>
    /// Стоимость предмета
    /// </summary>
    public float ItemCost { get { return _itemCost; } }

    private int _parentSlotId = -1;
    /// <summary>
    /// ID родительской ячейки инвентаря
    /// </summary>
    public int ParentSlotId { get { return _parentSlotId; } set { _parentSlotId = value; } }
    #endregion
}
