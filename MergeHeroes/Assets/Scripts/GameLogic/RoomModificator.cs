// Roman Baranov 08.09.2021

using UnityEngine;

public class RoomModificator
{
    #region VARIABLES
    private float _monsterDamageIncreaseAmount = 0.1f;
    /// <summary>
    /// Monster damage increase modificator
    /// </summary>
    public float MonsterDamageIncreaseAmount { get { return _monsterDamageIncreaseAmount; } }


    private float _monsterHpIncreaseAmount = 0.1f;
    /// <summary>
    /// Monster HP increase modificator
    /// </summary>
    public float MonsterHpIncreaseAmount { get { return _monsterHpIncreaseAmount; } }


    private float _monsterArmourIncreaseAmount = 0.1f;
    /// <summary>
    /// Monster armour increase modificator 
    /// </summary>
    public float MonsterArmourIncreaseAmount { get { return _monsterArmourIncreaseAmount; } }


    private float _goldFromBossDecrease = 0.1f;// 
    /// <summary>
    /// Gold from boss kill decrease modificator
    /// </summary>
    public float GoldFromBossDecrease { get { return _goldFromBossDecrease; } }

    /// <summary>
    /// Room modificators
    /// </summary>
    public enum Modificator
    {
        None,
        MonsterDamageIncrease,
        MonsterHpIncrease,
        MonsterArmourIncrease,
        GoldFromBossDecrease
    }
    #endregion

    #region PUBLIC Methods
    /// <summary>
    /// Set room modificator
    /// </summary>
    /// <param name="modificator">Room modificator that need to be set</param>
    public void SetRoomModificator(Modificator modificator)
    {
        switch (modificator)
        {
            case Modificator.None:
                Debug.Log("Modificator = NONE");
                break;

            case Modificator.MonsterDamageIncrease:
                MonsterDamageIncreaseModificator();
                break;

            case Modificator.MonsterHpIncrease:
                MonsterHpIncreaseModificator();
                break;

            case Modificator.MonsterArmourIncrease:
                MonsterArmourIncreaseModificator();
                break;

            case Modificator.GoldFromBossDecrease:
                GoldFromBossDecreaseModificator();
                break;

            default:
                Debug.Log("Modificator not found");
                break;
        }
    }
    #endregion

    #region PRIVATE Methods
    private void MonsterDamageIncreaseModificator()
    {

    }

    private void MonsterHpIncreaseModificator()
    {

    }

    private void MonsterArmourIncreaseModificator()
    {

    }

    private void GoldFromBossDecreaseModificator()
    {

    }

    #endregion

}
