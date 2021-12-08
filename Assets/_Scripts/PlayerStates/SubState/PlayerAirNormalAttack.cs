using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirNormalAttack : ActionState
{
    public PlayerAirNormalAttack(Player player, PlayerStateMachine stateMachine, PlayerData playerData, PlayerAnimationHolder playerAnimation) : base(player, stateMachine, playerData, playerAnimation)
    {
    }

    private float minimumAtkTime = 0.6f;

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Normal AIR ATTACK!");
        minimumAtkTime = playerData.airAttackDuration;

        player.Anim.SetFloat("falling", 0);
        player.Anim.Play(playerAnimation.normalAirAttack);
        comboCount += 3;
        actionFinished = false;
    }

    public override void Exit()
    {
        base.Exit();

        comboCount = 0;
        minimumAtkTime = 0;
        Debug.Log("Exit AIR ATTACK");

        actionFinished = true;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        minimumAtkTime -= Time.deltaTime;

        AttackGravity();

        SpeedChange(0, playerData.airAccelTime);
        SetPlayerHorizontalVelocity(horizontalVelocity, playerData.airSpeed);

        if (player.IsGrounded & minimumAtkTime <= 0)
        {
            stateMachine.ChangeState(player.LandState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private void AttackGravity()
    {
        //verticalVelocity = verticalVelocity + playerData.gravityValue + playerData.airAttackGravityMod * Time.deltaTime;
        //player.SetVelocityY(verticalVelocity);

        verticalVelocity = verticalVelocity + (playerData.gravityValue + playerData.airAttackGravityMod) * Time.deltaTime;
        player.SetVelocityY(verticalVelocity);
    }
}
