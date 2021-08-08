// Roman Branov 28.07.2021

using System;
using UnityEngine;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    #region VARIABLES
    [SerializeField] private string _monsterName = "SetMonsterName";
    [SerializeField] private float _monsterHp = 2f;// ������� �������� ������ � �������
    [SerializeField] private float _monsterGoldPerKill = 1f;// ������� �������� ������ �� �������� �������
    public float MonsterGoldPerKill { get { return _monsterGoldPerKill; } set { _monsterGoldPerKill = value; } }

    private Slider _monsterHpBar = null;// ������ �� HP ��� ������� �� �����
    private Text _monsterNameText = null;// ������ �� ��������� � ������� ��� ����������� ����� �������

    /// <summary>
    /// ������� ���������� ��� ������ �������
    /// </summary>
    public static event EventHandler OnMonsterDead;
    #endregion

    #region UNITY Methods

    private void Awake()
    {
        _monsterHpBar = FindObjectOfType<Slider>();
        _monsterNameText = _monsterHpBar.transform.Find("MonsterNameText").GetComponent<Text>();

    }
    // Start is called before the first frame update
    void Start()
    {
        _monsterHpBar.maxValue = _monsterHp;
        _monsterHpBar.value = _monsterHpBar.maxValue;

        _monsterNameText.text = $"{_monsterName}";
    }

    #endregion

    #region PUBLIC Methods
    /// <summary>
    /// ��������� �������� ����� �������. ���� �� ��������, �� ���������� ������� OnMonsterDead
    /// </summary>
    /// <param name="damage">��������, �� ������� ����� �������� HP �������</param>
    public void UpdateHP(float damage)
    {
        if (_monsterHp - damage > 0)
        {
            _monsterHp -= damage;

            //��������� �� ��� �������
            _monsterHpBar.value -= damage;
        }
        else
        {
            // ������ ����, ���������� �������
            OnMonsterDead?.Invoke(this, EventArgs.Empty);
        }
    }
}
    #endregion

