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
    public ZombieHand zombieHand;
    public int zombieDamage;
    private float speed;
    [SerializeField] private float attackTimer;
    [SerializeField] private float health = 100f;
    private float lastAttack;
    public bool dead = false;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        // if (playerAggro)
        // {
        //     agent.destination = player.position;
        // }
        lastAttack = attackTimer;
        zombieHand.damage = zombieDamage;
    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;

        if (health <= 0)
        {
            animator.SetBool("FallForward", Random.value < 0.5f);
            animator.SetTrigger("KnockDown");
            
            dead = true;
            agent.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (dead) return;
        lastAttack -= Time.deltaTime;

        // speed = agent.velocity.magnitude;
        // if (playerAggro)
        // {
        //     agent.destination = player.position;
            
        // }

        // TEMP ANIMATION LOGIC: To be replaced
        // if (speed <= 0.01f)
        // {
        //     animator.SetBool("Walking", false);
        //     animator.SetBool("Running", false);
        //     animator.SetBool("Idle", true);
        // }
        // else if (speed <= 1.5f)
        // {
        //     animator.SetBool("Idle", false);
        //     animator.SetBool("Running", false);
        //     animator.SetBool("Walking", true);
        // }
        // else
        // {
        //     animator.SetBool("Walking", false);
        //     animator.SetBool("Idle", false);
        //     animator.SetBool("Running", true);
        // }

        // if (agent.remainingDistance < 2.5f && lastAttack < 0f)
        // {
        //     lastAttack = attackTimer;
        //     animator.SetTrigger("Attack");
        // }
    }
}
