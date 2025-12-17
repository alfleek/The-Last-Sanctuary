using System;
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
    private ZombieHand[] hands;
    public int zombieDamage;
    private float speed;
    [SerializeField] private float attackTimer;
    [SerializeField] private float health = 100f;
    private float lastAttack;
    public bool dead = false;
    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (player == null)
        {
            Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        lastAttack = attackTimer;
        hands = GetComponentsInChildren<ZombieHand>();
        foreach (var hand in hands)
            hand.damage = zombieDamage;
    }

    public event Action<ZombieNavigation> OnDeath;

    public void TakeDamage(float damageAmount)
    {
        if(dead) return;

        health -= damageAmount;

        if (health <= 0)
        {
            Die();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (dead) return;
        lastAttack -= Time.deltaTime;

    }

    private void Die()
    {
        if (dead) return;

        animator.SetBool("FallForward", UnityEngine.Random.value < 0.5f);
        animator.SetTrigger("KnockDown");
        
        dead = true;
        agent.enabled = false;          

        OnDeath?.Invoke(this);
        Destroy(gameObject, 60f);
    }
}
