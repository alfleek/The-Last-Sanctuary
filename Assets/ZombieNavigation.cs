using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieNavigation : MonoBehaviour
{
    public Transform player;
    public bool playerAggro;
    private NavMeshAgent agent;
    public Animator animator;
    private float speed;
    [SerializeField] private float attackTimer;
    private float lastAttack;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (playerAggro)
        {
            agent.destination = player.position;
        }
        lastAttack = attackTimer;
    }

    // Update is called once per frame
    void Update()
    {
        lastAttack -= Time.deltaTime;

        speed = agent.velocity.magnitude;
        if (playerAggro)
        {
            agent.destination = player.position;
            
        }

        //TEMP ANIMATION LOGIC: To be replaced
        if (speed <= 0.01f)
        {
            animator.SetBool("Walking", false);
            animator.SetBool("Running", false);
            animator.SetBool("Idle", true);
        }
        else if (speed <= 1.5f)
        {
            animator.SetBool("Idle", false);
            animator.SetBool("Running", false);
            animator.SetBool("Walking", true);
        }
        else
        {
            animator.SetBool("Walking", false);
            animator.SetBool("Idle", false);
            animator.SetBool("Running", true);
        }

        if (agent.remainingDistance < 2.5f && lastAttack < 0f)
        {
            lastAttack = attackTimer;
            animator.SetTrigger("Attack");
        }
    }
}
