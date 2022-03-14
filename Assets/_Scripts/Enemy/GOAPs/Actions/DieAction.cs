using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SGoap;

public class DieAction : EnemyAction
{   
    public override bool PrePerform()
    {        
        return true;
    }

    public override EActionStatus Perform()
    {
        if (!States.HasState("isDead"))
            return EActionStatus.Success;

        else
            return EActionStatus.Running;
    }

    public override bool PostPerform()
    {
        return base.PostPerform();
    }

}
