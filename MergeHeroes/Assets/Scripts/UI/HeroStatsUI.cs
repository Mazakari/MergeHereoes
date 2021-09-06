// Roman Baranov 05.08.2021

using UnityEngine;
using UnityEngine.UI;

public class HeroStatsUI : MonoBehaviour
{
    #region VARIABLES
    private Text _heroDamageText = null;// ������� ���� �����
    private Text _heroArmourText = null;// ������� ���������� ����� �����
    private Text _goldPerKillText = null;// ������� ����� ����� �� �������� �������
    #endregion

    #region UNITY Methods
    private void Awake()
    {
        _heroDamageText = transform.Find("HeroDamageText").GetComponent<Text>();
        _heroArmourText = transform.Find("HeroArmourText").GetComponent<Text>();
        _goldPerKillText = transform.Find("GoldPerKillText").GetComponent<Text>();
    }

    // Start is called before the first frame update
    void Start()
    {
        CharactersSpawner.OnMonsterSpawn += CharactersSpawner_OnMonsterSpawn;
    }
    #endregion

    #region PUBLIC Methods
    /// <summary>
    /// ��������� ������������ ���������� ����� � ����������
    /// </summary>
    /// <param name="itemDamage">����� ���� �����</param>
    public void UpdateHeroDamage(float itemDamage)
    {
        _heroDamageText.text = $"Damage: {itemDamage:F2}";
       
    }

    /// <summary>
    /// ��������� ���������� ����� �����
    /// </summary>
    /// <param name="itemArmour">����� ���������� ����� �����</param>
    public void UpdateHeroArmour(float itemArmour)
    {
        _heroArmourText.text = $"Armour: {itemArmour:F2}";
    }

    /// <summary>
    /// ��������� �������� ������ �� �������� �������
    /// </summary>
    /// <param name="goldPerKill">����� ����� ����� �� �������� �������</param>
    public void UpdateGoldPerKill(float goldPerKill)
    {
        if (_goldPerKillText != null)
        {
            _goldPerKillText.text = $"Gold per kill: {goldPerKill:F2}";
        }
    }
    #endregion


    #region PRIVATE Methods

    /// <summary>
    /// ��������� ������� ������ �� �������� �������
    /// </summary>
    private void SetGoldPerKill()
    {
        _goldPerKillText.text = $"Gold per kill: {CharactersSpawner.Monster.MonsterGoldPerKill:F2}";// TO DO � ������ update _goldPerKillText = null
    }
    #endregion

    #region EVENTS
    /// <summary>
    /// ��������� ������� ������ �� �������� ������� ��� ������ ������ �������
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void CharactersSpawner_OnMonsterSpawn(object sender, System.EventArgs e)
    {
        SetGoldPerKill();
    }
    #endregion
}
