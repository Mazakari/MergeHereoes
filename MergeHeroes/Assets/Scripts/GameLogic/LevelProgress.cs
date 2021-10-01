// Roman Baranov 28.07.2021

using System.Collections;
using UnityEngine;

public class LevelProgress : MonoBehaviour
{
    #region VARIABLES
    private float _attackDelay = 1.0f;

    private IEnumerator _damageCoroutine = null;

    private static float _currentGoldAmount = 1000000f;// TO DO

    /// <summary>
    /// Player current gold amount
    /// </summary>
    public static float CurrentGoldAmount { get { return _currentGoldAmount; } set { _currentGoldAmount = value; } }

    private static float _goldPerKill = 1;
    /// <summary>
    /// Current gold per kill
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
            _damageCoroutine = DamageLoop();
            StartCoroutine(_damageCoroutine);
        }
    }
    #endregion

    #region PUBLIC Methods
    /// <summary>
    /// Reset items cost to default values
    /// </summary>
    public static void ResetResources()
    {
        _currentGoldAmount = 10f;
        _currentSwordBuyCost = 1f;
        _currentArmourBuyCost = 1f;
        _currentPotionBuyCost = 1f;
    }
    #endregion

    #region PRIVATE Methods

    /// <summary>
    /// Damage coroutine
    /// </summary>
    /// <returns>IEnumerator</returns>
    private IEnumerator DamageLoop()
    {
        while (true)
        {
            // Choose random monster from the wave
            int rnd = Random.Range(0, CharactersSpawner.Monsters.Count - 1);

            if (CharactersSpawner.Monsters != null && CharactersSpawner.Monsters.Count > 0)
            {
                // Hero damage
                float heroDamage = CharactersSpawner.Hero.Damage;

                // Damage chosen monster
                CharactersSpawner.Monsters[rnd].GetDamage(heroDamage);

                // Damage room wave health
                Level.DamageRoomWaveHealth(heroDamage);

                // Update room health in UI
                Room_UI.UpdateRoomWaveHealthInfo();
            }

            if (CharactersSpawner.Hero != null && CharactersSpawner.Monsters.Count > 0)
            {
                // Наносим урон герою этим монстром
                CharactersSpawner.Hero.GetDamage(CharactersSpawner.Monsters[rnd].MonsterDamage);
            }

            yield return new WaitForSeconds(_attackDelay);
        }

        yield return null;
    }
    #endregion
}
