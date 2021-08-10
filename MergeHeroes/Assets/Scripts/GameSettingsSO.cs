// Roman Baranov 20.07.2021

using UnityEngine;

[CreateAssetMenu(fileName ="GameSettingsSO", menuName ="Game Settings SO", order = 1)]
public class GameSettingsSO : ScriptableObject
{
    #region VARIABLES
    [SerializeField] private GameObject[] _items = null;

    /// <summary>
    /// ��������� ��������� ��� �����
    /// </summary>
    public GameObject[] Items { get { return _items; } }

    [SerializeField] private GameObject[] _heroes = null;

    /// <summary>
    /// ��������� ������
    /// </summary>
    public GameObject[] Heroes { get { return _heroes; } }

    [SerializeField] private GameObject[] _monsters = null;

    /// <summary>
    /// ��������� ��������
    /// </summary>
    public GameObject[] Monsters { get { return _monsters; } }

    // ������� ��� �������� ��� ������
    private static int _currentTierToBuy = 1;
    /// <summary>
    /// ������� ��� �������� ��� ������
    /// </summary>
    public static int CurrentTierToBuy { get { return _currentTierToBuy; } set { _currentTierToBuy = value; } }

    private static float _currentItemBuyCost = 1f;// ������� ��������� �������� ��� �������
    /// <summary>
    /// ������� ��������� �������� ��� �������
    /// </summary>
    public static float CurrentItemBuyCost { get { return _currentItemBuyCost; } set { _currentItemBuyCost = value; } }

    private static float _itemCostMultiplier = 1.2f;
    /// <summary>
    /// ��������� ����� ��������� ��������
    /// </summary>
    public static float ItemCostMultiplier { get { return _itemCostMultiplier; } }
    #endregion
}
