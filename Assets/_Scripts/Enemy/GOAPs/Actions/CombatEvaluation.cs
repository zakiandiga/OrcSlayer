using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SGoap;

public class CombatEvaluation : EnemyAction
{
    //Goal: Ready attack (decided on either adjust position, attack 1, attack 2, etc)
    ///What needed: 
    /// stamina point DONE
    /// which attack is ready
    /// distance to the player on preperform (if player is too far for the available attacks, adjust position)
    /// health (if too low, there's a chance to flee)
    /// if more than one attack action 
    /// 

    //List of actions to be evaluate during combat
    [SerializeField] private List<AttackAction> actionList = new List<AttackAction>();
    private List<AttackAction> availableActions = new List<AttackAction>();

    [SerializeField] protected StringReference IsAttackAvailable;

    private bool isEvaluating = false;
    private int availableAttackIndex = 0;

    public override EActionStatus Perform()
    {
        if (!enemy.IsAlert())
            return EActionStatus.Failed;

        if (isEvaluating)
            return EActionStatus.Running;
        else
            return EActionStatus.Success;
    }

    public override bool PostPerform()
    {
        return base.PostPerform();
    }

    public override bool PrePerform()
    {
        availableActions.Clear();
        isEvaluating = true;

        FindAttack();

        return base.PrePerform();
    }

    private void FindAttack()
    {
        for (int i = 0; i < actionList.Count; ++i)
        {
            if (actionList[i].IsAvailable())
            {
                availableActions.Add(actionList[i]);
                actionList[i].SetCost(Random.Range(1, actionList[i].ChanceValue));
                //Debug.Log("Add " + actionList[i].Name + " - with chance value 1/" + actionList[i].Cost);
            }
        }

        SetAttackAvailable();        
    }

    private void SetAttackAvailable()
    {
        if (availableActions.Count > 0)
        {
            if (!States.HasState(IsAttackAvailable.Value))
                States.AddState(IsAttackAvailable.Value, 1);
            else
                States.ModifyState(IsAttackAvailable.Value, 1);
        }
        else
        {
            if (States.HasState(IsAttackAvailable.Value))
                States.RemoveState(IsAttackAvailable.Value);
        }

        isEvaluating = false;
        /*
        Debug.Log("current available actions: " + availableActions.Count);
        if (availableActions.Count > 0)
        {
            availableAttackIndex = Random.Range(0, availableActions.Count);
            isEvaluating = false;
        }
        else
            availableAttackIndex = 0;
        */
    }
}
