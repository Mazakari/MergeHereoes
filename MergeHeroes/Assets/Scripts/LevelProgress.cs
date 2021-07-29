// Roman Baranov 28.07.2021

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LevelProgress : MonoBehaviour
{
    #region VARIABLES
    private float _heroAttackDelay = 1.0f;
    private IEnumerator _damageCoroutine = null;
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
