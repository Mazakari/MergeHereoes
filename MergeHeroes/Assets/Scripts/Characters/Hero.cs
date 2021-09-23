// Roman Baranov 21.07.202

using System;
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

    [SerializeField] private float _health = 100f;
    /// <summary>
    /// Текущий показатель здоровья героя
    /// </summary>
    public float Health { get { return _health; } }

    private Slider _heroHpBar = null;// Ссылка на HP бар монстра на сцене

    private Text _heroHealthStatusText = null;// Ссылка на компонент со статусом здоровья героя

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


    /// <summary>
    /// Событие вызывается при смерти героя
    /// </summary>
    public static event EventHandler OnHeroDead;
    #endregion

    #region UNITY Methods
    private void Awake()
    {
        _heroStatsUI = FindObjectOfType<HeroStatsUI>();
        SetHeroHealthBar();
    }

    private void Start()
    {
        _heroHpBar.maxValue = _health;
        _heroHpBar.value = _heroHpBar.maxValue;

        _heroHealthStatusText.text = $"{_heroHpBar.value} / {_heroHpBar.maxValue}";
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

    /// <summary>
    /// Обновляет значение жизни героя. Если он погибает, то вызывается событие OnHeroDead
    /// </summary>
    /// <param name="damage">значение, на которое нужно уменшить HP героя</param>
    public void GetDamage(float damage)
    {
        // Обнуляю урон, если он отрицательный после вычитания показателя брони
        float diminishedDamage = damage - _armour;
        if (diminishedDamage < 0)
        {
            diminishedDamage = 0.0f;
        }

        if (_health - diminishedDamage > 0)
        {
            _health -= diminishedDamage;

            //Обновляем хп бар монстра
            _heroHpBar.value -= diminishedDamage;

            // Обновляем статус здоровья героя
            _heroHealthStatusText.text = $"{_heroHpBar.value} / {_heroHpBar.maxValue}";
        }
        else
        {
            Debug.Log($"Hero health = {_health}");
            // Герой умер, отправляем событие
            OnHeroDead?.Invoke(this, EventArgs.Empty);
            //Debug.Log($"Hero {gameObject.name} is Dead");
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
        //_heroStatsUI.UpdateGoldPerKill(LevelProgress.GoldPerKill);
        //Debug.Log("Why change GPK here? TODO");
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

        //Debug.Log($"New hero health amount{_health}");
    }

    /// <summary>
    /// Находит HP бар героя и сохраняет ссылку на него
    /// </summary>
    private void SetHeroHealthBar()
    {
        Slider[] hpBars = FindObjectsOfType<Slider>();

        for (int i = 0; i < hpBars.Length; i++)
        {
            if (hpBars[i].gameObject.name == "HeroHPBar")
            {
                _heroHpBar = hpBars[i];
                _heroHealthStatusText = _heroHpBar.transform.Find("Fill Area").transform.Find("HeroHealthStatusText").GetComponent<Text>();
                return;
            }
        }
    }
    #endregion
}


