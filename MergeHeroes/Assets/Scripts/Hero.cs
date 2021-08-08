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

    private HeroStatsUI _heroStatsUI = null;

    private float _curGoldMultiplyer = 0f;
    #endregion

    private void Awake()
    {
        _heroStatsUI = FindObjectOfType<HeroStatsUI>();
    }

    private void Start()
    {
        Monster.OnMonsterDead += Monster_OnMonsterDead;
    }

   

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
        //_goldPerKill *= item.GoldMultiplyer;

        // ����������� ���� ��������
        item.OccupiedSlot = null;

        // ��������� ������� ��������� ������
        _curGoldMultiplyer = item.GoldMultiplyer;

        // ��������� ����� ����� � UI
        _heroStatsUI.UpdateHeroStats(item.gameObject.GetComponent<SpriteRenderer>().sprite, item.ItemTier, _damage, CharactersSpawner.Monster.MonsterGoldPerKill * _curGoldMultiplyer);

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

    /// <summary>
    /// ��������� ������ �� �������� ������� ������
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Monster_OnMonsterDead(object sender, System.EventArgs e)
    {
        if (_curGoldMultiplyer > 0)
        {
            // ��������� ������ ������ � ���������� �� ��������
            PlayerSettingsSO.CurrentGoldAmount += CharactersSpawner.Monster.MonsterGoldPerKill * _curGoldMultiplyer;
        }
        else
        {
            // ��������� ������ ������ � ���������� �� ��������
            PlayerSettingsSO.CurrentGoldAmount += CharactersSpawner.Monster.MonsterGoldPerKill;
        }

        // ��������� ������� ������ ������
        PlayerGoldCounterUI.UpdateGoldCounter();
    }
}

    
