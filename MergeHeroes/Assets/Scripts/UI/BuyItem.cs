// Roman Baranov 27.07.2021

using UnityEngine;
using UnityEngine.UI;

public class BuyItem : MonoBehaviour
{
    #region VARIABLES
    private Button _buyItemButton = null;

    private ItemsSpawner _itemsSpawner = null;
    #endregion

    #region UNITY Methods
    private void Awake()
    {
        _buyItemButton = GetComponent<Button>();

        _itemsSpawner = FindObjectOfType<ItemsSpawner>();
    }
    // Start is called before the first frame update
    void Start()
    {
        _buyItemButton.onClick.AddListener(BuyNewItem);
    }

    private void OnDestroy()
    {
        _buyItemButton.onClick.RemoveAllListeners();
    }
    #endregion

    #region PRIVATE Methods
    /// <summary>
    /// Обрабатывает покупку предмета при нажатии на кнопку
    /// </summary>
    private void BuyNewItem()
    {
        // Проверить достаточно ли у игрока золота для покупки

        // Проверить есть ли свободные слоты в инвентаре

        // Вычесть деньги за предмет у игрока

        // Заспавнить предмет в первой свободной ячейке
        _itemsSpawner.SpawnItem(1);
        // Обновить счетчик золота у игрока
        
    }

    #endregion
}
