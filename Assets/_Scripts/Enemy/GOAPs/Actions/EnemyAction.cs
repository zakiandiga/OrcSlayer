using UnityEngine;
using SGoap;

public class EnemyAction : BasicAction
{
    protected EnemyBehaviour enemy;
    protected WeaponAnimationEvents weaponAnimation;

    [SerializeField] protected ActionData actionData;

    public override float StaggerTime => actionData != null ? actionData.staggerTime : 0;

    protected void Start()
    {
        enemy = GetComponentInParent<EnemyBehaviour>();
        weaponAnimation = enemy.GetComponentInChildren<WeaponAnimationEvents>();
    }

    public bool IsAvailable()
    {
        if(IsUsable() && enemy.currentPlayer != null)
        {
            if(Mathf.Abs(enemy.transform.position.x - enemy.currentPlayer.position.x) >= actionData.minimumDistance && 
                Mathf.Abs(enemy.transform.position.x - enemy.currentPlayer.position.x) <= actionData.maximumDistance)
            {
                if(AgentData.Agent.States.GetValue("CurrentStamina") > actionData.staminaCost)
                    return true;
            }
        }
        return false;
    }

}
