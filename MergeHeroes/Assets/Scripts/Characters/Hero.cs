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
    #endregion

    #region UNITY Methods
   
    #endregion

    #region PRIVATE Methods
    /// <summary>
    /// ��������� ���������� �� �������� ������� �������� �� �����
    /// </summary>
    /// <param name="item">����� �������</param>
    private void EquipItem(Item item)
    {
        // ��������� ������� ��� ������� �������� � UI

        // ��������� ���� �����

        // ����������� ���� ��������

        // ��������� ����� ����� � UI

        // ���������� �������
        //Destroy(item.gameObject);
    }

    // ������������ ���������� �������� �� �����
    private void OnTriggerStay2D(Collider2D collision)
    {
        // ���� ��� ��������������� �������� �� ����� ������, �� ������� ���� �������
        // ����� ���������� ������� � ���� ����
    }
    #endregion
   
}

    
