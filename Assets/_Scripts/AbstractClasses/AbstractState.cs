
using UnityEngine;

public abstract class AbstractState
{
    protected float startTime; //to track how long we're in the state (in case needed)

    public virtual void Enter()
    { }

    public virtual void Exit()
    { }

    public virtual void LogicUpdate()
    { }

    public virtual void PhysicsUpdate()
    { }

}
