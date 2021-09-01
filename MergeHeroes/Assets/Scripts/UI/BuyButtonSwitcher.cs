// Roman Baranov 27.08.2021

using UnityEngine;
using UnityEngine.UI;

public class BuyButtonSwitcher : MonoBehaviour
{
    #region VARIABLES
    private Button _buyButtonSwitcher = null;
    private Text _buyButtonSwitcherText = null;

    [SerializeField] private GameObject _buttonTypesPanel = null;

    private bool _isPanelActive = false;

    #endregion

    #region UNITY Methods
    private void Awake()
    {
        _buyButtonSwitcher = GetComponent<Button>();
        _buyButtonSwitcherText = GetComponentInChildren <Text>();
    }

    private void Start()
    {
        _buyButtonSwitcher.onClick.AddListener(SwitchPanel);
        _buttonTypesPanel.SetActive(_isPanelActive);
        _buyButtonSwitcherText.text = "+";
        BuyButtonsManager.ShowButton(ItemTypes.Items.Sword);
    }

    private void OnDestroy()
    {
        _buyButtonSwitcher.onClick.RemoveAllListeners();
    }
    #endregion

    #region PRIVATE Methods
    /// <summary>
    /// Переключает состояние панели с выбором покупки типов предметов
    /// </summary>
    private void SwitchPanel()
    {
        if (_isPanelActive == false)
        {
            _buyButtonSwitcherText.text = "-";
            _isPanelActive = !_isPanelActive;
            _buttonTypesPanel.SetActive(_isPanelActive);
        }
        else
        {
            _buyButtonSwitcherText.text = "+";
            _isPanelActive = !_isPanelActive;
            _buttonTypesPanel.SetActive(_isPanelActive);
        }
    }
    #endregion
}
