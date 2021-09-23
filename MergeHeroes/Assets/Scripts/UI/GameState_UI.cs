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
    private GameObject _roomClearedPopup = null;// Ссылка на попап с попапом победы на уровне

    // Кнопки
    private Button _gameSettingsButton = null;// Ссылка на кнопку с настройками на уровне
    private Button _backButton = null;// Кнопка возврата на уровень
    private Button _restartLevelButton = null;// Кнопка рестарта уровня
    private Button _mainMenuButton = null;// Кнопка возврата в главное меню
    private Button _backToMapButton = null;// Кнопка возврата на карту с выбором уровней

    //
    private Button _leftRoomButton = null;// Кнопка загрузки левой комнаты в попапе выбора следующей комнаты
    private Button _rightRoomButton = null;// Кнопка загрузки правой комнаты в попапе выбора следующей комнаты
    #endregion

    #region UNITY Methods
    private void Awake()
    {
        // Находим ссылки на попапы
        _gameSettingsPopup = transform.Find("GameSettingsPopup").gameObject;

        _levelLostPopup = transform.Find("LevelLostPopup").gameObject;
        _levelWonPopup = transform.Find("LevelWonPopup").gameObject;

        _roomClearedPopup = transform.Find("RoomClearedPopup").gameObject;

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

        // Кнопка загрузки левой комнаты в попапе выбора следующей комнаты
        _leftRoomButton = transform.Find("LeftRoomButton").GetComponent<Button>();
        _leftRoomButton.onClick.AddListener(LoadNextRoom);

        // Кнопка загрузки правой комнаты в попапе выбора следующей комнаты
        _rightRoomButton = transform.Find("RightRoomButton").GetComponent<Button>();
        _rightRoomButton.onClick.AddListener(LoadNextRoom);

        // Подписываемся на событие при смерти героя
        Hero.OnHeroDead += Hero_OnHeroDead;

        // Подписываемся на событие смены комнаты
        CharactersSpawner.OnRoomCleared += CharactersSpawner_OnRoomCleared;
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

        _leftRoomButton.onClick.RemoveAllListeners();
        _rightRoomButton.onClick.RemoveAllListeners();

        Hero.OnHeroDead -= Hero_OnHeroDead;
        CharactersSpawner.OnRoomCleared -= CharactersSpawner_OnRoomCleared;
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

        _roomClearedPopup.SetActive(false);

        _backButton.gameObject.SetActive(false);
        _restartLevelButton.gameObject.SetActive(false);
        _mainMenuButton.gameObject.SetActive(false);
        _backToMapButton.gameObject.SetActive(false);

        _leftRoomButton.gameObject.SetActive(false);
        _rightRoomButton.gameObject.SetActive(false);
    }

    /// <summary>
    /// Назначает 2 произвольных спрайта для кнопок комнат
    /// </summary>
    private void UpdateRoomButtons()
    {
        // Выбираем 2 рандомных числа
        int leftBtnIndex = Random.Range(0, ItemsSpawner.gameSettingsSO.RoomButtonSprites.Length);
        int rightBtnIndex = Random.Range(0, ItemsSpawner.gameSettingsSO.RoomButtonSprites.Length);

        // Фильтруем индексы от повторения
        while (rightBtnIndex == leftBtnIndex)
        {
            rightBtnIndex = Random.Range(0, ItemsSpawner.gameSettingsSO.RoomButtonSprites.Length);
        }

        // Назначаем спрайты по выбранным индексам на кнопки 
        _leftRoomButton.GetComponent<Image>().sprite = ItemsSpawner.gameSettingsSO.RoomButtonSprites[leftBtnIndex];
        _rightRoomButton.GetComponent<Image>().sprite = ItemsSpawner.gameSettingsSO.RoomButtonSprites[rightBtnIndex];

        // Placeholder!
        // Выираем 2 рандомный индекса для модификатора комнаты
        int leftBtnModifierIndex = Random.Range(0, ItemsSpawner.gameSettingsSO.RoomModifiers.Length);
        int rightBtnModifierIndex = Random.Range(0, ItemsSpawner.gameSettingsSO.RoomModifiers.Length);

        // Фильтруем индексы от повторения
        while (leftBtnModifierIndex == rightBtnModifierIndex)
        {
            rightBtnModifierIndex = Random.Range(0, ItemsSpawner.gameSettingsSO.RoomModifiers.Length);
        }

        // Назначаем модификаторы по выбранным индексам на кнопки 
        _leftRoomButton.transform.GetComponentInChildren<Text>().text = ItemsSpawner.gameSettingsSO.RoomModifiers[leftBtnModifierIndex];
        _rightRoomButton.GetComponentInChildren<Text>().text = ItemsSpawner.gameSettingsSO.RoomModifiers[rightBtnModifierIndex];

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
        LevelProgress.ResetResources();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /// <summary>
    /// Загружает главное меню игры
    /// </summary>
    private void LoadMainMenu()
    {
        LevelProgress.ResetResources();
        SceneManager.LoadScene(0);
    }

    /// <summary>
    /// TO DO Загружает карту с уровнями
    /// </summary>
    private void LoadLevelMap()
    {
        // TO DO загружаем сцену с выбором уровней
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
            // Ставим игру на паузу
            Time.timeScale = 0.0f;

            // Активируем попап с очисткой комнаты
            _roomClearedPopup.SetActive(activate);

            // Активируем кнопки для этого попапа
            _leftRoomButton.gameObject.SetActive(activate);
            _rightRoomButton.gameObject.SetActive(activate);

            // Активируем кнопку возврата в главное меню игры
            _mainMenuButton.gameObject.SetActive(activate);

            // Отключаем кнопку с настройками игры, чтобы ее нельзя было нажать снова, пока активен попап
            _gameSettingsButton.enabled = !activate;
        }
        else
        {
            // Снимаем игру с паузы
            Time.timeScale = 1.0f;

            // Отключаем попап с очисткой комнаты
            _roomClearedPopup.SetActive(activate);

            // Отключаем кнопки для этого попапа
            _leftRoomButton.gameObject.SetActive(activate);
            _rightRoomButton.gameObject.SetActive(activate);

            // Отключаем кнопку возврата в главное меню игры
            _mainMenuButton.gameObject.SetActive(activate);

            // Активируем кнопку с настройками игры, чтобы ее можно было нажать снова
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
