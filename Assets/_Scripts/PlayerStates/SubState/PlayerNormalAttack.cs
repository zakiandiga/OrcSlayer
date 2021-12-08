using UnityEngine;

public class PlayerNormalAttack : ActionState
{
    public PlayerNormalAttack(Player player, PlayerStateMachine stateMachine, PlayerData playerData, PlayerAnimationHolder playerAnimation) : base(player, stateMachine, playerData, playerAnimation)
    {

    }

    private float attackDelay = 0.5f;
    private float comboGap = 0.8f;

    private string attackDelayTimer = "AttackDelayTimer";
    private string comboRefreshTimer = "ComboRefreshTimer";
    private bool attackDelayTimerRunning = false;
    private bool comboGapTimerRunning = false;

    public override void Enter()
    {
        base.Enter();
        attackDelay = playerData.attackDelay;
        comboGap = playerData.comboGap;

        //player.Anim.SetTrigger("normalAttack");

        Timer.Create(ReadyingAttack, attackDelay, attackDelayTimer);
        attackDelayTimerRunning = true;

        Timer.Create(ComboRefresh, comboGap, comboRefreshTimer);

        comboCount++;
        AttackAnimation(comboCount);

        actionFinished = false;
    }

    public override void Exit()
    {
        if(attackDelayTimerRunning)
            attackDelayTimerRunning = false;

        base.Exit();
        actionFinished = true;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!attackDelayTimerRunning && normalAttackInput && comboCount < playerData.maxComboCount) // && !isDelay)
        {
            //Debug.Log("Attack number: " + comboCount);

            Timer.ForceStopTimer(comboRefreshTimer);
            Timer.Create(ComboRefresh, comboGap, comboRefreshTimer);

            Timer.Create(ReadyingAttack, attackDelay, attackDelayTimer);
            attackDelayTimerRunning = true;
            comboCount++;
            AttackAnimation(comboCount);
        }
    }

    private void ReadyingAttack()
    {
        attackDelayTimerRunning = false;
    }

    private void ComboRefresh()
    {
        comboCount = 0;
        stateMachine.ChangeState(player.IdleState);
    }

    private void AttackAnimation(int currentComboCount)
    {
        switch (currentComboCount)
        {
            case 1:
                player.Anim.Play(playerAnimation.normalAttack01);
                break;

            case 2:
                player.Anim.Play(playerAnimation.normalAttack02);
                break;

            case 3:
                player.Anim.Play(playerAnimation.normalAttack03);
                break;
        }
    }

}
