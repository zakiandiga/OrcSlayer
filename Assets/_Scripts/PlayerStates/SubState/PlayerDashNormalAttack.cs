using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashNormalAttack : ActionState
{
    public PlayerDashNormalAttack(Player player, PlayerStateMachine stateMachine, PlayerData playerData, PlayerAnimationHolder playerAnimation) : base(player, stateMachine, playerData, playerAnimation)
    {

    }

    private float attackDelay = 0.4f;
    private float comboGap = 1.2f;
    private float attackDelayTime;
    private float comboGapTime;

    public override void Enter()
    {
        base.Enter();
        attackDelay = playerData.attackDelay;
        comboGap = playerData.comboGap;
        Debug.Log("Dash Attack!");
        player.Anim.Play(playerAnimation.normalSlideAttack);
        comboGapTime = comboGap;
        attackDelayTime = Time.time + attackDelay;

        comboCount += 2;

        actionFinished = false;
    }

    public override void Exit()
    {
        base.Exit();
        attackDelayTime = 0;
        comboGapTime = 0;

        actionFinished = true;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(Time.time >= attackDelayTime && normalAttackInput && comboCount <= playerData.maxComboCount)
        {
            stateMachine.ChangeState(player.NormalAttackState);
        }

        SpeedChange(0, playerData.deccelTime);
        SetPlayerHorizontalVelocity(horizontalVelocity, playerData.groundSpeed);

        ComboGapTimer();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private void ComboGapTimer()
    {
        comboGapTime -= Time.deltaTime;
        if(comboGapTime <=0)
        {
            comboCount = 0;
            stateMachine.ChangeState(player.IdleState);
        }
    }
}
