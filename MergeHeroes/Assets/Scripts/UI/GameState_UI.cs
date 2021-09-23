// Roman Baranov 01.09.2021

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameState_UI : MonoBehaviour
{
    #region VARIABLES
    // ������
    private GameObject _gameSettingsPopup = null;// ������ �� ����� � ����������� �� ������
    private GameObject _levelLostPopup = null;// ������ �� ����� � ������� ��������� �� ������
    private GameObject _levelWonPopup = null;// ������ �� ����� � ������� ������ �� ������
    private GameObject _roomClearedPopup = null;// ������ �� ����� � ������� ������ �� ������

    // ������
    private Button _gameSettingsButton = null;// ������ �� ������ � ����������� �� ������
    private Button _backButton = null;// ������ �������� �� �������
    private Button _restartLevelButton = null;// ������ �������� ������
    private Button _mainMenuButton = null;// ������ �������� � ������� ����
    private Button _backToMapButton = null;// ������ �������� �� ����� � ������� �������

    //
    private Button _leftRoomButton = null;// ������ �������� ����� ������� � ������ ������ ��������� �������
    private Button _rightRoomButton = null;// ������ �������� ������ ������� � ������ ������ ��������� �������
    #endregion

    #region UNITY Methods
    private void Awake()
    {
        // ������� ������ �� ������
        _gameSettingsPopup = transform.Find("GameSettingsPopup").gameObject;

        _levelLostPopup = transform.Find("LevelLostPopup").gameObject;
        _levelWonPopup = transform.Find("LevelWonPopup").gameObject;

        _roomClearedPopup = transform.Find("RoomClearedPopup").gameObject;

        // ������� ������ �� ������
        // ������ � ����������� ����
        _gameSettingsButton = transform.Find("GameSettings").Find("GameSettingsButton").GetComponent<Button>();
        _gameSettingsButton.onClick.AddListener(SwitchGameSettingsPopupState);

        // ������ �������� �� �������
        _backButton = transform.Find("BackButton").GetComponent<Button>();
        _backButton.onClick.AddListener(SwitchGameSettingsPopupState);

        // ������ �������� ������
        _restartLevelButton = transform.Find("RestartLevelButton").GetComponent<Button>();
        _restartLevelButton.onClick.AddListener(RestartLevel);

        // ������ �������� � ������� ���� ����
        _mainMenuButton = transform.Find("MainMenuButton").GetComponent<Button>();
        _mainMenuButton.onClick.AddListener(LoadMainMenu);

        // ������ �������� �� ����� � ������� �������
        _backToMapButton = transform.Find("BackToMapButton").GetComponent<Button>();
        _backToMapButton.onClick.AddListener(LoadLevelMap);

        // ������ �������� ����� ������� � ������ ������ ��������� �������
        _leftRoomButton = transform.Find("LeftRoomButton").GetComponent<Button>();
        _leftRoomButton.onClick.AddListener(LoadNextRoom);

        // ������ �������� ������ ������� � ������ ������ ��������� �������
        _rightRoomButton = transform.Find("RightRoomButton").GetComponent<Button>();
        _rightRoomButton.onClick.AddListener(LoadNextRoom);

        // ������������� �� ������� ��� ������ �����
        Hero.OnHeroDead += Hero_OnHeroDead;

        // ������������� �� ������� ����� �������
        CharactersSpawner.OnRoomCleared += CharactersSpawner_OnRoomCleared;
    }

    // Start is called before the first frame update
    void Start()
    {
        // ������� ���� � �����
        Time.timeScale = 1.0f;
        InitLevelUI();
    }

    private void OnDestroy()
    {
        _gameSettingsButton.onClick.RemoveAllListeners();

        _backButton.onClick.RemoveAllListeners();
        _restartLevelButton.onClick.RemoveAllListeners();
        _mainMenuButton.onClick.RemoveAllListeners();
        _backToMapButton.onClick.RemoveAllListeners();

        _leftRoomButton.onClick.RemoveAllListeners();
        _rightRoomButton.onClick.RemoveAllListeners();

        Hero.OnHeroDead -= Hero_OnHeroDead;
        CharactersSpawner.OnRoomCleared -= CharactersSpawner_OnRoomCleared;
    }
    #endregion

    #region PRIVATE Methods
    /// <summary>
    /// ��������� ��� ������ � ������ ������� �� ������ �� ������
    /// </summary>
    private void InitLevelUI()
    {
        _gameSettingsPopup.SetActive(false);

        _levelLostPopup.SetActive(false);
        _levelWonPopup.SetActive(false);

        _roomClearedPopup.SetActive(false);

        _backButton.gameObject.SetActive(false);
        _restartLevelButton.gameObject.SetActive(false);
        _mainMenuButton.gameObject.SetActive(false);
        _backToMapButton.gameObject.SetActive(false);

        _leftRoomButton.gameObject.SetActive(false);
        _rightRoomButton.gameObject.SetActive(false);
    }

    /// <summary>
    /// ��������� 2 ������������ ������� ��� ������ ������
    /// </summary>
    private void UpdateRoomButtons()
    {
        // �������� 2 ��������� �����
        int leftBtnIndex = Random.Range(0, ItemsSpawner.gameSettingsSO.RoomButtonSprites.Length);
        int rightBtnIndex = Random.Range(0, ItemsSpawner.gameSettingsSO.RoomButtonSprites.Length);

        // ��������� ������� �� ����������
        while (rightBtnIndex == leftBtnIndex)
        {
            rightBtnIndex = Random.Range(0, ItemsSpawner.gameSettingsSO.RoomButtonSprites.Length);
        }

        // ��������� ������� �� ��������� �������� �� ������ 
        _leftRoomButton.GetComponent<Image>().sprite = ItemsSpawner.gameSettingsSO.RoomButtonSprites[leftBtnIndex];
        _rightRoomButton.GetComponent<Image>().sprite = ItemsSpawner.gameSettingsSO.RoomButtonSprites[rightBtnIndex];

        // Placeholder!
        // ������� 2 ��������� ������� ��� ������������ �������
        int leftBtnModifierIndex = Random.Range(0, ItemsSpawner.gameSettingsSO.RoomModifiers.Length);
        int rightBtnModifierIndex = Random.Range(0, ItemsSpawner.gameSettingsSO.RoomModifiers.Length);

        // ��������� ������� �� ����������
        while (leftBtnModifierIndex == rightBtnModifierIndex)
        {
            rightBtnModifierIndex = Random.Range(0, ItemsSpawner.gameSettingsSO.RoomModifiers.Length);
        }

        // ��������� ������������ �� ��������� �������� �� ������ 
        _leftRoomButton.transform.GetComponentInChildren<Text>().text = ItemsSpawner.gameSettingsSO.RoomModifiers[leftBtnModifierIndex];
        _rightRoomButton.GetComponentInChildren<Text>().text = ItemsSpawner.gameSettingsSO.RoomModifiers[rightBtnModifierIndex];

    }

    /// <summary>
    /// ����������� ��������� ������ � ����������� ���� � ������ ��� ����� ������
    /// </summary>
    private void SwitchGameSettingsPopupState()
    {
        if (_gameSettingsPopup.activeSelf == false)
        {
            // ���������� ����� � ����������� ����
            _gameSettingsPopup.SetActive(true);

            // ���������� ������ ��� ����� ������
            _backButton.gameObject.SetActive(true);
            _restartLevelButton.gameObject.SetActive(true);
            _mainMenuButton.gameObject.SetActive(true);

            // ��������� ������ � ����������� ����, ����� �� ������ ���� ������ �����, ���� ������� �����
            _gameSettingsButton.enabled = false;
        }
        else
        {
            // ������������ ����� � ����������� ����
            _gameSettingsPopup.SetActive(false);

            // ����������� ������ ��� ����� ������
            _backButton.gameObject.SetActive(false);
            _restartLevelButton.gameObject.SetActive(false);
            _mainMenuButton.gameObject.SetActive(false);

            // �������� ������ � ����������� ����
            _gameSettingsButton.enabled = true;
        }
    }

    /// <summary>
    /// ���������� ����� ��������� � ������ ��� ����� ������
    /// </summary>
    private void ActivateLevelLostPopup()
    {
        // ������ ���� �� �����
        Time.timeScale = 0.0f;

        // ���������� ����� � ����������� ����
        _levelLostPopup.SetActive(true);

        // ���������� ������ ��� ����� ������
        _backToMapButton.gameObject.SetActive(true);
        _restartLevelButton.gameObject.SetActive(true);
        _mainMenuButton.gameObject.SetActive(true);

        // ��������� ������ � ����������� ����, ����� �� ������ ���� ������ �����, ���� ������� �����
        _gameSettingsButton.enabled = false;
    }

    /// <summary>
    /// ���������� ����� ������ � ������ ��� ����� ������
    /// </summary>
    private void ActivateLevelWonPopup()
    {
        // ������ ���� �� �����
        Time.timeScale = 0.0f;

        // ���������� ����� � ����������� ����
        _levelWonPopup.SetActive(true);

        // ���������� ������ ��� ����� ������
        _backToMapButton.gameObject.SetActive(true);
        _restartLevelButton.gameObject.SetActive(true);
        _mainMenuButton.gameObject.SetActive(true);

        // ��������� ������ � ����������� ����, ����� �� ������ ���� ������ �����, ���� ������� �����
        _gameSettingsButton.enabled = false;
    }

    /// <summary>
    /// ��������� ������� ������� �����
    /// </summary>
    private void RestartLevel()
    {
        LevelProgress.ResetResources();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /// <summary>
    /// ��������� ������� ���� ����
    /// </summary>
    private void LoadMainMenu()
    {
        LevelProgress.ResetResources();
        SceneManager.LoadScene(0);
    }

    /// <summary>
    /// TO DO ��������� ����� � ��������
    /// </summary>
    private void LoadLevelMap()
    {
        // TO DO ��������� ����� � ������� �������
        LevelProgress.ResetResources();
        LoadMainMenu();
    }

 /// <summary>
 /// Switch Room Cleared Popup active states
 /// </summary>
 /// <param name="activate">Room Cleared Popup target active state</param>
    private void SwitchRoomClearedPopup(bool activate)
    {
        if (activate)
        {
            // ������ ���� �� �����
            Time.timeScale = 0.0f;

            // ���������� ����� � �������� �������
            _roomClearedPopup.SetActive(activate);

            // ���������� ������ ��� ����� ������
            _leftRoomButton.gameObject.SetActive(activate);
            _rightRoomButton.gameObject.SetActive(activate);

            // ���������� ������ �������� � ������� ���� ����
            _mainMenuButton.gameObject.SetActive(activate);

            // ��������� ������ � ����������� ����, ����� �� ������ ���� ������ �����, ���� ������� �����
            _gameSettingsButton.enabled = !activate;
        }
        else
        {
            // ������� ���� � �����
            Time.timeScale = 1.0f;

            // ��������� ����� � �������� �������
            _roomClearedPopup.SetActive(activate);

            // ��������� ������ ��� ����� ������
            _leftRoomButton.gameObject.SetActive(activate);
            _rightRoomButton.gameObject.SetActive(activate);

            // ��������� ������ �������� � ������� ���� ����
            _mainMenuButton.gameObject.SetActive(activate);

            // ���������� ������ � ����������� ����, ����� �� ����� ���� ������ �����
            _gameSettingsButton.enabled = !activate;
        }

    }

    /// <summary>
    /// Adds new room to the level and deactivates Room Cleared Popup
    /// </summary>
    private void LoadNextRoom()
    {
        Level.AddRoom(RoomModificator.Modificator.None);
        SwitchRoomClearedPopup(false);
    }
    #endregion

    #region EVENTS
    /// <summary>
    /// If hero HP = 0 activate Level Lost popup
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Hero_OnHeroDead(object sender, System.EventArgs e)
    {
        ActivateLevelLostPopup();
    }

    /// <summary>
    /// When all monster waves in room are cleared shows Room Cleared Popup or Level Won Popup
    /// </summary>
    /// <param name="sender">Event sender</param>
    /// <param name="e">Additional event params</param>
    private void CharactersSpawner_OnRoomCleared(object sender, System.EventArgs e)
    {
        // Check if level is completed
        if (Level.CurrentRoom.CurRoomNumber == Level.MaxRooms)
        {
            ActivateLevelWonPopup();
        }
        else
        {
            UpdateRoomButtons();
            SwitchRoomClearedPopup(true);
        }
    }
    #endregion
}
