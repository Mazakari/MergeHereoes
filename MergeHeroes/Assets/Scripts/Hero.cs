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

    private int _currentItemTier = 1;// ������� ��� �������� ������� �� �����

    private HeroStatsUI _heroStatsUI = null;// ������ �� ������ ��� ���������� UI �����

    #endregion

    #region UNITY Methods
    private void Awake()
    {
        _heroStatsUI = FindObjectOfType<HeroStatsUI>();
    }
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

        // ����������� ���� ��������
        item.OccupiedSlot = null;

        // ��������� ����� ����� � UI
        _heroStatsUI.UpdateHeroStats(item.gameObject.GetComponent<SpriteRenderer>().sprite, item.ItemTier, _damage, CharactersSpawner.Monster.MonsterGoldPerKill);

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

    
