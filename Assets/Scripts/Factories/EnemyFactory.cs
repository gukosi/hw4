using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory
{
    public Enemy CreateEnemy(EnemyData data)
    {
        switch (data.Type)
        {
            case EnemyType.StealthBomber:
                return new StealthBomberFactory().CreateEnemy(data);
            case EnemyType.Soldier:
                return new SoldierFactory().CreateEnemy(data);
            case EnemyType.ScoutCar:
                return new ScoutCarFactory().CreateEnemy(data);
            default:
                throw new System.ArgumentException("Unknown enemy type");
        }
    }
}

