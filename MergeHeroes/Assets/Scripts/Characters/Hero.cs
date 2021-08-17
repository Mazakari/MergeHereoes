// Roman Baranov 21.07.202

using UnityEngine;
using UnityEngine.UI;

public class Hero : MonoBehaviour
{
    #region VARIABLES
    private float _damage = 0.1f;// Базовый урон героя

    /// <summary>
    /// Базовый урон героя
    /// </summary>
    public float Damage { get { return _damage; } }

    private HeroStatsUI _heroStatsUI = null;// Ссылка на скрипт с управлением отображения статистикой героя

    private Item _equippedItem = null;
    /// <summary>
    /// Предмет, экипированный в данный момент на герое
    /// </summary>
    public Item EquippedItem { get { return _equippedItem; } }
    #endregion

    #region UNITY Methods
    private void Awake()
    {
        _heroStatsUI = FindObjectOfType<HeroStatsUI>();
    }
    private void Start()
    {
        EquipItem(ItemsSpawner.gameSettingsSO.Items[0].GetComponent<Item>());
    }
    #endregion

    #region PUBLIC Methods
    /// <summary>
    /// Обновляем информацию по текущему одетому предмету на герое
    /// </summary>
    /// <param name="item">Новый предмет</param>
    public void EquipItem(Item item)
    {
        // Обновляем экипированный предмет на герое
        _equippedItem = item;

        // Обновляем урон героя
        _damage = item.ItemDamage;
        // Освобождаем слот предмета

        // Обновляем статы героя в UI
        _heroStatsUI.UpdateHeroStats(item.GetComponent<Image>().sprite, item.ItemTier, _damage, LevelProgress.GoldPerKill);

        if (_equippedItem.ItemTier > 0)
        {
            // Уничтожаем предмет
            Destroy(item.gameObject);
        }
    }
    #endregion
}

    
