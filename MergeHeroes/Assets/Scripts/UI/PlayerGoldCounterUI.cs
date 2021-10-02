// Roman Baranov 28.07.2021

using UnityEngine;
using UnityEngine.UI;

public class PlayerGoldCounterUI : MonoBehaviour
{
    #region VARIABLES
    private static Text _counterText = null;// —сылка на компонент текста счетчика
    private static Text _goldPerKillText = null;//Gold per kill text reference

    #endregion

    #region UNITY Methods
    private void Awake()
    {
        _counterText = transform.Find("GoldCounterText").GetComponent<Text>();
        _goldPerKillText = transform.Find("GoldPerKillText").GetComponent<Text>();
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateGoldCounter();
    }
    #endregion

    #region PUBLIC Methods
    /// <summary>
    /// Update player gold counter in UI
    /// </summary>
    public static void UpdateGoldCounter()
    {
        if (_counterText != null)
        {
            _counterText.text = $"{(int)LevelProgress.CurrentGoldAmount}";
        }
    }
   
    /// <summary>
    /// Update gold per kill counter in UI
    /// </summary>
    public static void UpdateGoldPerKill()
    {
        if (_goldPerKillText != null)
        {
            if (Level.CurrentRoom.CurRoomType == Room.RoomType.Boss)
            {
                _goldPerKillText.text = $"Gold per kill: {(int)Level.CurrentRoom.BossRoomGoldPerKill}";
            }
            else
            {
                _goldPerKillText.text = $"Gold per kill: {(int)Level.CurrentRoom.RoomGoldPerKill}";
            }
        }
    }
    #endregion
}
