// Roman Baranov 01.09.2021

using UnityEngine;
using UnityEngine.UI;

public class BuildVersion : MonoBehaviour
{
    #region VARIABLES
    private Text _buildVersionText = null;
    #endregion

    #region UNITY Methods
    private void Awake()
    {
        _buildVersionText = GetComponent<Text>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _buildVersionText.text = $"Ver {Application.version}";
    }
    #endregion
}
