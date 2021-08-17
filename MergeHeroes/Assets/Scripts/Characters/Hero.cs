// Roman Baranov 21.07.202

using UnityEngine;
using UnityEngine.UI;

public class Hero : MonoBehaviour
{
    #region VARIABLES
    private float _damage = 0.1f;// ������� ���� �����

    /// <summary>
    /// ������� ���� �����
    /// </summary>
    public float Damage { get { return _damage; } }

    private HeroStatsUI _heroStatsUI = null;// ������ �� ������ � ����������� ����������� ����������� �����

    private Item _equippedItem = null;
    /// <summary>
    /// �������, ������������� � ������ ������ �� �����
    /// </summary>
    public Item EquippedItem { get { return _equippedItem; } }
    #endregion

    #region UNITY Methods
    private void Awake()
    {
        _heroStatsUI = FindObjectOfType<HeroStatsUI>();
    }
    private void Start()
    {
        EquipItem(ItemsSpawner.gameSettingsSO.Items[0].GetComponent<Item>());
    }
    #endregion

    #region PUBLIC Methods
    /// <summary>
    /// ��������� ���������� �� �������� ������� �������� �� �����
    /// </summary>
    /// <param name="item">����� �������</param>
    public void EquipItem(Item item)
    {
        // ��������� ������������� ������� �� �����
        _equippedItem = item;

        // ��������� ���� �����
        _damage = item.ItemDamage;
        // ����������� ���� ��������

        // ��������� ����� ����� � UI
        _heroStatsUI.UpdateHeroStats(item.GetComponent<Image>().sprite, item.ItemTier, _damage, LevelProgress.GoldPerKill);

        if (_equippedItem.ItemTier > 0)
        {
            // ���������� �������
            Destroy(item.gameObject);
        }
    }
    #endregion
}

    
