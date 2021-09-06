// Roman Baranov 01.09.2021

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameState_UI : MonoBehaviour
{
    #region VARIABLES
    // Попапы
    private GameObject _gameSettingsPopup = null;// Ссылка на попап с настройками на уровне
    private GameObject _levelLostPopup = null;// Ссылка на попап с попапом проигрыша на уровне
    private GameObject _levelWonPopup = null;// Ссылка на попап с попапом победы на уровне

    // Кнопки
    private Button _gameSettingsButton = null;// Ссылка на кнопку с настройками на уровне
    private Button _backButton = null;// Кнопка возврата на уровень
    private Button _restartLevelButton = null;// Кнопка рестарта уровня
    private Button _mainMenuButton = null;// Кнопка возврата в главное меню
    private Button _backToMapButton = null;// Кнопка возврата на карту с выбором уровней

    #endregion

    #region UNITY Methods
    private void Awake()
    {
        // Находим ссылки на попапы
        _gameSettingsPopup = transform.Find("GameSettingsPopup").gameObject;
        _levelLostPopup = transform.Find("LevelLostPopup").gameObject;
        _levelWonPopup = transform.Find("LevelWonPopup").gameObject;

        // Находим ссылки на кнопки
        // Кнопка с настройками игры
        _gameSettingsButton = transform.Find("GameSettings").Find("GameSettingsButton").GetComponent<Button>();
        _gameSettingsButton.onClick.AddListener(SwitchGameSettingsPopupState);

        // Кнопка возврата на уровень
        _backButton = transform.Find("BackButton").GetComponent<Button>();
        _backButton.onClick.AddListener(SwitchGameSettingsPopupState);

        // Кнопка рестарта уровня
        _restartLevelButton = transform.Find("RestartLevelButton").GetComponent<Button>();
        _restartLevelButton.onClick.AddListener(RestartLevel);

        // Кнопка возврата в главное меню игры
        _mainMenuButton = transform.Find("MainMenuButton").GetComponent<Button>();
        _mainMenuButton.onClick.AddListener(LoadMainMenu);

        // Кнопка возврата на карту с выбором уровней
        _backToMapButton = transform.Find("BackToMapButton").GetComponent<Button>();
        _backToMapButton.onClick.AddListener(LoadLevelMap);

        // Подписываемся на событие при смерти героя
        Hero.OnHeroDead += Hero_OnHeroDead;
    }


    // Start is called before the first frame update
    void Start()
    {
        // Снимаем игру с паузы
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
    /// Отключает все попапы и кнопки попапов на уровне на старте
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
    /// Переключает состояние попапа с настройками игры и кнопки для этого попапа
    /// </summary>
    private void SwitchGameSettingsPopupState()
    {
        if (_gameSettingsPopup.activeSelf == false)
        {
            // Активируем попап с настройками игры
            _gameSettingsPopup.SetActive(true);

            // Активируем кнопки для этого попапа
            _backButton.gameObject.SetActive(true);
            _restartLevelButton.gameObject.SetActive(true);
            _mainMenuButton.gameObject.SetActive(true);

            // Отключаем кнопку с настройками игры, чтобы ее нельзя было нажать снова, пока активен попап
            _gameSettingsButton.enabled = false;
        }
        else
        {
            // Деактивируем попап с настройками игры
            _gameSettingsPopup.SetActive(false);

            // Дективируем кнопки для этого попапа
            _backButton.gameObject.SetActive(false);
            _restartLevelButton.gameObject.SetActive(false);
            _mainMenuButton.gameObject.SetActive(false);

            // Включаем кнопку с настройками игры
            _gameSettingsButton.enabled = true;
        }
    }

    /// <summary>
    /// Активирует попап проигрыша и кнопки для этого попапа
    /// </summary>
    private void ActivateLevelLostPopup()
    {
        // Ставим игру на паузу
        Time.timeScale = 0.0f;

        // Активируем попап с настройками игры
        _levelLostPopup.SetActive(true);

        // Активируем кнопки для этого попапа
        _backToMapButton.gameObject.SetActive(true);
        _restartLevelButton.gameObject.SetActive(true);
        _mainMenuButton.gameObject.SetActive(true);

        // Отключаем кнопку с настройками игры, чтобы ее нельзя было нажать снова, пока активен попап
        _gameSettingsButton.enabled = false;
    }

    /// <summary>
    /// Активирует попап победы и кнопки для этого попапа
    /// </summary>
    private void ActivateLevelWonPopup()
    {
        // Ставим игру на паузу
        Time.timeScale = 0.0f;

        // Активируем попап с настройками игры
        _levelWonPopup.SetActive(true);

         // Активируем кнопки для этого попапа
         _backToMapButton.gameObject.SetActive(true);
         _restartLevelButton.gameObject.SetActive(true);
         _mainMenuButton.gameObject.SetActive(true);

         // Отключаем кнопку с настройками игры, чтобы ее нельзя было нажать снова, пока активен попап
         _gameSettingsButton.enabled = false;
    }

    /// <summary>
    /// Загружает текущий уровень снова
    /// </summary>
    private void RestartLevel()
    {
        Debug.Log(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /// <summary>
    /// Загружает главное меню игры
    /// </summary>
    private void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    /// <summary>
    /// TO DO Загружает карту с уровнями
    /// </summary>
    private void LoadLevelMap()
    {
        // TO DO загружаем сцену с выбором уровней
        LoadMainMenu();
    }
    #endregion

    #region EVENTS
    /// <summary>
    /// Если у героя ХП = 0, то активируем попап проигрыша на уровне
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Hero_OnHeroDead(object sender, System.EventArgs e)
    {
        // TO DO разобраться с ошибкой при рестарте и уничтожением компонента текста
        //ActivateLevelLostPopup();
    }
    #endregion
}
