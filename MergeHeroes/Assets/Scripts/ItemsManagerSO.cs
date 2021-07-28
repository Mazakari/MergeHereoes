// Roman Baranov 28.07.2021

using UnityEngine;

[CreateAssetMenu(fileName = "ItemsManagerSO", menuName = "Items Manager SO", order = 3)]
public class ItemsManagerSO : ScriptableObject
{
    #region VARIABLES
    // Текущий тир предмета для спавна
    private static int _currentTierToBuy = 1;
    /// <summary>
    /// Текущий тир предмета для спавна
    /// </summary>
    public static int CurrentTierToBuy { get { return _currentTierToBuy; } set { _currentTierToBuy = value; } }

    private static float _currentItemBuyCost = 1f;// Текущая стоимость предмета для покупки
    /// <summary>
    /// Текущая стоимость предмета для покупки
    /// </summary>
    public static float CurrentItemBuyCost { get { return _currentItemBuyCost; } set { _currentItemBuyCost = value; } }

    private static float _itemCostMultiplier = 1.2f;
    /// <summary>
    /// Множитель роста стоимости предмета
    /// </summary>
    public static float ItemCostMultiplier { get { return _itemCostMultiplier; } }
    #endregion

}
