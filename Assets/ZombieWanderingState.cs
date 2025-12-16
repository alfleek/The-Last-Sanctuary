using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieWanderingState : StateMachineBehaviour
{
    [Header("Wander Settings")]
    public float wanderTime = 20f;
    public float wanderRadius = 20f;
    public float wanderSpeed = 0.4f;
    public float minIdleAtPoint = 3f;
    public float maxIdleAtPoint = 5f;

    [Header("Detection")]
    public float checkInterval = 0.2f;
    public float detectionRadiusMult = 1f;
    public Transform player;
    private NavMeshAgent agent;
    private Transform zombieTransform;

    private PlayerDetection playerDetection;

    private float checkTimer;
    public float wanderTimer;

    public float idleTimer;
    private float idleDuration;


    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.GetComponent<NavMeshAgent>();
        zombieTransform = animator.transform;

        agent.isStopped = false;
        agent.speed = wanderSpeed;

        checkTimer = 0f;
        wanderTimer = 0f;

        if (player == null)
        {
            GameObject playerGO = GameObject.FindGameObjectWithTag("Player");
            if (playerGO != null)
            {
                player = playerGO.transform;
                playerDetection = playerGO.GetComponent<PlayerDetection>();
            }
        }

        idleTimer = 0f;
        ChooseNewDestination();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Idle when time is up
        wanderTimer += Time.deltaTime;
        if (wanderTimer > wanderTime)
        {
            animator.SetBool("Idle", true);
        }


        //Check for player
        checkTimer += Time.deltaTime;
        if (checkTimer >= checkInterval)
        {
            checkTimer = 0f;
            DetectPlayer(animator);
        }

        //Wander Pathfinding 
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance + 0.1f)
        {
            if (!agent.isStopped)
            {
                agent.isStopped = true;
                idleTimer = 0f;
                idleDuration = Random.Range(minIdleAtPoint, maxIdleAtPoint);
            }
            // zombie reached its point; wait a bit, then pick a new one
            idleTimer += Time.deltaTime;
            if (idleTimer >= idleDuration)
            {
                ChooseNewDestination();
            }
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (agent.isActiveAndEnabled) agent.ResetPath(); //Stop the current pathfinding when we go to idle or chase
        animator.SetBool("Wandering", false);
    }

    private void ChooseNewDestination()
    {

        if (zombieTransform == null || agent == null)
            return;

        agent.isStopped = false;

        Vector3 newPos;
        if (RandomPointOnNavmesh(zombieTransform.position, wanderRadius, out newPos))
        {
            agent.SetDestination(newPos);
        }
        else
        {
            // Fallback: just stand there if no random point found
            agent.SetDestination(zombieTransform.position);
        }
    }

    private bool RandomPointOnNavmesh(Vector3 origin, float distance, out Vector3 result)
    {
        Vector2 randomCircle = Random.insideUnitCircle * distance;
        Vector3 randomDirection = new Vector3(randomCircle.x, 0f, randomCircle.y);
        randomDirection += origin;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, distance, NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }

        result = origin;
        return false;
    }

    private void DetectPlayer(Animator animator)
    {
        if (player == null || playerDetection == null)
            return;
        float detectionRadius = playerDetection.GetDetectionRadius() * detectionRadiusMult;

        float sqrDetectionRadius = detectionRadius * detectionRadius;

        Vector3 toPlayer = player.position - animator.transform.position;
        if (toPlayer.sqrMagnitude <= sqrDetectionRadius)
        {
            animator.SetBool("Chasing", true);
        }
    }
}
