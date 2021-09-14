// Roman Baranov 01.09.2021

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu_UI : MonoBehaviour
{
    #region VARIABLES
    private Button _startGameButton = null;// Ссылка на кнопку старта игры
    private Button _quitGameButton = null;// Ссылка на кнопку выхода из игры

    private GameObject _gameModesPopup = null;// Попап с кнопками выбора режимов игры
    private Button _easyModeGameButton = null;// Кнопка легкого режима игры
    private Button _normalModeGameButton = null;// Кнопка среднего режима игры
    private Button _hardModeGameButton = null;// Кнопка сложного режима игры
    private Button _veryHardModeGameButton = null;// Кнопка сложного режима игры

    private Button [] _gameModeButtonsCollection = null;// Коллекция кнопок с режимами сложности игры
    private int _curGameModeSelected = 0;// Индекс текущего выбранного режима сложности игры

    private Button _previousGameDifficultyButton = null;// Кнопка переключения режима сложности вперед
    private Button _nextGameDifficultyButton = null;// Кнопка переключения режима сложности назад



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
    /// Добавляет ссылки всем элементам UI главного меню перед стартом сцены
    /// </summary>
    private void SetUIElementReferences()
    {
        // Кнопки старта и выхода из игры
        _startGameButton = transform.Find("StartGameButton").GetComponent<Button>();
        _startGameButton.onClick.AddListener(ShowGameModesPopup);

        _quitGameButton = transform.Find("QuitGameButton").GetComponent<Button>();
        _quitGameButton.onClick.AddListener(QuitGame);

        // Попап с кнопками выбора режимов игры
        _gameModesPopup = transform.Find("GameModesPopup").gameObject;

        // Кнопки выбора режимов игры
        _easyModeGameButton = _gameModesPopup.transform.Find("GameModesButtons").Find("EasyModeGameButton").GetComponent<Button>(); 
        _easyModeGameButton.onClick.AddListener(StartEasyGame);

        _normalModeGameButton = _gameModesPopup.transform.Find("GameModesButtons").Find("NormalModeGameButton").GetComponent<Button>();
        _normalModeGameButton.onClick.AddListener(StartMediumGame);

        _hardModeGameButton = _gameModesPopup.transform.Find("GameModesButtons").Find("HardModeGameButton").GetComponent<Button>();
        _hardModeGameButton.onClick.AddListener(StartHardGame);

        _veryHardModeGameButton = _gameModesPopup.transform.Find("GameModesButtons").Find("VeryHardModeGameButton").GetComponent<Button>();
        _veryHardModeGameButton.onClick.AddListener(StartVeryHardGame);

        // Кнопки переключения режимов игры
        _previousGameDifficultyButton = _gameModesPopup.transform.Find("SwitchGameModesButtons").Find("PreviousGameDifficultyButton").GetComponent<Button>();
        _previousGameDifficultyButton.onClick.AddListener(PreviousGameDifficulty);

        _nextGameDifficultyButton = _gameModesPopup.transform.Find("SwitchGameModesButtons").Find("NextGameDifficultyButton").GetComponent<Button>();
        _nextGameDifficultyButton.onClick.AddListener(NextGameDifficulty);
    }

    /// <summary>
    /// Получает количество кнопок с режимами игры
    /// </summary>
    private void GetGameModesButtons()
    {
        _gameModeButtonsCollection = transform.Find("GameModesPopup").Find("GameModesButtons").GetComponentsInChildren<Button>();
    }


    /// <summary>
    /// Настраивает UI на старте сцены с главным меню
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
    /// Активирует кнопку режима игры по указанному индексу, остальные деактивирует
    /// </summary>
    /// <param name="buttonIndex">Индекс кнопки режима игры, которую нужно активировать</param>
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
    /// Показывает попап с выбором режимов игры
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
    /// Запускает игру в режиме "Легко"
    /// </summary>
    private void StartEasyGame()
    {
        // TO DO Загружать + 2, т.к. +1 будет карта с уровнями
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    /// <summary>
    /// Запускает игру в режиме "Средне"
    /// </summary>
    private void StartMediumGame()
    {
        // TO DO Загружать + 2, т.к. +1 будет карта с уровнями
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    /// <summary>
    /// Запускает игру в режиме "Сложно"
    /// </summary>
    private void StartHardGame()
    {
        // TO DO Загружать + 2, т.к. +1 будет карта с уровнями
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    /// <summary>
    /// Запускает игру в режиме "Очень Сложно"
    /// </summary>
    private void StartVeryHardGame()
    {
        // TO DO Загружать + 2, т.к. +1 будет карта с уровнями
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    /// <summary>
    /// Закрывает игру
    /// </summary>
    private void QuitGame()
    {
        Debug.Log("QuitGame");
        Application.Quit();
    }
    #endregion
}
