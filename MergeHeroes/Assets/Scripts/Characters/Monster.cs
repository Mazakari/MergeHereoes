// Roman Branov 28.07.2021

using System;
using UnityEngine;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    #region VARIABLES
    [SerializeField] private string _monsterName = "SetMonsterName";
    [SerializeField] private float _monsterHp = 2f;// Базовое значение жизней у монстра
    [SerializeField] private float _monsterGoldPerKill = 1f;// Базовое значение золота за убийство монстра
    public float MonsterGoldPerKill { get { return _monsterGoldPerKill; } set { _monsterGoldPerKill = value; } }

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
        SetMonsterHealthBar();
    }
    // Start is called before the first frame update
    void Start()
    {
        _monsterHpBar.maxValue = _monsterHp;
        _monsterHpBar.value = _monsterHpBar.maxValue;

        _monsterNameText.text = $"{_monsterName}";
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
            // Монстр умер, отправляем событие
            OnMonsterDead?.Invoke(this, EventArgs.Empty);
        }
    }
    #endregion

    #region PRIVATE Methods
    /// <summary>
    /// Находит HP бар монстра и текст его имени и сохраняет ссылки на них
    /// </summary>
    private void SetMonsterHealthBar()
    {
        Slider[] hpBars = FindObjectsOfType<Slider>();

        for (int i = 0; i < hpBars.Length; i++)
        {
            if (hpBars[i].transform.Find("MonsterNameText"))
            {
                _monsterHpBar = hpBars[i];
                _monsterNameText = _monsterHpBar.transform.Find("MonsterNameText").GetComponent<Text>();
            }
        }
    }
    #endregion
}




