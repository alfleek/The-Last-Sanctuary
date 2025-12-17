using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Base Weapon Settings")]
    public float durability;
    public float baseDamage;
    public float attackDetectionRange;
    public float attackDetectionTime = 0f;
    public float attackDetectionTimer;

    // Update is called once per frame
    void Update()
    {

    }

    public virtual void SingleAttack()
    {

    }

    public virtual void AlternateAttack()
    {

    }

    public virtual void HoldAttackStart()
    {

    }

    public virtual void HoldAttackStop()
    {

    }

    public virtual void AltHoldAttackStart()
    {

    }

    public virtual void AltHoldAttackStop()
    {

    }

    public virtual void Reload()
    {
        
    }
    
}
