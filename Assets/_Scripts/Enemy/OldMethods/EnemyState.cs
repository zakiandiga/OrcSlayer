using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : AbstractState
{
    protected Enemy enemy;
    protected EnemyStateMachine stateMachine;
    protected EnemyData enemyData;

    public EnemyState(Enemy enemy, EnemyStateMachine stateMachine, EnemyData enemyData)
    {

    }
}
