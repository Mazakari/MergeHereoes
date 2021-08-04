// Roman Baranov 21.07.202

using UnityEngine;

public class Hero : MonoBehaviour
{
    #region VARIABLES
    [SerializeField] private float _damage = 0.1f;// ������� ���� �����

    /// <summary>
    /// ������� ���� �����
    /// </summary>
    public float Damage { get { return _damage; } }

    [SerializeField] private float _goldPerKill = 0;// ������, ���������� ������ �� �������� �����
    ///// <summary>
    ///// ������, ���������� ������ �� �������� �����
    ///// </summary>
    //public float GoldPerKill { get { return _goldPerKill; } set { _goldPerKill = value; } }

    private int _currentItemTier = 1;// ������� ��� �������� ������� �� �����
    #endregion

    #region PRIVATE Methods
    /// <summary>
    /// ��������� ���������� �� �������� ������� �������� �� �����
    /// </summary>
    /// <param name="item">����� �������</param>
    private void EquipItem(Item item)
    {
        // ��������� ������� ��� ������� ��������
        _currentItemTier = item.ItemTier;

        // ��������� ���� �����
        _damage *= item.DamageMultiplyer;

        // ��������� ������ �� �������� � �������
        _goldPerKill *= item.GoldMultiplyer;
        // ����������� ���� ��������
        item.OccupiedSlot = null;

        // ���������� �������
        Destroy(item.gameObject);
    }

    // ������������ ���������� �������� �� �����
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<Item>())
        {
            Item item = collision.GetComponent<Item>();
           
            if (TouchManager.IsMergable)
            {
                // ���� ��� ��������������� �������� �� ����� ������, �� ������� ���� �������
                if (item.ItemTier > _currentItemTier)
                {
                    EquipItem(item);
                    TouchManager.IsMergable = false;
                }
                else
                {
                    // ����� ���������� ������� � ���� ����
                    item.gameObject.transform.position = item.StartPos;
                }
            }
        }
    }
    #endregion
}
