// Roman Baranov 05.08.2021

using UnityEngine;
using UnityEngine.UI;

public class HeroStatsUI : MonoBehaviour
{
    #region VARIABLES
    [Header("Hero Stats Section")]
    [SerializeField] private Text _heroDamageText = null;// ������� ���� �����
    [SerializeField] private Text _heroArmourText = null;// ������� ���������� ����� �����
    [SerializeField] private Text _goldPerKillText = null;// ������� ����� ����� �� �������� �������
    #endregion

    #region UNITY Methods
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
        _goldPerKillText.text = $"Gold per kill: {goldPerKill:F2}";
    }
    #endregion


    #region PRIVATE Methods

    /// <summary>
    /// ��������� ������� ������ �� �������� �������
    /// </summary>
    private void UpdateGoldPerKill()
    {
        _goldPerKillText.text = $"Gold per kill: {CharactersSpawner.Monster.MonsterGoldPerKill:F2}";
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
        UpdateGoldPerKill();
    }
    #endregion
}
