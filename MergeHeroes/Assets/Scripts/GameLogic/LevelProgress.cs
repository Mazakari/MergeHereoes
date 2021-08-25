// Roman Baranov 28.07.2021

using System.Collections;
using UnityEngine;

public class LevelProgress : MonoBehaviour
{
    #region VARIABLES
    private float _heroAttackDelay = 1.0f;// Задержка урона между атаки героя

    private IEnumerator _damageCoroutine = null;// Курутина для постоянного нанесения урона монстру

    private static float _currentGoldAmount = 10;
    /// <summary>
    /// Текущее количество золота у игрока
    /// </summary>
    public static float CurrentGoldAmount { get { return _currentGoldAmount; } set { _currentGoldAmount = value; } }

    private static float _goldPerKill = 1;
    /// <summary>
    /// Текущее количество золота, начисляемого за убийство монстра
    /// </summary>
    public static float GoldPerKill { get { return _goldPerKill; } set { _goldPerKill = value; } }

    #region Sword
    // Текущий тир меча для спавна
    private static int _currentSwordTierToBuy = 1;
    /// <summary>
    /// Текущий тир меча для спавна
    /// </summary>
    public static int CurrentSwordTierToBuy { get { return _currentSwordTierToBuy; } set { _currentSwordTierToBuy = value; } }

    private static float _currentSwordBuyCost = 1f;// Текущая стоимость меча для покупки
    /// <summary>
    /// Текущая стоимость меча для покупки
    /// </summary>
    public static float CurrentSwordBuyCost { get { return _currentSwordBuyCost; } set { _currentSwordBuyCost = value; } }

    private static float _swordCostMultiplier = 1.1f;
    /// <summary>
    /// Множитель роста стоимости меча
    /// </summary>
    public static float SwordCostMultiplier { get { return _swordCostMultiplier; } }
    #endregion

    #region Armour
    // Текущий тир брони для спавна
    private static int _currentArmourTierToBuy = 1;
    /// <summary>
    /// Текущий тир брони для спавна
    /// </summary>
    public static int CurrentArmourTierToBuy { get { return _currentArmourTierToBuy; } set { _currentArmourTierToBuy = value; } }

    private static float _currentArmourBuyCost = 1f;// Текущая стоимость брони для покупки
    /// <summary>
    /// Текущая стоимость брони для покупки
    /// </summary>
    public static float CurrentArmourBuyCost { get { return _currentArmourBuyCost; } set { _currentArmourBuyCost = value; } }

    private static float _armourCostMultiplier = 1.1f;
    /// <summary>
    /// Множитель роста стоимости брони
    /// </summary>
    public static float ArmourCostMultiplier { get { return _armourCostMultiplier; } }
    #endregion

    #region Potion
    // Текущий тир зелья для спавна
    private static int _currentPotionTierToBuy = 1;
    /// <summary>
    /// Текущий тир зелья для спавна
    /// </summary>
    public static int CurrentPotionTierToBuy { get { return _currentPotionTierToBuy; } set { _currentPotionTierToBuy = value; } }

    private static float _currentPotionBuyCost = 1f;// Текущая стоимость зелья для покупки
    /// <summary>
    /// Текущая стоимость зелья для покупки
    /// </summary>
    public static float CurrentPotionBuyCost { get { return _currentPotionBuyCost; } set { _currentPotionBuyCost = value; } }

    private static float _potionCostMultiplier = 1.1f;
    /// <summary>
    /// Множитель роста стоимости зелья
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
    /// Курутина запускает постоянный урон по монстрам
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
