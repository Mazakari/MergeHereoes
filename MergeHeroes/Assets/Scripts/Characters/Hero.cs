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
    #endregion

    #region UNITY Methods
   
    #endregion

    #region PRIVATE Methods
    /// <summary>
    /// Обновляем информацию по текущему одетому предмету на герое
    /// </summary>
    /// <param name="item">Новый предмет</param>
    private void EquipItem(Item item)
    {
        // Обновляем текущий тир одетого предмета в UI

        // Обновляем урон героя

        // Освобождаем слот предмета

        // Обновляем статы героя в UI

        // Уничтожаем предмет
        //Destroy(item.gameObject);
    }

    // Обрабатываем экипировку предмета на героя
    private void OnTriggerStay2D(Collider2D collision)
    {
        // Если тир перетягиваемого предмета на героя больше, то одеваем этот предмет
        // Иначе возвращаем предмет в свой слот
    }
    #endregion
   
}

    
