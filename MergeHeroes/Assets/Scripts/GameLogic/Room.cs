// Roman Baranov 14.09.2021

using UnityEngine;

public class Room
{
    #region VARIABLES
    // ��� �������

    // ������� �� �������

    private string _roomName = "";// �������� �������
    /// <summary>
    /// �������� �������
    /// </summary>
    public string RoomName { get { return _roomName; } }

    private RoomModificator.Modificator _RoomModificator;// ����������� �������

    private int _curRoomNumber = 0;
    /// <summary>
    /// ������� ����� �������
    /// </summary>
    public int CurRoomNumber { get { return _curRoomNumber; } }

    private float _roomHealth = 100f;
    /// <summary>
    /// ����� ��� �������� ��� �������� � �������
    /// </summary>
    public float RoomHealth { get { return _roomHealth; } }

    private int _curWaveNumber = 0;
    /// <summary>
    /// ����� ������� ����� ��������
    /// </summary>
    public int CurWaveNumber { get { return _curWaveNumber; } set { _curWaveNumber = value; } }

    private int _curMonstersInWave = 0;
    /// <summary>
    /// ������� ���������� �������� � ������� �����
    /// </summary>
    public int CurMonstersInWave { get { return _curMonstersInWave; } set { _curMonstersInWave = value; } }

    private Sprite _roomBackgroundSprite = null;// ������ ���� �������
    #endregion

    #region CONSTRUCTOR
    /// <summary>
    /// ����������� �������
    /// </summary>
    /// <param name="roomName">�������� �������</param>
    /// <param name="curRoomNumber">������� ����� �������</param>
    /// <param name="roomHealth">��� �������� ��� �������� � �������</param>
    /// <param name="curMonsterWave">������� ���������� ���� �������� � �������</param>
    /// <param name="curMonstersInWave">������� ���������� �������� � ������� �����</param>
    /// <param name="roomBackgroundSprite">������ ���� �������</param>
    /// <param name="roomModificator">����������� �������</param>
    public Room(string roomName, int curRoomNumber, float roomHealth, int curMonstersInWave, Sprite roomBackgroundSprite, RoomModificator.Modificator roomModificator)
    {
        _roomName = roomName;
        _curRoomNumber = curRoomNumber;

        _roomHealth = roomHealth;

        _curWaveNumber = 1;
        _curMonstersInWave = curMonstersInWave;

        _roomBackgroundSprite = roomBackgroundSprite;

        _RoomModificator = roomModificator;
    }
    #endregion
}
