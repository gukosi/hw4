using Enemy.Factory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealthBomberFactory : IEnemyFactory
{
    public Enemy CreateEnemy(EnemyData data)
    {
        GameObject instance = GameObject.Instantiate(data.Prefab);
        StealthBomber enemy = instance.GetComponent<StealthBomber>();
        enemy.Configure(data);
        return enemy;
    }
}
