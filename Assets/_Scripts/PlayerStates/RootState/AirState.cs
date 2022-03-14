using UnityEngine;

public class AirState : PlayerState
{
    public AirState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, PlayerAnimationHolder playerAnimation) : base(player, stateMachine, playerData, playerAnimation)
    {

    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        Gravity();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    protected void Gravity()
    {
        //OnAir
        verticalVelocity = verticalVelocity + playerData.gravityValue * Time.deltaTime;
        player.SetVelocityY(verticalVelocity);
    }
}
