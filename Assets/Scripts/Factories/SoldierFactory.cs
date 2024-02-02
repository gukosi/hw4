using Enemy.Factory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierFactory : IEnemyFactory
{
    public Enemy CreateEnemy(EnemyData data)
    {
        GameObject instance = GameObject.Instantiate(data.Prefab);
        Soldier enemy = instance.GetComponent<Soldier>();
        enemy.Configure(data);
        return enemy;
    }
}
