// Roman Baranov 21.07.202

using System;
using UnityEngine;
using UnityEngine.UI;

public class Hero : MonoBehaviour
{
    #region VARIABLES
    private float _damage = 0.5f;
    /// <summary>
    /// ������� ���� �����
    /// </summary>
    public float Damage { get { return _damage; } }

    private float _armour = 0.5f;
    /// <summary>
    /// ������� ���������� ����� �����
    /// </summary>
    public float Armour { get { return _armour; } }

    [SerializeField] private float _health = 100f;
    /// <summary>
    /// ������� ���������� �������� �����
    /// </summary>
    public float Health { get { return _health; } }

    private Slider _heroHpBar = null;// ������ �� HP ��� ������� �� �����

    private Text _heroHealthStatusText = null;// ������ �� ��������� �� �������� �������� �����

    private HeroStatsUI _heroStatsUI = null;// ������ �� ������ � ����������� ����������� ����������� �����

    private Sword _equippedSword = null;
    /// <summary>
    /// ���, ������������� � ������ ������ �� �����
    /// </summary>
    public Sword EquippedSword { get { return _equippedSword; } }

    private Armour _equippedArmour = null;
    /// <summary>
    /// �����, ������������� � ������ ������ �� �����
    /// </summary>
    public Armour EquippedArmour { get { return _equippedArmour; } }

    private Potion _equippedPotion = null;
    /// <summary>
    /// �����, ������������� � ������ ������ �� �����
    /// </summary>
    public Potion EquippedPotion { get { return _equippedPotion; } }


    /// <summary>
    /// ������� ���������� ��� ������ �����
    /// </summary>
    public static event EventHandler OnHeroDead;
    #endregion

    #region UNITY Methods
    private void Awake()
    {
        _heroStatsUI = FindObjectOfType<HeroStatsUI>();
        SetHeroHealthBar();
    }

    private void Start()
    {
        _heroHpBar.maxValue = _health;
        _heroHpBar.value = _heroHpBar.maxValue;

        _heroHealthStatusText.text = $"{_heroHpBar.value} / {_heroHpBar.maxValue}";
    }
    #endregion

    #region PUBLIC Methods
    /// <summary>
    /// ��������� ���������� �� �������� ������� �������� �� �����
    /// </summary>
    /// <param name="item">����� �������</param>
    public void EquipItem(Item item)
    {
        if (item.GetComponent<Sword>())
        {
            EquipSword(item);
        }
        else if (item.GetComponent<Armour>())
        {
            EquipArmour(item);
        }
        else if (item.GetComponent<Potion>())
        {
            EquipPotion(item);
        }
        else
        {
            Debug.Log($"This item type cannot be found");
        }
       
    }

    /// <summary>
    /// ��������� �������� ����� �����. ���� �� ��������, �� ���������� ������� OnHeroDead
    /// </summary>
    /// <param name="damage">��������, �� ������� ����� �������� HP �����</param>
    public void GetDamage(float damage)
    {
        // ������� ����, ���� �� ������������� ����� ��������� ���������� �����
        float diminishedDamage = damage - _armour;
        if (diminishedDamage < 0)
        {
            diminishedDamage = 0.0f;
        }

        if (_health - diminishedDamage > 0)
        {
            _health -= diminishedDamage;

            //��������� �� ��� �������
            _heroHpBar.value -= diminishedDamage;

            // ��������� ������ �������� �����
            _heroHealthStatusText.text = $"{_heroHpBar.value} / {_heroHpBar.maxValue}";
        }
        else
        {
            Debug.Log($"Hero health = {_health}");
            // ����� ����, ���������� �������
            OnHeroDead?.Invoke(this, EventArgs.Empty);
            //Debug.Log($"Hero {gameObject.name} is Dead");
        }
    }
    #endregion

    #region PRIVATE Methods
    /// <summary>
    /// ��������� ���� �����
    /// </summary>
    /// <param name="item">����� ��� �����</param>
    private void EquipSword(Item item)
    {
        // ��������� ������������� ��� �� �����
        _equippedSword = item.GetComponent<Sword>();

        // ��������� ���� �����
        _damage = _equippedSword.Damage;

        // ��������� ���� ����� � UI
        _heroStatsUI.UpdateHeroDamage(_damage);

        // ��������� GoldPerKill � UI ??????????
        //_heroStatsUI.UpdateGoldPerKill(LevelProgress.GoldPerKill);
        //Debug.Log("Why change GPK here? TODO");
    }

    /// <summary>
    /// ��������� ���������� ����� �����
    /// </summary>
    /// <param name="item">����� �����</param>
    private void EquipArmour(Item item)
    {
        // ��������� ������������� ����� �� �����
        _equippedArmour = item.GetComponent<Armour>();

        // ��������� ���������� ����� �����
        _armour = _equippedArmour.ArmourAmount;

        // ��������� ���������� ����� � UI
        _heroStatsUI.UpdateHeroArmour(_armour);
    }

    /// <summary>
    /// ��������� ����� �����
    /// </summary>
    /// <param name="item">����� �����</param>
    private void EquipPotion(Item item)
    {
        // ��������� ������������� ����� �� �����
        _equippedPotion = item.GetComponent<Potion>();

        // ��������� ���������� ����� �����
        _health += _equippedPotion.BonusHpAmount;

        //Debug.Log($"New hero health amount{_health}");
    }

    /// <summary>
    /// ������� HP ��� ����� � ��������� ������ �� ����
    /// </summary>
    private void SetHeroHealthBar()
    {
        Slider[] hpBars = FindObjectsOfType<Slider>();

        for (int i = 0; i < hpBars.Length; i++)
        {
            if (hpBars[i].gameObject.name == "HeroHPBar")
            {
                _heroHpBar = hpBars[i];
                _heroHealthStatusText = _heroHpBar.transform.Find("Fill Area").transform.Find("HeroHealthStatusText").GetComponent<Text>();
                return;
            }
        }
    }
    #endregion
}


