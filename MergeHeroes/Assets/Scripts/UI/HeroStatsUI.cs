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
        if (_heroDamageText != null)
        {
            _heroDamageText.text = $"Damage: {itemDamage:F2}";
        }
        
       
    }

    /// <summary>
    /// ��������� ���������� ����� �����
    /// </summary>
    /// <param name="itemArmour">����� ���������� ����� �����</param>
    public void UpdateHeroArmour(float itemArmour)
    {
        if (_heroArmourText != null)
        {
            _heroArmourText.text = $"Armour: {itemArmour:F2}";
        }
       
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

    #region EVENTS
    /// <summary>
    /// ��������� ������� ������ �� �������� ������� ��� ������ ������ �������
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void CharactersSpawner_OnMonsterSpawn(object sender, System.EventArgs e) => UpdateGoldPerKill(CharactersSpawner.Monster.MonsterGoldPerKill);
    #endregion
}
