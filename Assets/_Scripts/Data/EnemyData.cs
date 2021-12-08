using UnityEngine;

[CreateAssetMenu(fileName = "newEnemyData", menuName = "Data/EnemyData/Properties")]
public class EnemyData : ScriptableObject
{
    public string enemyName;
    public EnemyType enemyType;

    [Header("Enemy Stats")]
    public int maxHP = 10;
    public int maxStamina = 10;

    [Header("Movement Property")]
    public float maxWanderRange = 8;
    public float minWanderRange = 5;
    public float wanderSpeed = 3;
    public float aggroSpeed = 8;
    public float wanderAlertColliderRadius = 7;
    public float aggroAlertColliderRadius = 9;
    public float restAlertColliderRadius = 3;
    [Tooltip("Time needed for the enemy to stop aggro when player exit the AlertCollider")]
    public float unaggroDelay = 3.5f;

}

public enum EnemyType
{
    melee,
    range,
    etc
}
