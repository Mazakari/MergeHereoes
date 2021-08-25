// Roman Baranov 28.07.2021

using System.Collections;
using UnityEngine;

public class LevelProgress : MonoBehaviour
{
    #region VARIABLES
    private float _heroAttackDelay = 1.0f;// �������� ����� ����� ����� �����

    private IEnumerator _damageCoroutine = null;// �������� ��� ����������� ��������� ����� �������

    private static float _currentGoldAmount = 10;
    /// <summary>
    /// ������� ���������� ������ � ������
    /// </summary>
    public static float CurrentGoldAmount { get { return _currentGoldAmount; } set { _currentGoldAmount = value; } }

    private static float _goldPerKill = 1;
    /// <summary>
    /// ������� ���������� ������, ������������ �� �������� �������
    /// </summary>
    public static float GoldPerKill { get { return _goldPerKill; } set { _goldPerKill = value; } }

    #region Sword
    // ������� ��� ���� ��� ������
    private static int _currentSwordTierToBuy = 1;
    /// <summary>
    /// ������� ��� ���� ��� ������
    /// </summary>
    public static int CurrentSwordTierToBuy { get { return _currentSwordTierToBuy; } set { _currentSwordTierToBuy = value; } }

    private static float _currentSwordBuyCost = 1f;// ������� ��������� ���� ��� �������
    /// <summary>
    /// ������� ��������� ���� ��� �������
    /// </summary>
    public static float CurrentSwordBuyCost { get { return _currentSwordBuyCost; } set { _currentSwordBuyCost = value; } }

    private static float _swordCostMultiplier = 1.1f;
    /// <summary>
    /// ��������� ����� ��������� ����
    /// </summary>
    public static float SwordCostMultiplier { get { return _swordCostMultiplier; } }
    #endregion

    #region Armour
    // ������� ��� ����� ��� ������
    private static int _currentArmourTierToBuy = 1;
    /// <summary>
    /// ������� ��� ����� ��� ������
    /// </summary>
    public static int CurrentArmourTierToBuy { get { return _currentArmourTierToBuy; } set { _currentArmourTierToBuy = value; } }

    private static float _currentArmourBuyCost = 1f;// ������� ��������� ����� ��� �������
    /// <summary>
    /// ������� ��������� ����� ��� �������
    /// </summary>
    public static float CurrentArmourBuyCost { get { return _currentArmourBuyCost; } set { _currentArmourBuyCost = value; } }

    private static float _armourCostMultiplier = 1.1f;
    /// <summary>
    /// ��������� ����� ��������� �����
    /// </summary>
    public static float ArmourCostMultiplier { get { return _armourCostMultiplier; } }
    #endregion

    #region Potion
    // ������� ��� ����� ��� ������
    private static int _currentPotionTierToBuy = 1;
    /// <summary>
    /// ������� ��� ����� ��� ������
    /// </summary>
    public static int CurrentPotionTierToBuy { get { return _currentPotionTierToBuy; } set { _currentPotionTierToBuy = value; } }

    private static float _currentPotionBuyCost = 1f;// ������� ��������� ����� ��� �������
    /// <summary>
    /// ������� ��������� ����� ��� �������
    /// </summary>
    public static float CurrentPotionBuyCost { get { return _currentPotionBuyCost; } set { _currentPotionBuyCost = value; } }

    private static float _potionCostMultiplier = 1.1f;
    /// <summary>
    /// ��������� ����� ��������� �����
    /// </summary>
    public static float PotionCostMultiplier { get { return _potionCostMultiplier; } }
    #endregion

    #endregion

    #region UNITY Methods
    private void Start()
    {
        if (_damageCoroutine != null)
        {
            StopCoroutine(_damageCoroutine);
        }
        else
        {
            _damageCoroutine = DamageMonsters();
            StartCoroutine(_damageCoroutine);
        }
    }
    #endregion

    #region PRIVATE Methods

    /// <summary>
    /// �������� ��������� ���������� ���� �� ��������
    /// </summary>
    /// <returns></returns>
    private IEnumerator DamageMonsters()
    {
        while (true)
        {
            if (CharactersSpawner.Monster != null)
            {
                CharactersSpawner.Monster.UpdateHP(CharactersSpawner.Hero.Damage);
            }
            yield return new WaitForSeconds(_heroAttackDelay); 
        }

        yield return null;
    }
    #endregion
}
