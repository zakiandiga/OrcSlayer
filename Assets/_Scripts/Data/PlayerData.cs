using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/PlayerData/BaseData")]
public class PlayerData : ScriptableObject
{
    public string dataOwner;

    [Header("Player Stats")]
    public int maxHP = 10;
    public int maxStamina = 10;

    [Header("Ground Speed")]
    [Range(0, 50)]
    public float groundSpeed = 15f;
    [Range(0, 50)]
    public float accelTime = 20f;
    [Range(0, 50)]
    public float deccelTime = 18.4f;
    [Range(0, 50)]
    public float driftingStrong = 9.5f;
    [Range(0, 50)]
    public float driftingWeak = 5f;
    public float driftingTreshold = 1;

    [Header("Air Speed")]
    [Range(0, 50)]
    public float airSpeed = 8f;
    [Range(0, 50)]
    public float airAccelTime = 5f;

    [Header("Jump & Gravity")]
    [Range(0, 20)]
    public float jumpHeight = 8;
    public float jumpConst = -1f;
    public float gravityValue = -98.5f;
    public float airAttackGravityMod = -10f;
    public int maxJumpCount = 2;
    public float recoveryFall = 0.5f;
    public float recoveryAirNormalAttack = 0.8f;


    [Header("Turning Speed")]
    [Range(30, 200)]
    public float instantTurn = 198f;
    [Range(30, 200)]
    public float quickTurn = 50f;
    [Range(30, 200)]
    public float slowTurn = 35f;

    [Header("Combat Properties")]
    public int maxComboCount = 3;
    [Range(0,3)]
    public float attackDelay = 0.5f;
    [Range(0,3)]
    public float comboGap = 0.8f;
    [Range(0, 3)]
    public float airAttackDuration = 0.5f;

    public float staggerTime = 0.8f;
    
}

    ////Default Initial
/*
public float groundSpeed = 15f;
public float airSpeed = 8f;
public float airAccelTime = 5f;
public float accelTime = 20f;
public float deccelTime = 18.4f;
public float driftingStrong = 9.5f;
public float driftingWeak = 5f;
public float turningSpeed = 500f;

public float jumpHeight = 8;
public float jumpConst = -1f;
public float gravityValue = -98.5f;
public int maxJumpCount = 2;
*/
