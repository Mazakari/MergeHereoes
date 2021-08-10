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
