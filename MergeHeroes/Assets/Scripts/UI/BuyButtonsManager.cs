// Roman Baranov 27.08.2021

using UnityEngine;
using UnityEngine.UI;

public class BuyButtonsManager : MonoBehaviour
{
    #region VARIABLES
    private static Button[] _buyButtons = null;
    #endregion

    #region UNITY Methods
    // Start is called before the first frame update
    void Awake()
    {
        FindBuyButtons();
    }
    #endregion

    #region PUBLIC Methods
    /// <summary>
    /// ���������� ������ ������� ���������� ���� � ��������� ��� ���������
    /// </summary>
    /// <param name="buttonType">��� ������, ������� ���� ������������</param>
    public static void ShowButton(ItemTypes.Items buttonType)
    {
        for (int i = 0; i < _buyButtons.Length; i++)
        {
            if (_buyButtons[i].GetComponent<BuyItem>().ButtonType == buttonType)
            {
                _buyButtons[i].gameObject.SetActive(true);
            }
            else
            {
                _buyButtons[i].gameObject.SetActive(false);
            }
        }
    }
    #endregion

    #region PRIVATE Methods
    /// <summary>
    /// ������� ��� ������ ������� � ��������� ������ � ����� ��������
    /// </summary>
    private void FindBuyButtons()
    {
        BuyItem[] buttons = transform.GetComponentsInChildren<BuyItem>();

        _buyButtons = new Button[buttons.Length];

        for (int i = 0; i < buttons.Length; i++)
        {
            _buyButtons[i] = buttons[i].GetComponent<Button>();
        }
    }
    #endregion
}
