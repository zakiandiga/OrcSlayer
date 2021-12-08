using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using SGoap;

public class EnemyBehaviour : MonoBehaviour, IDamageHandler
{
    public int MaxHealth { get { return enemyData.maxHP; } private set { } }
    public int CurrentHealth { get { return _currentHealth; } private set { } }

    public Vector3 EnemyPosition { get { return transform.position; } private set { } }
    public float MaxWanderRange { get { return enemyData.maxWanderRange; } private set { } }
    public float MinWanderRange { get { return enemyData.minWanderRange; } private set { } }
    public float WanderSpeed { get { return enemyData.wanderSpeed; } private set { } }
    public float AggroSpeed { get { return enemyData.aggroSpeed; } private set { } }
    public float AggroAlertColliderRadius { get { return enemyData.aggroAlertColliderRadius; } private set { } }
    public float WanderAlertColliderRadius { get { return enemyData.wanderAlertColliderRadius; } private set { } }
    public float RestAlertColliderRadius { get { return enemyData.restAlertColliderRadius; } private set { } }
    public float UnaggroDelay { get { return enemyData.unaggroDelay; } private set { } }

    public Transform Player { get; private set; }    
    public NavMeshPath AgentPath { get { return navAgent.path; } private set { } }
    public EnemyAnimationManager AnimManager { get; private set; }

    #region private Components
    private NavMeshAgent navAgent;
    private SphereCollider playerSensorCollider;
    private Agent goapAgent;
    [SerializeField] private EnemyData enemyData;
    [SerializeField] private GameObject alertCollider;
    #endregion

    #region private Variables
    private int _currentHealth = 10;
    private string aggroColliderTimer = "PlayerExitAggro";

    #endregion

    public enum EnemyType //variation in behavior data
    {
        neutral,
        hostile
    }

    [SerializeField] private EnemyType enemyType = EnemyType.neutral;

    public static event Action<GameObject, int> OnEnemyTakesDamage;
    public static event Action<GameObject> OnEnemyDies;

    private void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        goapAgent = GetComponent<Agent>();
        AnimManager = GetComponent<EnemyAnimationManager>();  
        playerSensorCollider = alertCollider.GetComponent<SphereCollider>();
    }

    #region Player Sensor
    public bool IsAlert() => goapAgent.States.HasState("IsAlert");

    public void SetAlertColliderRadius(float targetRadius) => playerSensorCollider.radius = targetRadius;

    public void SetPlayerOn(Transform player)
    {
        if (!IsAlert())
        {
            Player = player;
            goapAgent.States.AddState("IsAlert", 1);
        }
        else        
            Timer.ForceStopTimer(aggroColliderTimer);
    }

    public void SetPlayerOff(Transform player) => Timer.Create(PlayerExitAggro, UnaggroDelay, aggroColliderTimer);

    private void PlayerExitAggro()
    {
        Player = null;
        goapAgent.States.RemoveState("IsAlert");
    }

    #endregion

    #region NavMesh Movement Functions
    public void SetDestination(Vector3 destination, float speed)
    {
        navAgent.speed = speed;      
        navAgent.SetDestination(destination);
    }

    public void SetStopNavMesh() => navAgent.ResetPath();

    public float NavRemainingDistance() => navAgent.remainingDistance;
    #endregion

    #region Interface implementations
    public void Die()
    {
        OnEnemyDies?.Invoke(this.gameObject);

        //temporary dies
        gameObject.SetActive(false);
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        OnEnemyTakesDamage?.Invoke(this.gameObject, damage);

        if (_currentHealth <= 0)
            Die();
            

    }
    #endregion
}
