// Roman Baranov 20.07.2021

using UnityEngine;

[CreateAssetMenu(fileName ="GameSettingsSO", menuName ="Game Settings SO", order = 1)]
public class GameSettingsSO : ScriptableObject
{
    #region VARIABLES
    [SerializeField] private GameObject[] _swords = null;

    /// <summary>
    /// ��������� ����� ��� �����
    /// </summary>
    public GameObject[] Swords { get { return _swords; } }

    [SerializeField] private GameObject[] _armour = null;

    /// <summary>
    /// ��������� ����� ��� �����
    /// </summary>
    public GameObject[] Armour { get { return _armour; } }

    [SerializeField] private GameObject[] _potions = null;

    /// <summary>
    /// ��������� ����� ��� �����
    /// </summary>
    public GameObject[] Potions { get { return _potions; } }


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
    #endregion
}
