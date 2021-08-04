// Roman Baranov 21.07.202

using UnityEngine;

public class Hero : MonoBehaviour
{
    #region VARIABLES
    [SerializeField] private float _damage = 0.1f;// Базовый урон героя

    /// <summary>
    /// Базовый урон героя
    /// </summary>
    public float Damage { get { return _damage; } }

    [SerializeField] private float _goldPerKill = 0;// Золото, получаемое героем за убийство врага
    ///// <summary>
    ///// Золото, получаемое героем за убийство врага
    ///// </summary>
    //public float GoldPerKill { get { return _goldPerKill; } set { _goldPerKill = value; } }

    private int _currentItemTier = 1;// Текущий тир предмета одетого на герое
    #endregion

    #region PRIVATE Methods
    /// <summary>
    /// Обновляем информацию по текущему одетому предмету на герое
    /// </summary>
    /// <param name="item">Новый предмет</param>
    private void EquipItem(Item item)
    {
        // Обновляем текущий тир одетого предмета
        _currentItemTier = item.ItemTier;

        // Обновляем урон героя
        _damage *= item.DamageMultiplyer;

        // Обновляем золото за убийство с монстра
        _goldPerKill *= item.GoldMultiplyer;
        // Освобождаем слот предмета
        item.OccupiedSlot = null;

        // Уничтожаем предмет
        Destroy(item.gameObject);
    }

    // Обрабатываем экипировку предмета на героя
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<Item>())
        {
            Item item = collision.GetComponent<Item>();
           
            if (TouchManager.IsMergable)
            {
                // Если тир перетягиваемого предмета на героя больше, то одеваем этот предмет
                if (item.ItemTier > _currentItemTier)
                {
                    EquipItem(item);
                    TouchManager.IsMergable = false;
                }
                else
                {
                    // Иначе возвращаем предмет в свой слот
                    item.gameObject.transform.position = item.StartPos;
                }
            }
        }
    }
    #endregion
}
