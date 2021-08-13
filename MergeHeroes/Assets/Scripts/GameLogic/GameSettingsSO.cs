// Roman Baranov 20.07.2021

using UnityEngine;

[CreateAssetMenu(fileName ="GameSettingsSO", menuName ="Game Settings SO", order = 1)]
public class GameSettingsSO : ScriptableObject
{
    #region VARIABLES
    [SerializeField] private GameObject[] _items = null;

    /// <summary>
    /// Коллекция предметов для мержа
    /// </summary>
    public GameObject[] Items { get { return _items; } }

    [SerializeField] private GameObject[] _heroes = null;

    /// <summary>
    /// Коллекция героев
    /// </summary>
    public GameObject[] Heroes { get { return _heroes; } }

    [SerializeField] private GameObject[] _monsters = null;

    /// <summary>
    /// Коллекция монстров
    /// </summary>
    public GameObject[] Monsters { get { return _monsters; } }
    #endregion
}
