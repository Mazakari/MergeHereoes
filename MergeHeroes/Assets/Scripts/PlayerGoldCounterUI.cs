// Roman Baranov 28.07.2021

using UnityEngine;
using UnityEngine.UI;

public class PlayerGoldCounterUI : MonoBehaviour
{
    #region VARIABLES
    private static Text _counterText = null;// ������ �� ��������� ������ ��������

    #endregion

    #region UNITY Methods
    private void Awake()
    {
        _counterText = GetComponent<Text>();
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateGoldCounter();
    }
    #endregion

    #region PUBLIC Methods
    /// <summary>
    /// ��������� �������� �������� ������ ������
    /// </summary>
    public static void UpdateGoldCounter()
    {
        _counterText.text = $"{PlayerSettingsSO.CurrentGoldAmount.ToString("F2")}";
    }
    #endregion

}
