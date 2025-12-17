using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : Weapon
{
    [Header("Melee Settings")]
    public float attackDistance;
    public float attackDelay;
    public float attackSpeed;
    public float staminaPerSwing;

    bool attacking = false;
    bool attackInput = false;
    bool readyToAttack = true;
    public InputManager playerInput;
    public GameObject bloodSprayPrefab;
    public Camera cam;
    public PlayerMotor player;
    public LayerMask attackLayer;
    private Animator animator;

    public void Awake()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMotor>();
    }

    void Start()
    {
        if (playerInput && gameObject.activeSelf) playerInput.EquipWeapon(gameObject.GetComponent<Melee>());
        attackDetectionTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (attackInput) Attack();
        attackDetectionTimer -= Time.deltaTime;
    }

    public override void HoldAttackStart()
    {
        attackInput = true;
    }

    public override void HoldAttackStop()
    {
        attackInput = false;
    }

    public void Attack()
    {
        if (!readyToAttack || attacking) return;

        player.StaminaDrain(staminaPerSwing);
        animator.SetTrigger("Attack");

        attackDetectionTimer = attackDetectionTime;
        readyToAttack = false;
        attacking = true;

        Invoke(nameof(ResetAttack), attackSpeed);
        Invoke(nameof(AttackRaycast), attackDelay);



    }

    void ResetAttack()
    {
        attacking = false;
        readyToAttack = true;
    }

    void AttackRaycast()
    {
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit, attackDistance, attackLayer, QueryTriggerInteraction.Collide))
        {
            Collider other = hit.collider;

            if (other.CompareTag("Zombie") || other.CompareTag("ZombieHand"))
            {
                float mult = other.GetComponent<ZombieHitbox>().damageMult;
                var zombie = other.transform.root.GetComponent<ZombieNavigation>();
                if (zombie != null && !zombie.dead)
                {
                    zombie.TakeDamage(baseDamage * mult);
                }

                CreateBloodSpray(other.transform, hit.point, hit.normal);
            }
        }
    }

    private void CreateBloodSpray(Transform parent, Vector3 hitPoint, Vector3 hitNormal)
    {
        if (bloodSprayPrefab == null) return;

        GameObject blood = Instantiate(
            bloodSprayPrefab,
            hitPoint,
            Quaternion.LookRotation(hitNormal)
        );

        blood.transform.SetParent(parent);
    }

}
