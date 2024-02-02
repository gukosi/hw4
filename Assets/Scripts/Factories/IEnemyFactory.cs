using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy.Factory
{
    public interface IEnemyFactory
    {
        Enemy CreateEnemy(EnemyData data);
    }
}

