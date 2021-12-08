using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdle : GroundState
{
    public PlayerIdle(Player player, PlayerStateMachine stateMachine, PlayerData playerData, PlayerAnimationHolder playerAnimation) : base(player, stateMachine, playerData, playerAnimation)
    {

    }

    private float delayAfterLanding = 0.35f;
    private float delayTimer;
    private bool ActionReady => delayTimer <= 0;


    public override void Enter()
    {
        base.Enter();

        player.Anim.SetFloat("running", 0);

        if (stateMachine.LastState == player.LandState)
        {
            delayTimer = delayAfterLanding;
        }
        else delayTimer = 0;
    }

    public override void Exit()
    {
        base.Exit();
        delayTimer = 0;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(!ActionReady)
        {
            delayTimer -= Time.deltaTime;
        }

        if (ActionReady)
        {
            if (normalAttackInput && comboCount < playerData.maxComboCount)
            {
                if (isTurning)
                {
                    isTurning = false;
                    ForceTurning();
                }

                stateMachine.ChangeState(player.NormalAttackState);
            }

            if (Mathf.Abs(moveInputAxis) > 0)
            {
                stateMachine.ChangeState(player.MoveState);
            }
        }

        if (!player.IsGrounded)
        {
            stateMachine.ChangeState(player.FallState);            
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
