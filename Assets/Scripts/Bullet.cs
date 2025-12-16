using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletDamage = 25f;
    public GameObject bloodSprayPrefab;
    public LayerMask hitMask = ~0;   // what the bullet can hit

    private Rigidbody rb;
    private Vector3 previousPosition;
    private bool hasHit;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        previousPosition = transform.position;
        hasHit = false;
    }

    private void FixedUpdate()
    {
        if (hasHit) return;

        Vector3 currentPosition = rb.position;
        Vector3 displacement = currentPosition - previousPosition;
        float distance = displacement.magnitude;

        if (distance > 0f)
        {
            RaycastHit hit;
            if (Physics.Raycast(
                    previousPosition,
                    displacement.normalized,
                    out hit,
                    distance,
                    hitMask,
                    QueryTriggerInteraction.Collide))
            {
                HandleHit(hit);
                return;
            }
        }

        previousPosition = currentPosition;
    }

    private void HandleHit(RaycastHit hit)
    {
        hasHit = true;
        Collider other = hit.collider;

        if (other.CompareTag("Zombie") || other.CompareTag("ZombieHand"))
        {
            float mult = other.GetComponent<ZombieHitbox>().damageMult;
            var zombie = other.transform.root.GetComponent<ZombieNavigation>();
            if (zombie != null && !zombie.dead)
            {
                zombie.TakeDamage(bulletDamage * mult);
            }

            CreateBloodSpray(other.transform, hit.point, hit.normal);
        }

        Destroy(gameObject);
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
