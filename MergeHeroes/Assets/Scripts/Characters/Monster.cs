// Roman Branov 28.07.2021

using System;
using UnityEngine;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    #region VARIABLES
    private float _monsterHp = 2f;// Base monster HP

    [SerializeField] private float _monsterDamage = 5f;

    /// <summary>
    /// Monster damage
    /// </summary>
    public float MonsterDamage { get { return _monsterDamage; } }

    [SerializeField] private float _monsterGoldPerKill = 1f;
    /// <summary>
    /// Base monster gold per kill value
    /// </summary>
    public float MonsterGoldPerKill { get { return _monsterGoldPerKill; } set { _monsterGoldPerKill = value; } }

    /// <summary>
    /// Monster dead event
    /// </summary>
    public event EventHandler<Monster> OnMonsterDead;
    #endregion

    #region UNITY Methods
    void Start() => SetHealth();
    #endregion

    #region PUBLIC Methods
    /// <summary>
    /// Updates monster HP. If monster dies send OnMonsterDead callback 
    /// </summary>
    /// <param name="damage">Monster HP damage amount</param>
    public void GetDamage(float damage)
    {
        if (_monsterHp - damage > 0)
        {
            _monsterHp -= damage;
        }
        else
        {
            // Monster died send callback
            OnMonsterDead?.Invoke(this, this);
        }
    }
    #endregion

    #region PRIVATE Methods
    private void SetHealth()
    {
        _monsterHp = Level.PerMonsterHealth;
    }
    #endregion
}




