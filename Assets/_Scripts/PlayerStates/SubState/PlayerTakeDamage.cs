using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTakeDamage : ActionState
{
    public PlayerTakeDamage(Player player, PlayerStateMachine stateMachine, PlayerData playerData, PlayerAnimationHolder playerAnimation) : base(player, stateMachine, playerData, playerAnimation)
    {
    }

    public override void Enter()
    {
        base.Enter();

        //Debug.Log("Player take damage from " + stateMachine.LastState);
        actionFinished = false;

        ActionFinishDebug();
    }

    public override void Exit()
    {
        base.Exit();

        actionFinished = true;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    private void ActionFinishDebug()
    {
        stateMachine.ChangeState(player.IdleState);
    }
}
