// Roman Baranov 08.09.2021

public class Level
{
    private int _maxRooms = 4;// ������������ ���������� ������ �� ������

    private int _maxMosterWavePerRoom = 3;// ������������ ���������� ���� �������� � �������

    private int _maxMonstersPerWave = 3;// ������������ ���������� �������� � �����

    private int _bossesCount = 1;// ���������� ������ �� ������

    private LevelModificator.Modificator _levelsmodificator;// ����������� ������

    /// <summary>
    /// ����������� ������
    /// </summary>
    /// <param name="maxRooms">������������ ���������� ������ �� ������</param>
    /// <param name="maxMonsterWavePerRoom">������������ ���������� ���� �������� � �������</param>
    /// <param name="maxMonstersPerWave">������������ ���������� �������� � �����</param>
    /// <param name="bossesCount">���������� ������ �� ������</param>
    /// <param name="levelsmodificator">����������� ������</param>
    public Level(int maxRooms, int maxMonsterWavePerRoom, int maxMonstersPerWave, int bossesCount, LevelModificator.Modificator levelsmodificator)
    {
        _maxRooms = maxRooms;
        _maxMosterWavePerRoom = maxMonsterWavePerRoom;
        _maxMonstersPerWave = maxMonstersPerWave;
        _bossesCount = bossesCount;
        _levelsmodificator = levelsmodificator;
    }
}
