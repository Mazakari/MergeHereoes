// Roman Baranov 27.07.2021

using UnityEngine;
using UnityEngine.UI;

public class BuyItem : MonoBehaviour
{
    #region VARIABLES
    private Button _buyItemButton = null;

    [SerializeField] private ItemTypes.Items _buttonType;
    /// <summary>
    /// ��� ������
    /// </summary>
    public ItemTypes.Items ButtonType { get { return _buttonType; } }

    private ItemsSpawner _itemsSpawner = null;

    #endregion

    #region UNITY Methods
    private void Awake()
    {
        _buyItemButton = GetComponent<Button>();

        _itemsSpawner = FindObjectOfType<ItemsSpawner>();
    }
    // Start is called before the first frame update
    void Start() => _buyItemButton.onClick.AddListener(BuyNewItem);
    private void OnDestroy() => _buyItemButton.onClick.RemoveAllListeners();
    #endregion

    #region PRIVATE Methods
    /// <summary>
    /// ������������ ������� �������� ��� ������� �� ������
    /// </summary>
    private void BuyNewItem()
    {
        // ��������� ���������� �� � ������ ������ ��� �������
        if (CheckItemCost(_buttonType))
        {
            // ��������� ���� �� ��������� ����� � ���������
            if (ItemsSpawner.FindEmptySlot())
            {
                switch (_buttonType)
                {
                    case ItemTypes.Items.Sword:
                        BuySword();
                        break;

                    case ItemTypes.Items.Armour:
                        BuyArmour();
                        break;

                    case ItemTypes.Items.Potion:
                        BuyPotion();
                        break;

                    default:
                        Debug.Log("There is no such Item Type found!");
                        break;
                }
            }
            else
            {
                Debug.Log("No empty slots avaiable");
            }
        }
        else
        {
            Debug.Log("Not enough money");
        }
    }

    /// <summary>
    /// ��������� ���������� �� � ������ ����� �� ������� ���������� ���� ��������.
    /// ���� �������, ���������� true, ���� ���, �� ���������� false
    /// </summary>
    /// <param name="itemType">��� ��������, ���� �������� ����� ���������</param>
    /// <returns>bool</returns>
    private bool CheckItemCost(ItemTypes.Items itemType)
    {
        switch (itemType)
        {
            case ItemTypes.Items.Sword:
                if (LevelProgress.CurrentGoldAmount >= LevelProgress.CurrentSwordBuyCost)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            case ItemTypes.Items.Armour:
                if (LevelProgress.CurrentGoldAmount >= LevelProgress.CurrentArmourBuyCost)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            case ItemTypes.Items.Potion:
                if (LevelProgress.CurrentGoldAmount >= LevelProgress.CurrentPotionBuyCost)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            default:
                Debug.Log("No Such Item Type Found!");
                return false;
        }
    }

    /// <summary>
    /// �������� ������� "���" � ������� ��� � ��������� ������
    /// </summary>
    private void BuySword()
    {
        // ������� ������ �� ��� � ������
        LevelProgress.CurrentGoldAmount -= LevelProgress.CurrentSwordBuyCost;

        // �������� ���� ������� �� ��������� ��������� ���� �� LevelProgress
        LevelProgress.CurrentSwordBuyCost *= LevelProgress.SwordCostMultiplier;

        // �������� ������� ��������� ����
        ItemCostCounterUI.UpdateSwordCostUI();

        // ���������� ��� � ������ ��������� ������ ���������
        _itemsSpawner.SpawnItem(1, ItemTypes.Items.Sword);

        // �������� ������� ������ � ������
        PlayerGoldCounterUI.UpdateGoldCounter();
    }

    /// <summary>
    /// �������� ������� "�����" � ������� ��� � ��������� ������
    /// </summary>
    private void BuyArmour()
    {
        // ������� ������ �� ����� � ������
        LevelProgress.CurrentGoldAmount -= LevelProgress.CurrentArmourBuyCost;

        // �������� ���� ������� ����� �� ��������� �������� �� LevelProgress
        LevelProgress.CurrentArmourBuyCost *= LevelProgress.ArmourCostMultiplier;

        // �������� ������� ��������� �����
        ItemCostCounterUI.UpdateArmourCostUI();

        // ���������� ����� � ������ ��������� ������ ���������
        _itemsSpawner.SpawnItem(1, ItemTypes.Items.Armour);

        // �������� ������� ������ � ������
        PlayerGoldCounterUI.UpdateGoldCounter();
    }

    /// <summary>
    /// �������� ������� "�����" � ������� ��� � ��������� ������
    /// </summary>
    private void BuyPotion()
    {
        // ������� ������ �� ����� � ������
        LevelProgress.CurrentGoldAmount -= LevelProgress.CurrentPotionBuyCost;

        // �������� ���� ������� ����� �� ��������� �������� �� LevelProgress
        LevelProgress.CurrentPotionBuyCost *= LevelProgress.PotionCostMultiplier;

        // �������� ������� ��������� �����
        ItemCostCounterUI.UpdatePotionCostUI();

        // ���������� ����� � ������ ��������� ������
        _itemsSpawner.SpawnItem(1, ItemTypes.Items.Potion);

        // �������� ������� ������ � ������
        PlayerGoldCounterUI.UpdateGoldCounter();
    }
    #endregion
}
