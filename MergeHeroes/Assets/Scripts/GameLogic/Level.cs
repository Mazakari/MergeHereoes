// Roman Baranov 08.09.2021

public class Level
{
    private int _maxRooms = 4;// Максимальное количество комнат на уровне

    private int _maxMosterWavePerRoom = 3;// Максимальное количество волн монстров в комнате

    private int _maxMonstersPerWave = 3;// Максимальное количество монстров в волне

    private int _bossesCount = 1;// Количество боссов на уровне

    private LevelModificator.Modificator _levelsmodificator;// Модификатор уровня

    /// <summary>
    /// Конструктор уровня
    /// </summary>
    /// <param name="maxRooms">Максимальное количество комнат на уровне</param>
    /// <param name="maxMonsterWavePerRoom">Максимальное количество волн монстров в комнате</param>
    /// <param name="maxMonstersPerWave">Максимальное количество монстров в волне</param>
    /// <param name="bossesCount">Количество боссов на уровне</param>
    /// <param name="levelsmodificator">Модификатор уровня</param>
    public Level(int maxRooms, int maxMonsterWavePerRoom, int maxMonstersPerWave, int bossesCount, LevelModificator.Modificator levelsmodificator)
    {
        _maxRooms = maxRooms;
        _maxMosterWavePerRoom = maxMonsterWavePerRoom;
        _maxMonstersPerWave = maxMonstersPerWave;
        _bossesCount = bossesCount;
        _levelsmodificator = levelsmodificator;
    }
}
