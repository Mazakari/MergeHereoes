// Roman Baranov 28.07.2021

using UnityEngine;

[CreateAssetMenu(fileName = "MonstersManagerSO", menuName = "Monsters Manager SO", order = 3)]
public class MonstersManagerSO : ScriptableObject
{
    #region VARIABLES
    private static float _monsterHpMultiplier = 1.5f;
    /// <summary>
    /// Множитель HP монстра
    /// </summary>
    public static float MonsterHpMultiplyer { get { return _monsterHpMultiplier; } }


    private static float _monsterGoldMultiplyer = 1.2f;
    /// <summary>
    /// Множитель золота монстра
    /// </summary>
    public static float MonsterGoldMultiplier { get { return _monsterGoldMultiplyer; } }

    #endregion
}
