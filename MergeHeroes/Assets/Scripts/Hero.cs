// Roman Baranov 21.07.202

using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    #region VARIABLES
    [SerializeField] private float _damage = 0.1f;// ������� ���� � ������� �����

    /// <summary>
    /// ������� ���� � ������� �����
    /// </summary>
    public float Damage { get { return _damage; } }
    #endregion

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
}
