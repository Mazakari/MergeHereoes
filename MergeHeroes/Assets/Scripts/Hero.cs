// Roman Baranov 21.07.202

using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    #region VARIABLES
    [SerializeField] private float _damagePerSecond = 0f;// Базовый урон в секунду героя
    [SerializeField] private float _goldPerSecond = 0f;// Базовый показатель золота в секунду героя

    //private int _maxHeroInventorySize = 6;
    //private List<MergeItem> _heroInverntory = null;// Инвентарь героя

    #region TO DO MERGING Section
    [SerializeField] private int _heroTier = 0;// TO DO Текущий тир героя

    [SerializeField] private HeroType _mergeHeroType;// TO DO Тип героя
    /// <summary>
    /// TO DO Тип героя
    /// </summary>
    public HeroType MergeHeroType { get { return _mergeHeroType; } }

    /// <summary>
    /// TO DO Список типов героев
    /// </summary>
    public enum HeroType
    {
        HeroMelee,
        HeroRanged,
        HeroMage,
    }

    private GameSettingsSO _gameSettingsSO = null;// TO DO Ссылка на SO с коллекцией героев для спавна
    #endregion
    #endregion
}
