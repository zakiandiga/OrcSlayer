using UnityEngine;

[CreateAssetMenu(fileName = "newAnimationsHolder", menuName = "Data/PlayerData/AnimationHolder")]
public class PlayerAnimationHolder : AnimationHolder
{    
    [Header("Attack")]
    [Tooltip("String of normal attack STATE name in the Object's Animator")]
    public string normalAttack01;
    [Tooltip("String of 2nd normal attack STATE name in the Object's Animator")]
    public string normalAttack02;
    [Tooltip("String of 3rd normal attack STATE name in the Object's Animator")]
    public string normalAttack03;
    [Tooltip("String of dash normal attack STATE name in the Object's Animator")]
    public string normalSlideAttack;
    [Tooltip("String of air normal attack STATE name in the Object's Animator")]
    public string normalAirAttack;

    [Header("Movement")]
    public string roll;
    [Tooltip("string of jumping animation STATE NAME")]
    public string jumpTrigger;
    [Tooltip("string of landing animation STATE NAME")]
    public string landTrigger;
    [Tooltip("string of falling animation PARAMETER NAME")]
    public string fallFloat;

    [Tooltip("string of falling animation PARAMETER NAME")]
    public string runningFloat;
    [Tooltip("string of falling animation PARAMETER NAME")]
    public string runningBool;

    public string driftTrigger;

    [Header("Other")]
    [Tooltip("string of take damage animation STATE NAME")]
    public string takeDamage;
}
