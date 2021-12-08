using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class OrcController : MonoBehaviour
{
    private NavMeshAgent agent;


    [SerializeField] private float lookRange;
    [SerializeField] private Vector3 eyeOffset; //for drawray

    [SerializeField] private Transform player;

    private float distance;

    [SerializeField] private float patrolSpeed;
    [SerializeField] private float chaseSpeed;
    private Vector3 patrolTargetOffset;
    private Vector3 patrolDestination;
    private bool isPatroling;
    private int patrolCredit;
    private float patrolTimer;

    //look properties
    private const float rightAxis = 90;
    private const float leftAxis = 270;

    public enum EnemyState
    {
        patrol,
        rest,
        chase,
        attack
    }

    public EnemyState enemyState;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    void Update()
    {
        switch (enemyState)
        {
            case EnemyState.patrol:
                //get target path
                GetPatrolPath();

                if(isPatroling)
                {
                    agent.SetDestination(patrolDestination);
                }

                if(transform.position == patrolDestination)
                {
                    isPatroling = false;
                    //StartPatrolTimer(Random.Range(0.8f, 3));
                }
                //move to path

                //set timer to loop the actions

                if (PlayerOnSight())
                {
                    enemyState = EnemyState.chase;
                }

                break;

            case EnemyState.chase:
                //Get player position
                //move to player positon - attackRange
                //if player too far, repeat
                //else, enemyState.attack

                break;

        }
    }

    private bool PlayerOnSight()
    {
        Vector3 rayOrigin = transform.position + eyeOffset;
        Ray ray = new Ray(rayOrigin, transform.forward);
        RaycastHit hit;
        Physics.Raycast(ray, out hit, lookRange);
        if (hit.collider != null && hit.collider.tag == "Player")
        {
            return true;
                
        }
        else
            return false;
    }

    private bool FaceRight()
    {
        float currentFace = transform.rotation.eulerAngles.y;
        if (currentFace == rightAxis)
            return true;

        else 
            return false;
    }

    private void GetPatrolPath()
    {
        if(!isPatroling)
        {
            patrolTargetOffset = Vector3.zero;
            float pathRange = Random.Range(4, 9);
            if (!FaceRight())
            {
                patrolTargetOffset.x = pathRange;
                patrolDestination = transform.position + patrolTargetOffset;
            }
            else if (FaceRight())
            {
                patrolTargetOffset.x = pathRange * -1;
                patrolDestination = transform.position + patrolTargetOffset;
            }
            patrolCredit--;
            isPatroling = true;
        }

    }

    private void StartPatrolTimer (float time)
    {
        patrolTimer += Time.deltaTime;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position + eyeOffset, transform.forward * lookRange);
    }
}
