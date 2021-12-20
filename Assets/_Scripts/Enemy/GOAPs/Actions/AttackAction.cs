using SGoap;
using System.Collections;
using UnityEngine;

public class AttackAction : EnemyAction
{
    public int ChanceValue { get { return actionData.chanceValue; } }

    private bool actionOngoing = false;
    private int currentHitPerformed = 0;
    private int delayTime = 0;


    private IEnumerator posthitDelay;

    [SerializeField] private StringReference isAttackAvailable;

    public override float CooldownTime => cooldownTime;

    private float cooldownTime = 0;

    public void SetCost(int actionCost)
    {
        Cost = actionCost;
    }

    public override EActionStatus Perform()
    {
        // return failed if "IsStaggered" when it's implemented

        if (actionOngoing)           
            return EActionStatus.Running;

        else
        {
            StopCoroutine(posthitDelay);
            return EActionStatus.Success;
        }
    }

    public override bool PostPerform()
    {
        currentHitPerformed = 0;
        States.RemoveState(isAttackAvailable.Value);
        return base.PostPerform();
    }

    public override bool PrePerform()
    {
        actionOngoing = true;
        cooldownTime = Random.Range(actionData.cooldownMinTimeModifier, actionData.cooldownMaxTimeModifier);
        StartAttack(currentHitPerformed);
        
        return base.PrePerform();
    }

    private void StartAttack(int hitPerformed)
    {
        enemy.AnimManager.SetAttack(actionData.hitAnimationCodeList[hitPerformed]);        
        weaponAnimation.OnAttackExecuting += PostHit;
    }

    private void PostHit(bool isRunning)
    {
        if(!isRunning)
        {
            weaponAnimation.OnAttackExecuting -= PostHit;
            currentHitPerformed += 1;
            delayTime = Random.Range(actionData.minimumEndingDelayFrame, actionData.maximumEndingDelayFrame);
            
            if (posthitDelay != null)
                StopCoroutine(posthitDelay);

            posthitDelay = FrameCountDown();
            StartCoroutine(posthitDelay);                
        }
    }

    private IEnumerator FrameCountDown()
    {
        for (int i = 0; i < delayTime; i++)
            yield return null;

        if (currentHitPerformed < actionData.hitCount)
            StartAttack(currentHitPerformed);            
        
        else
        {
            enemy.StaminaUsed(actionData.staminaCost);
            actionOngoing = false;        
        }
    }
}
