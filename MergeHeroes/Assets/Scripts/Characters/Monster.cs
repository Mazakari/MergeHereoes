// Roman Branov 28.07.2021

using System;
using UnityEngine;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    #region VARIABLES
    [SerializeField] private string _monsterName = "SetMonsterName";
    [SerializeField] private float _monsterHp = 2f;// ������� �������� ������ � �������
    [SerializeField] private float _monsterDamage = 5f;// ���� �������
    /// <summary>
    /// ���� �������
    /// </summary>
    public float MonsterDamage { get { return _monsterDamage; } }
    [SerializeField] private float _monsterGoldPerKill = 1f;// ������� �������� ������ �� �������� �������
    public float MonsterGoldPerKill { get { return _monsterGoldPerKill; } set { _monsterGoldPerKill = value; } }

    private Slider _monsterHpBar = null;// ������ �� HP ��� ������� �� �����
    private Text _monsterNameText = null;// ������ �� ��������� � ������� ��� ����������� ����� �������
    private Text _monsterHealthStatusText = null;// ������ �� ��������� �� �������� �������� �������

    /// <summary>
    /// ������� ���������� ��� ������ �������
    /// </summary>
    public static event EventHandler OnMonsterDead;
    #endregion

    #region UNITY Methods

    private void Awake()
    {
        SetMonsterHealthBar();
    }
    // Start is called before the first frame update
    void Start()
    {
        _monsterHpBar.maxValue = _monsterHp;
        _monsterHpBar.value = _monsterHpBar.maxValue;

        _monsterNameText.text = $"{_monsterName}";
        _monsterHealthStatusText.text = $"{_monsterHpBar.value} / {_monsterHpBar.maxValue}";
    }

    #endregion

    #region PUBLIC Methods
    /// <summary>
    /// ��������� �������� ����� �������. ���� �� ��������, �� ���������� ������� OnMonsterDead
    /// </summary>
    /// <param name="damage">��������, �� ������� ����� �������� HP �������</param>
    public void GetDamage(float damage)
    {
        if (_monsterHp - damage > 0)
        {
            _monsterHp -= damage;

            //��������� �� ��� �������
            _monsterHpBar.value -= damage;

            // ��������� ������ �������� �������
            _monsterHealthStatusText.text = $"{_monsterHpBar.value} / {_monsterHpBar.maxValue}";
        }
        else
        {
            // ������ ����, ���������� �������
            OnMonsterDead?.Invoke(this, EventArgs.Empty);
        }
    }
    #endregion

    #region PRIVATE Methods
    /// <summary>
    /// ������� HP ��� ������� � ����� ��� �����, ������� �������� � ��������� ������ �� ���
    /// </summary>
    private void SetMonsterHealthBar()
    {
        Slider[] hpBars = FindObjectsOfType<Slider>();

        for (int i = 0; i < hpBars.Length; i++)
        {
            if (hpBars[i].transform.Find("MonsterNameText"))
            {
                _monsterHpBar = hpBars[i];
                _monsterNameText = _monsterHpBar.transform.Find("MonsterNameText").GetComponent<Text>();
                _monsterHealthStatusText = _monsterHpBar.transform.Find("Fill Area").transform.Find("MonsterHealthStatusText").GetComponent<Text>();
            }
        }
    }
    #endregion
}




