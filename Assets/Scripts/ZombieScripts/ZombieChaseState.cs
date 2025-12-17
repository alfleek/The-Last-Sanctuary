using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieChaseState : StateMachineBehaviour
{

    public Transform player;
    private NavMeshAgent agent;

    public float chaseSpeed = 1.5f;

    public float stopChasingDistance = 21f;
    public float attackingDistance = 2f;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (player == null)
        {
            GameObject playerGO = GameObject.FindGameObjectWithTag("Player");
            if (playerGO != null)
            {
                player = playerGO.transform;
            }
        }
        agent = animator.GetComponent<NavMeshAgent>();

        agent.speed = chaseSpeed;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(agent.isActiveAndEnabled) agent.SetDestination(player.position);
        //animator.transform.LookAt(player);

        float distanceFromPlayer = Vector3.Distance(player.position, animator.transform.position);

        if (distanceFromPlayer > stopChasingDistance)
        {
            animator.SetBool("Wandering", true);
        }

        if (distanceFromPlayer < attackingDistance)
        {
            animator.SetBool("Attacking", true);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(agent.isActiveAndEnabled) agent.ResetPath();
        animator.SetBool("Chasing", false);
    }
}
