// Roman Baranov 28.07.2021

using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSettingsSO", menuName = "Player Settings SO", order = 2)]
public class PlayerSettingsSO : ScriptableObject
{
    #region VARIABLES
    private static float _currentGoldAmount = 10;
    /// <summary>
    /// Текущее количество золота у игрока
    /// </summary>
    public static float CurrentGoldAmount { get { return _currentGoldAmount; } set { _currentGoldAmount = value; } }
    #endregion
}
