using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newActionData", menuName = "Data/ActionData")]
public class ActionData : ScriptableObject
{
    [Header("General Properties")]
    [Tooltip("Time needed for an action to be usable again. Cooldown time is randomize between min and max Time Modifier values")]
    public float cooldownMinTimeModifier = 0;
    public float cooldownMaxTimeModifier = 0;
    [Tooltip("Times needed for the agent to perform the next action. Heavier attacks should have longer staggerTime")]
    public float staggerTime = 0;

    [Header("Attack Action Properties")]
    public int damageAmount = 1;
    [Tooltip("1 per X chance the agent will choose this attack when more than one attack actions available")]
    public int chanceValue = 1;
    public float minimumDistance;
    public float maximumDistance;
    public int staminaCost;
    [Tooltip("Total number of hit within this action (for combo execution)")]
    public int hitCount = 1;
    [Tooltip("Set the length to be the same as the hit count, fill the array with attack animation int code")]
    public int[] hitAnimationCodeList;
    [Tooltip("Minimum number of frame to wait before ending the hit")]
    public int minimumEndingDelayFrame = 4;
    [Tooltip("Maximum number of frame to wait before ending the hit")]
    public int maximumEndingDelayFrame = 10;

    [Header("Movement Action Properties")]
    public float moveSpeed;
    public float minDestinationTreshold;
    public float maxDestinationTreshold;

}

