// Roman Baranov 14.09.2021

using UnityEngine;

public class Room
{
    // ��� �������

    // ������� �� �������

    private string _roomName = "";// �������� �������

    private int _curRoomNumber = 0;// ������� ����� �������

    private float _roomHealth = 100f;// ��� �������� ��� �������� � �������

    private int _curMonsterWave = 0;// ������� ���������� ���� �������� � �������

    private int _curWaveNumber = 0;// ����� ������� ����� ��������

    private int _curMonstersInWave = 0;// ������� ���������� �������� � ������� �����

    private Sprite _roomBackgroundSprite = null;// ������ ���� �������

    /// <summary>
    /// ����������� �������
    /// </summary>
    /// <param name="roomName">�������� �������</param>
    /// <param name="curRoomNumber">������� ����� �������</param>
    /// <param name="roomHealth">��� �������� ��� �������� � �������</param>
    /// <param name="curMonsterWave">������� ���������� ���� �������� � �������</param>
    /// <param name="curMonstersInWave">������� ���������� �������� � ������� �����</param>
    /// <param name="roomBackgroundSprite">������ ���� �������</param>
    public Room(string roomName, int curRoomNumber, float roomHealth, int curMonsterWave, int curMonstersInWave, Sprite roomBackgroundSprite)
    {
        _roomName = roomName;
        _curRoomNumber = curRoomNumber;

        _roomHealth = roomHealth;

        _curMonsterWave = curMonsterWave;
        _curWaveNumber = 0;
        _curMonstersInWave = curMonstersInWave;

        _roomBackgroundSprite = roomBackgroundSprite;
    }
}
