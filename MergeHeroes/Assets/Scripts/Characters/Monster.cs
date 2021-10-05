// Roman Branov 28.07.2021

using System;
using UnityEngine;

public class Monster : MonoBehaviour
{
    #region VARIABLES

    [SerializeField] private bool _isBoss = false;// Is this monstera boss

    //private float _monsterHp = 2f;// Base monster HP

    [SerializeField] private float _monsterDamage = 5f;

    /// <summary>
    /// Monster damage
    /// </summary>
    public float MonsterDamage { get { return _monsterDamage; } }

    /// <summary>
    /// Monster dead event
    /// </summary>
    public event EventHandler<Monster> OnMonsterDead;
    #endregion

    #region UNITY Methods
    //void Start() => SetHealth();
    #endregion

    #region PUBLIC Methods
    /// <summary>
    /// Send OnMonsterDead callback 
    /// </summary>
    public void RemoveMonster()
    {
       OnMonsterDead?.Invoke(this, this);
    }
    #endregion

    #region PRIVATE Methods
    /// <summary>
    /// Set monster health
    /// </summary>
    //private void SetHealth()
    //{
    //    if (_isBoss)
    //    {
    //        _monsterHp = Level.MaxBossHealth;
    //    }
    //    else
    //    {
    //        _monsterHp = Level.PerMonsterHealth;
    //    }

    //}
    #endregion
}




