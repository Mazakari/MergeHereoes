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

    // ������
    private Button _gameSettingsButton = null;// ������ �� ������ � ����������� �� ������
    private Button _backButton = null;// ������ �������� �� �������
    private Button _restartLevelButton = null;// ������ �������� ������
    private Button _mainMenuButton = null;// ������ �������� � ������� ����
    private Button _backToMapButton = null;// ������ �������� �� ����� � ������� �������

    #endregion

    #region UNITY Methods
    private void Awake()
    {
        // ������� ������ �� ������
        _gameSettingsPopup = transform.Find("GameSettingsPopup").gameObject;
        _levelLostPopup = transform.Find("LevelLostPopup").gameObject;
        _levelWonPopup = transform.Find("LevelWonPopup").gameObject;

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

        // ������������� �� ������� ��� ������ �����
        Hero.OnHeroDead += Hero_OnHeroDead;
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

        _backButton.gameObject.SetActive(false);
        _restartLevelButton.gameObject.SetActive(false);
        _mainMenuButton.gameObject.SetActive(false);
        _backToMapButton.gameObject.SetActive(false);
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
        Debug.Log(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /// <summary>
    /// ��������� ������� ���� ����
    /// </summary>
    private void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    /// <summary>
    /// TO DO ��������� ����� � ��������
    /// </summary>
    private void LoadLevelMap()
    {
        // TO DO ��������� ����� � ������� �������
        LoadMainMenu();
    }
    #endregion

    #region EVENTS
    /// <summary>
    /// ���� � ����� �� = 0, �� ���������� ����� ��������� �� ������
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Hero_OnHeroDead(object sender, System.EventArgs e)
    {
        // TO DO ����������� � ������� ��� �������� � ������������ ���������� ������
        //ActivateLevelLostPopup();
    }
    #endregion
}
