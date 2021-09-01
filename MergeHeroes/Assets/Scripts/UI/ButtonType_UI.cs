// Roman Baranov 27.08.2021

using UnityEngine;
using UnityEngine.UI;

public class ButtonType_UI : MonoBehaviour
{
    #region VARIABLES
    private Button _button = null;

    [SerializeField] private ItemTypes.Items _buttonType;
    /// <summary>
    /// Тип кнопки
    /// </summary>
    public ItemTypes.Items ItemType { get { return _buttonType; } }
    #endregion

    #region UNITY Methods
    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void Start()
    {
        _button.onClick.AddListener(ActivateBuyButton);
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveAllListeners();
    }
    #endregion

    #region PRIVATE Methods
    private void ActivateBuyButton()
    {
        BuyButtonsManager.ShowButton(_buttonType);
    }
    #endregion

}
