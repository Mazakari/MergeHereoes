// Roman Baranov 22.08.2021

using UnityEngine;

public class Sword : MonoBehaviour
{
    #region VARIABLES
    [Header("Item Parameters")]
    [SerializeField] private float _damage = 0;
    /// <summary>
    /// Урон оружия
    /// </summary>
    public float Damage { get { return _damage; } }
    #endregion
}
