// Roman Baranov 28.07.2021

using UnityEngine;
using UnityEngine.UI;

public class ItemCostCounterUI : MonoBehaviour
{
    #region VARIABLES
    private static Text _itemCostText = null;// ������ �� ��������� � ������� �������� 
    #endregion

    #region UNITY Methods
    private void Awake()
    {
        _itemCostText = transform.Find("ItemCostCounterText").GetComponent<Text>();
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateUtemCost();
    }
    #endregion

    #region PRIVATE Methods
   
    #endregion

    /// <summary>
    /// ��������� ������� � ������������ ���� ������� ��������
    /// </summary>
    public static void UpdateUtemCost()
    {
        _itemCostText.text = $"{LevelProgress.CurrentItemBuyCost:F2}";
    }
}
