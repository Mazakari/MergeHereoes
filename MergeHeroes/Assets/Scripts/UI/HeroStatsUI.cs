// Roman Baranov 05.08.2021

using UnityEngine;
using UnityEngine.UI;

public class HeroStatsUI : MonoBehaviour
{
    #region VARIABLES
    [Header("Hero Item Section")]
    [SerializeField] private Sprite _defaultItemTier = null;// ������ ��� �������� �� ��������
    public Sprite DefaultItemSprite { get { return _defaultItemTier; } }

    [SerializeField] private Image _heroItemImage = null;// ����������� �������� ������������ �������� �� �����
    [SerializeField] private Text _heroItemTierText = null;// �������� �������� ������������ �������� �� �����

    [Space(3)]
    [Header("Hero Stats Section")]
    [SerializeField] private Text _heroDamageText = null;// ������� ���� �����
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
    /// <param name="itemSprite">������ ������ ��������</param>
    /// <param name="itemTier">��� ������ ��������</param>
    /// <param name="itemDamage">����� ���� �����</param>
    /// <param name="heroGoldPerKill">����� ����� ����� �� �������� �������</param>
    public void UpdateHeroStats(Sprite itemSprite, int itemTier, float itemDamage, float goldPerKill)
    {
        _heroItemImage.sprite = itemSprite;
        _heroItemTierText.text = $"Tier {itemTier}";

        _heroDamageText.text = $"Damage: {itemDamage.ToString("F2")}";
        _goldPerKillText.text = $"Gold per kill: {goldPerKill.ToString("F2")}";
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
