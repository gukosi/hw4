using Enemy.Factory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoutCarFactory : IEnemyFactory
{
    public Enemy CreateEnemy(EnemyData data)
    {
        GameObject instance = GameObject.Instantiate(data.Prefab);
        ScoutCar enemy = instance.GetComponent<ScoutCar>();
        enemy.Configure(data);
        return enemy;
    }
}
