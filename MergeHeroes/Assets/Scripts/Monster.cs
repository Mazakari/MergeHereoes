// Roman Branov 28.07.2021

using System;
using UnityEngine;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    #region VARIABLES
    [SerializeField] private string _monsterName = "SetMonsterName";
    [SerializeField] private float _monsterHp = 2f;// Базовое значение жизней у монстра
    [SerializeField] private float _monsterGold = 1f;// Базовое значение золота за убийство монстра

    private Slider _monsterHpBar = null;// Ссылка на HP бар монстра на сцене
    private Text _monsterNameText = null;// Ссылка на компонент с текстом для отображения имени монстра

    /// <summary>
    /// Событие вызывается при смерти монстра
    /// </summary>
    public static event EventHandler OnMonsterDead;
    #endregion

    #region UNITY Methods

    private void Awake()
    {
        _monsterHpBar = FindObjectOfType<Slider>();
        _monsterNameText = _monsterHpBar.transform.Find("MonsterNameText").GetComponent<Text>();

    }
    // Start is called before the first frame update
    void Start()
    {
        _monsterHp *= MonstersManagerSO.MonsterHpMultiplyer;
        _monsterHpBar.maxValue = _monsterHp;
        _monsterHpBar.value = _monsterHpBar.maxValue;

        _monsterNameText.text = $"{_monsterName}";
       
        _monsterGold *= MonstersManagerSO.MonsterGoldMultiplier;
    }

    #endregion

    #region PUBLIC Methods
    /// <summary>
    /// Обновляет значение жизни монстра. Если он погибает, то вызывается событие OnMonsterDead
    /// </summary>
    /// <param name="damage">значение, на которое нужно уменшить HP монстра</param>
    public void UpdateHP(float damage)
    {
        if (_monsterHp - damage > 0)
        {
            _monsterHp -= damage;

            //Обновляем хп бар монстра
            _monsterHpBar.value -= damage;
        }
        else
        {
            // Начисляем игроку золото
            PlayerSettingsSO.CurrentGoldAmount += _monsterGold;

            // Обновляем счетчик золота игрока
            PlayerGoldCounter.UpdateGoldCounter();

            // Монстр умер, отправляем событие
            OnMonsterDead?.Invoke(this, EventArgs.Empty);
        }
    }
    }
    #endregion

