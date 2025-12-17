using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHand : MonoBehaviour
{

    public float damage;

    private bool canDamage;
    private bool hasHitThisAttack;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!canDamage || hasHitThisAttack)
            return;

        if (!other.CompareTag("Player"))
            return;

        var motor = other.GetComponent<PlayerMotor>();
        if (motor != null)
        {
            motor.TakeDamage(damage);
            hasHitThisAttack = true;
        }
    }

    public void BeginAttackWindow()
    {
        hasHitThisAttack = false;
        canDamage = true;
    }

    public void EndAttackWindow()
    {
        canDamage = false;
    }
}
