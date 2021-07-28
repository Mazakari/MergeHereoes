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
    #endregion
}
