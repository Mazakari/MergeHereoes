// Roman Baranov 22.08.2021

using UnityEngine;

public class Armour : MonoBehaviour
{
    #region VARIABLES
    [Header("Item Parameters")]
    [SerializeField] private float _armourAmount = 0;
    /// <summary>
    /// Показатель брони
    /// </summary>
    public float ArmourAmount { get { return _armourAmount; } }
    #endregion
}
