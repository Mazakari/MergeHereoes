// Roman Baranov 21.07.202

using UnityEngine;
using UnityEngine.UI;

public class Hero : MonoBehaviour
{
    #region VARIABLES
    private float _damage = 0.5f;
    /// <summary>
    /// Текущий урон героя
    /// </summary>
    public float Damage { get { return _damage; } }


    private float _armour = 0.5f;
    /// <summary>
    /// Текущий показатель брони героя
    /// </summary>
    public float Armour { get { return _armour; } }


    private float _health = 100f;
    /// <summary>
    /// Текущий показатель здоровья героя
    /// </summary>
    public float Health { get { return _health; } }


    private HeroStatsUI _heroStatsUI = null;// Ссылка на скрипт с управлением отображения статистикой героя


    private Sword _equippedSword = null;
    /// <summary>
    /// Меч, экипированный в данный момент на герое
    /// </summary>
    public Sword EquippedSword { get { return _equippedSword; } }


    private Armour _equippedArmour = null;
    /// <summary>
    /// Броня, экипированная в данный момент на герое
    /// </summary>
    public Armour EquippedArmour { get { return _equippedArmour; } }


    private Potion _equippedPotion = null;
    /// <summary>
    /// Зелье, экипированное в данный момент на герое
    /// </summary>
    public Potion EquippedPotion { get { return _equippedPotion; } }
    #endregion

    #region UNITY Methods
    private void Awake()
    {
        _heroStatsUI = FindObjectOfType<HeroStatsUI>();
    }
    #endregion

    #region PUBLIC Methods
    /// <summary>
    /// Обновляем информацию по текущему одетому предмету на герое
    /// </summary>
    /// <param name="item">Новый предмет</param>
    public void EquipItem(Item item)
    {
        if (item.GetComponent<Sword>())
        {
            EquipSword(item);
        }
        else if (item.GetComponent<Armour>())
        {
            EquipArmour(item);
        }
        else if (item.GetComponent<Potion>())
        {
            EquipPotion(item);
        }
        else
        {
            Debug.Log($"This item type cannot be found");
        }
       
    }
    #endregion

    #region PRIVATE Methods
    /// <summary>
    /// Обновляет урон героя
    /// </summary>
    /// <param name="item">Новый меч героя</param>
    private void EquipSword(Item item)
    {
        // Обновляем экипированный меч на герое
        _equippedSword = item.GetComponent<Sword>();

        // Обновляем урон героя
        _damage = _equippedSword.Damage;

        // Обновляем урон героя в UI
        _heroStatsUI.UpdateHeroDamage(_damage);

        // Обновляем GoldPerKill в UI ??????????
        _heroStatsUI.UpdateGoldPerKill(LevelProgress.GoldPerKill);
        Debug.Log("Why change GPK here? TODO");
    }

    /// <summary>
    /// Обновляет показатель брони героя
    /// </summary>
    /// <param name="item">Новая броня</param>
    private void EquipArmour(Item item)
    {
        // Обновляем экипированную броню на герое
        _equippedArmour = item.GetComponent<Armour>();

        // Обновляем показатель брони героя
        _armour = _equippedArmour.ArmourAmount;

        // Обновляем показатель брони в UI
        _heroStatsUI.UpdateHeroArmour(_armour);
    }

    /// <summary>
    /// Обновляет зелье героя
    /// </summary>
    /// <param name="item">Новое зелье</param>
    private void EquipPotion(Item item)
    {
        // Обновляем экипированную броню на герое
        _equippedPotion = item.GetComponent<Potion>();

        // Обновляем показатель брони героя
        _health += _equippedPotion.BonusHpAmount;

        Debug.Log($"New hero health amount{_health}");
    }
    #endregion
}


