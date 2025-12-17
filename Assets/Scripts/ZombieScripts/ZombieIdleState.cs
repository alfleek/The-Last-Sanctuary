using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieIdleState : StateMachineBehaviour
{
    public float idleTime = 15f;

    public float detectionRadiusMult = 0.5f;
    public float checkInterval = 0.2f;
    public Transform player;
    private PlayerDetection playerDetection;

    private float checkTimer;
    private float idleTimer;


    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        checkTimer = 0f;
        idleTimer = 0f;

        if (player == null)
        {
            GameObject playerGO = GameObject.FindGameObjectWithTag("Player");
            if (playerGO != null)
            {
                player = playerGO.transform;
                playerDetection = playerGO.GetComponent<PlayerDetection>();
            }
        }



    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        idleTimer += Time.deltaTime;
        if (idleTimer > idleTime)
        {
            animator.SetBool("Wandering", true);
        }

        if (player == null || playerDetection == null)
            return;

        checkTimer += Time.deltaTime;
        if (checkTimer >= checkInterval)
        {
            checkTimer = 0f;
            DetectPlayer(animator);
        }


    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("Idle", false);
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
