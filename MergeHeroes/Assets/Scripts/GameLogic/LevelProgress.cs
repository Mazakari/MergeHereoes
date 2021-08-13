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

    // ������� ��� �������� ��� ������
    private static int _currentTierToBuy = 1;
    /// <summary>
    /// ������� ��� �������� ��� ������
    /// </summary>
    public static int CurrentTierToBuy { get { return _currentTierToBuy; } set { _currentTierToBuy = value; } }

    private static float _currentItemBuyCost = 1f;// ������� ��������� �������� ��� �������
    /// <summary>
    /// ������� ��������� �������� ��� �������
    /// </summary>
    public static float CurrentItemBuyCost { get { return _currentItemBuyCost; } set { _currentItemBuyCost = value; } }

    private static float _itemCostMultiplier = 1.2f;
    /// <summary>
    /// ��������� ����� ��������� ��������
    /// </summary>
    public static float ItemCostMultiplier { get { return _itemCostMultiplier; } }
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
