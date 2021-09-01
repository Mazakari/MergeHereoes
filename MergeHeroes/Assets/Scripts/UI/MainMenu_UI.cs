// Roman Baranov 01.09.2021

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu_UI : MonoBehaviour
{
    #region VARIABLES
    private Button _startGameButton = null;// Ссылка на кнопку старта игры
    #endregion

    #region UNITY Methods
    private void Awake()
    {
        _startGameButton = transform.Find("StartGameButton").GetComponent<Button>();
        _startGameButton.onClick.AddListener(StartGame);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnDestroy()
    {
        _startGameButton.onClick.RemoveAllListeners();

    }
    #endregion

    #region PRIVATE Methods
    /// <summary>
    /// Загружает первый уровень игры
    /// </summary>
    private void StartGame()
    {
        // TO DO Загружать + 2, т.к. +1 будет карта с уровнями
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    #endregion
}
