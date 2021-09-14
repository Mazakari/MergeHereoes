// Roman Baranov 01.09.2021

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu_UI : MonoBehaviour
{
    #region VARIABLES
    private Button _startGameButton = null;// ������ �� ������ ������ ����
    private Button _quitGameButton = null;// ������ �� ������ ������ �� ����

    private GameObject _gameModesPopup = null;// ����� � �������� ������ ������� ����
    private Button _easyModeGameButton = null;// ������ ������� ������ ����
    private Button _normalModeGameButton = null;// ������ �������� ������ ����
    private Button _hardModeGameButton = null;// ������ �������� ������ ����
    private Button _veryHardModeGameButton = null;// ������ �������� ������ ����

    private Button [] _gameModeButtonsCollection = null;// ��������� ������ � �������� ��������� ����
    private int _curGameModeSelected = 0;// ������ �������� ���������� ������ ��������� ����

    private Button _previousGameDifficultyButton = null;// ������ ������������ ������ ��������� ������
    private Button _nextGameDifficultyButton = null;// ������ ������������ ������ ��������� �����



    #endregion

    #region UNITY Methods
    private void Awake()
    {
        SetUIElementReferences();
        GetGameModesButtons();
    }

    // Start is called before the first frame update
    void Start()
    {
        InitMainMenuUI();
    }

    private void OnDestroy()
    {
        _startGameButton.onClick.RemoveAllListeners();
        _quitGameButton.onClick.RemoveAllListeners();

        _easyModeGameButton.onClick.RemoveAllListeners();
        _normalModeGameButton.onClick.RemoveAllListeners();
        _hardModeGameButton.onClick.RemoveAllListeners();
        _veryHardModeGameButton.onClick.RemoveAllListeners();

        _previousGameDifficultyButton.onClick.RemoveAllListeners();
        _nextGameDifficultyButton.onClick.RemoveAllListeners();
    }
    #endregion

    #region PRIVATE Methods
    /// <summary>
    /// ��������� ������ ���� ��������� UI �������� ���� ����� ������� �����
    /// </summary>
    private void SetUIElementReferences()
    {
        // ������ ������ � ������ �� ����
        _startGameButton = transform.Find("StartGameButton").GetComponent<Button>();
        _startGameButton.onClick.AddListener(ShowGameModesPopup);

        _quitGameButton = transform.Find("QuitGameButton").GetComponent<Button>();
        _quitGameButton.onClick.AddListener(QuitGame);

        // ����� � �������� ������ ������� ����
        _gameModesPopup = transform.Find("GameModesPopup").gameObject;

        // ������ ������ ������� ����
        _easyModeGameButton = _gameModesPopup.transform.Find("GameModesButtons").Find("EasyModeGameButton").GetComponent<Button>(); 
        _easyModeGameButton.onClick.AddListener(StartEasyGame);

        _normalModeGameButton = _gameModesPopup.transform.Find("GameModesButtons").Find("NormalModeGameButton").GetComponent<Button>();
        _normalModeGameButton.onClick.AddListener(StartMediumGame);

        _hardModeGameButton = _gameModesPopup.transform.Find("GameModesButtons").Find("HardModeGameButton").GetComponent<Button>();
        _hardModeGameButton.onClick.AddListener(StartHardGame);

        _veryHardModeGameButton = _gameModesPopup.transform.Find("GameModesButtons").Find("VeryHardModeGameButton").GetComponent<Button>();
        _veryHardModeGameButton.onClick.AddListener(StartVeryHardGame);

        // ������ ������������ ������� ����
        _previousGameDifficultyButton = _gameModesPopup.transform.Find("SwitchGameModesButtons").Find("PreviousGameDifficultyButton").GetComponent<Button>();
        _previousGameDifficultyButton.onClick.AddListener(PreviousGameDifficulty);

        _nextGameDifficultyButton = _gameModesPopup.transform.Find("SwitchGameModesButtons").Find("NextGameDifficultyButton").GetComponent<Button>();
        _nextGameDifficultyButton.onClick.AddListener(NextGameDifficulty);
    }

    /// <summary>
    /// �������� ���������� ������ � �������� ����
    /// </summary>
    private void GetGameModesButtons()
    {
        _gameModeButtonsCollection = transform.Find("GameModesPopup").Find("GameModesButtons").GetComponentsInChildren<Button>();
    }


    /// <summary>
    /// ����������� UI �� ������ ����� � ������� ����
    /// </summary>
    private void InitMainMenuUI()
    {
        _startGameButton.gameObject.SetActive(true);
        _quitGameButton.gameObject.SetActive(true);

        ActivateGameModeButton(_curGameModeSelected);

        if (_curGameModeSelected == 0)
        {
            _previousGameDifficultyButton.enabled = false;
        }

        _gameModesPopup.SetActive(false);
    }

    /// <summary>
    /// ���������� ������ ������ ���� �� ���������� �������, ��������� ������������
    /// </summary>
    /// <param name="buttonIndex">������ ������ ������ ����, ������� ����� ������������</param>
    private void ActivateGameModeButton(int buttonIndex)
    {
        for (int i = 0; i < _gameModeButtonsCollection.Length; i++)
        {
            if (i == buttonIndex)
            {
                _gameModeButtonsCollection[i].gameObject.SetActive(true);
                continue;
            }

            _gameModeButtonsCollection[i].gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// ���������� ����� � ������� ������� ����
    /// </summary>
    private void ShowGameModesPopup()
    {
        _startGameButton.gameObject.SetActive(false);
        _gameModesPopup.SetActive(true);
    }

    private void NextGameDifficulty()
    {
        _previousGameDifficultyButton.enabled = true;

        if ((_curGameModeSelected + 1) <= _gameModeButtonsCollection.Length)
        {
            _curGameModeSelected++;

            ActivateGameModeButton(_curGameModeSelected);
        }

        if (_curGameModeSelected == _gameModeButtonsCollection.Length - 1)
        {
            _nextGameDifficultyButton.enabled = false;
        }
    }

    private void PreviousGameDifficulty()
    {
        _nextGameDifficultyButton.enabled = true;

        if ((_curGameModeSelected - 1) >= 0)
        {
            _curGameModeSelected--;

            ActivateGameModeButton(_curGameModeSelected);
        }

        if (_curGameModeSelected == 0)
        {
            _previousGameDifficultyButton.enabled = false;
        }
    }

    /// <summary>
    /// ��������� ���� � ������ "�����"
    /// </summary>
    private void StartEasyGame()
    {
        // TO DO ��������� + 2, �.�. +1 ����� ����� � ��������
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    /// <summary>
    /// ��������� ���� � ������ "������"
    /// </summary>
    private void StartMediumGame()
    {
        // TO DO ��������� + 2, �.�. +1 ����� ����� � ��������
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    /// <summary>
    /// ��������� ���� � ������ "������"
    /// </summary>
    private void StartHardGame()
    {
        // TO DO ��������� + 2, �.�. +1 ����� ����� � ��������
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    /// <summary>
    /// ��������� ���� � ������ "����� ������"
    /// </summary>
    private void StartVeryHardGame()
    {
        // TO DO ��������� + 2, �.�. +1 ����� ����� � ��������
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    /// <summary>
    /// ��������� ����
    /// </summary>
    private void QuitGame()
    {
        Debug.Log("QuitGame");
        Application.Quit();
    }
    #endregion
}
