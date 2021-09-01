// Roman Baranov 01.09.2021

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu_UI : MonoBehaviour
{
    #region VARIABLES
    private Button _startGameButton = null;// ������ �� ������ ������ ����
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
    /// ��������� ������ ������� ����
    /// </summary>
    private void StartGame()
    {
        // TO DO ��������� + 2, �.�. +1 ����� ����� � ��������
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    #endregion
}
