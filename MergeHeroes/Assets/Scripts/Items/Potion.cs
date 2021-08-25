// Roman Baranov 22.08.2021

using UnityEngine;

public class Potion : MonoBehaviour
{
    #region VARIABLES
    [Header("Item Parameters")]
    [SerializeField] private float _bonusHpAmount = 0;
    /// <summary>
    /// Размер бонусного здоровья
    /// </summary>
    public float BonusHpAmount { get { return _bonusHpAmount; } }
    #endregion
}
