using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AnimationManager : MonoBehaviour
{
    protected Animator anim;    

    protected void Start()
    {
        anim = GetComponentInChildren<Animator>();   
    }

    public virtual void SetRunningFloat(float value)
    {
        
    }

    public virtual void SetFallingFloat(float value)
    {
        
    }

    public virtual void SetRunningBool(bool isRunning)
    {
    }

    public virtual void SetTriggerJump()
    {
    }

    public virtual void SetTriggerLand()
    {
    }

}
