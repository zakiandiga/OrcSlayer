using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirNormalAttack : ActionState
{
    public PlayerAirNormalAttack(Player player, PlayerStateMachine stateMachine, PlayerData playerData, PlayerAnimationHolder playerAnimation) : base(player, stateMachine, playerData, playerAnimation)
    {
    }

    //Timer calculation variables
    private float minimumAtkTime = 0.6f;
    private string airAttackDurationTimer = "AirAttackTimer";
    private bool isAirAttackDone = true;

    public override void Enter()
    {
        base.Enter();
        minimumAtkTime = playerData.airAttackDuration;

        player.Anim.SetFloat("falling", 0);

        player.SetCurrentDamage(1);
        player.Anim.Play(playerAnimation.normalAirAttack);
        comboCount += 3;
        actionFinished = false;
        isAirAttackDone = false;
    }

    public override void Exit()
    {
        base.Exit();

        //Handling timer if forced state change happen (e.g. takes damage)
        if (Timer.TimerRunning(airAttackDurationTimer))
            Timer.ForceStopTimer(airAttackDurationTimer);

        comboCount = 0;

        actionFinished = true;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(!Timer.TimerRunning(airAttackDurationTimer))
            Timer.Create(AirAttackDoneSwitch, minimumAtkTime, airAttackDurationTimer);

        SpeedChange(moveInputAxis, playerData.airAccelTime);


        if (player.IsGrounded & isAirAttackDone)
        {
            horizontalVelocity = 0;
            stateMachine.ChangeState(player.LandState);
        }

        AttackGravity();
        SetPlayerHorizontalVelocity(horizontalVelocity, playerData.airSpeed);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private void AttackGravity()
    {
        verticalVelocity += (playerData.gravityValue + playerData.airAttackGravityMod) * Time.deltaTime;
        player.SetVelocityY(verticalVelocity);
    }

    private void AirAttackDoneSwitch()
    {
        if (!isAirAttackDone)
            isAirAttackDone = true;
    }
}
