// Roman Baranov 21.07.202

using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    #region VARIABLES
    [SerializeField] private float _damagePerSecond = 0f;// ������� ���� � ������� �����
    [SerializeField] private float _goldPerSecond = 0f;// ������� ���������� ������ � ������� �����

    //private int _maxHeroInventorySize = 6;
    //private List<MergeItem> _heroInverntory = null;// ��������� �����

    #region TO DO MERGING Section
    [SerializeField] private int _heroTier = 0;// TO DO ������� ��� �����

    [SerializeField] private HeroType _mergeHeroType;// TO DO ��� �����
    /// <summary>
    /// TO DO ��� �����
    /// </summary>
    public HeroType MergeHeroType { get { return _mergeHeroType; } }

    /// <summary>
    /// TO DO ������ ����� ������
    /// </summary>
    public enum HeroType
    {
        HeroMelee,
        HeroRanged,
        HeroMage,
    }

    private GameSettingsSO _gameSettingsSO = null;// TO DO ������ �� SO � ���������� ������ ��� ������
    #endregion
    #endregion
}
