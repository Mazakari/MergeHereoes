// Roman Baranov 08.09.2021

public class RoomModificator
{
    private float _monsterDamageIncreaseAmount = 0.1f;
    /// <summary>
    /// ������� ���������� ����� ��������
    /// </summary>
    public float MonsterDamageIncreaseAmount { get { return _monsterDamageIncreaseAmount; } }


    private float _monsterHpIncreaseAmount = 0.1f;
    /// <summary>
    /// ������� ���������� �������� ��������
    /// </summary>
    public float MonsterHpIncreaseAmount { get { return _monsterHpIncreaseAmount; } }


    private float _monsterArmourIncreaseAmount = 0.1f; 
    /// <summary>
    /// ������� ���������� ����� �������� 
    /// </summary>
    public float MonsterArmourIncreaseAmount { get { return _monsterArmourIncreaseAmount; } }


    private float _goldFromBossDecrease = 0.1f;// 
    /// <summary>
    /// ������� ���������� ������, ����������� �� �������� �����
    /// </summary>
    public float GoldFromBossDecrease { get { return _goldFromBossDecrease; } }


    public enum Modificator
    {
        None,
        MonsterDamageIncrease,
        MonsterHpIncrease,
        MonsterArmourIncrease,
        GoldFromBossDecrease
    }
}
