using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : GroundState
{
    public PlayerMove(Player player, PlayerStateMachine stateMachine, PlayerData playerData, PlayerAnimationHolder playerAnimation) : base(player, stateMachine, playerData, playerAnimation)
    {

    }

    public enum MoveState
    {
        ready,
        acceleration,
        topSpeed,
        decceleration,
        drifting,
        stop
    }
    private MoveState moveState = MoveState.ready;

    private float tempAxis; //temporary value used in running state

    public override void Enter()
    {
        base.Enter();

        player.Anim.SetBool(playerAnimation.runningBool, true);
        
        if (stateMachine.LastState == player.LandState)
        {
            moveState = MoveState.decceleration;   
        }
        
        else        
            moveState = MoveState.ready;
        
    }

    public override void Exit()
    {
        base.Exit();
        player.Anim.SetBool(playerAnimation.runningBool, false);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        Moving();

        if(!player.IsGrounded)
        {
            stateMachine.ChangeState(player.FallState);
        }

        if(normalAttackInput && moveState != MoveState.drifting)
        {
            stateMachine.ChangeState(player.DashNormalAttackState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private void Moving()
    {
        switch (moveState)
        {
            case MoveState.ready:
                tempAxis = 0;

                ExitFromReady();
                break;

            case MoveState.acceleration:
                SpeedChange(tempAxis, playerData.accelTime);
                    
                ExitFromAcceleration();                
                break;

            case MoveState.topSpeed:
                horizontalVelocity = tempAxis;
                

                ExitFromTopSpeed();
                break;

            case MoveState.decceleration:
                SpeedChange(0, playerData.deccelTime);                

                ExitFromDecceleration();
                break;

            case MoveState.drifting:
                SpeedChange(0, playerData.driftingStrong);                

                ExitFromDrifting();
                break;

            case MoveState.stop:
                tempAxis = 0;
                
                ExitFromStop();                
                break;

            //End of switch
        }

        player.Anim.SetFloat(playerAnimation.runningFloat, Mathf.Abs(horizontalVelocity));
        SetPlayerHorizontalVelocity(horizontalVelocity, playerData.groundSpeed);
    }

    #region Move Micro-State Exit Functions
    private void ExitFromReady()
    {
        if (Mathf.Abs(moveInputAxis) > 0 ) //Exit to acceleration condition
        {
            if (FaceCheck(moveInputAxis) & isTurning == false)
                SetTurning(playerData.instantTurn);

            tempAxis = moveInputAxis;
            moveState = MoveState.acceleration;
        }
        else if (Mathf.Abs(horizontalVelocity) > 0.01f && moveInputAxis == 0)
        {
            moveState = MoveState.decceleration;
        }
    }

    private void ExitFromAcceleration()
    {

        if (Mathf.Abs(tempAxis) > 0.98f && Mathf.Abs(horizontalVelocity) > 0.98f)
        {
            moveState = MoveState.topSpeed;
        }
        else if (moveInputAxis == 0f) //regardless of current processed axis
        {
            moveState = MoveState.decceleration;
        }
    }

    private void ExitFromTopSpeed()
    {
        if ((tempAxis > 0 && moveInputAxis <= 0) || (tempAxis < 0 && moveInputAxis >= 0))
        {
            moveState = MoveState.decceleration;
        }
    }

    private void ExitFromDecceleration()
    {
        if (Mathf.Abs(moveInputAxis) > 0.01f) //Check condition for drifting
        {
            if(FaceCheck(moveInputAxis))
            {
                if (Mathf.Abs(horizontalVelocity) > 0.15f)
                {
                    tempAxis *= -1;
                    SetTurning(playerData.slowTurn);
                    player.Anim.SetTrigger(playerAnimation.driftTrigger);
                    moveState = MoveState.drifting;
                }
                else
                {
                    moveState = MoveState.ready;
                }
                
            }
            else if(!FaceCheck(moveInputAxis))
            {
                tempAxis = moveInputAxis;
                moveState = MoveState.acceleration;
            }
        }
        else if (Mathf.Abs(moveInputAxis) < 0.01f && Mathf.Abs(horizontalVelocity) < 0.03f)
        {
            moveState = MoveState.stop;
        }
    }

    private void ExitFromDrifting()
    {
        if (Mathf.Abs(horizontalVelocity) < 0.1f && !isTurning)// Mathf.Abs(tempAxis) == 1 &&
        {
            moveState = MoveState.stop;
        }
        else if (Mathf.Abs(horizontalVelocity) > 0.1f && !isTurning)// Mathf.Abs(tempAxis) == 1 &&
        {
            moveState = MoveState.ready;
        }
    }

    private void ExitFromStop()
    {
        if (moveInputAxis == 0)
        {
            tempAxis = 0;
            horizontalVelocity = 0;
            stateMachine.ChangeState(player.IdleState);
        }
        else if (Mathf.Abs(moveInputAxis) > 0.01)
        {
            moveState = MoveState.ready;
        }        
    }
    #endregion
}
