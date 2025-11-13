using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Weapon
{
    [Header("Gun Settings")]
    public float fireRate = 10f;
    public bool automatic = true;
    public float reloadTime;
    public int magazineSize, remainingBullets;
    public bool isReloading;
    public bool allowFireWhileAiming = true;
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletVelocity = 30;
    public float bulletPrefabLifeTime = 3f;

    public float spreadAngleDegrees;
    public float spreadAngleDegreesADS;

    private Coroutine firingLoop;
    private bool isAiming;
    private float lastShot;

    public InputManager playerInput;
    private Animator animator;
    public GameObject muzzleFlash;

    public void Awake()
    {
        animator = GetComponent<Animator>();
        remainingBullets = magazineSize;
    }

    public void Start()
    {
        if (playerInput) playerInput.EquipWeapon(gameObject.GetComponent<Gun>());
    }
    public override void HoldAttackStart()
    {
        if (isReloading) return;
        if (firingLoop == null)
            firingLoop = StartCoroutine(FiringCoroutine());
    }

    public override void HoldAttackStop()
    {
        if (isReloading) return;
        if (firingLoop != null)
        {
            StopCoroutine(firingLoop);
            firingLoop = null;
        }
    }

    public override void AltHoldAttackStart()
    {
        if (isReloading) return;
        isAiming = true;
    }

    public override void AltHoldAttackStop()
    {
        isAiming = false;
    }

    public override void SingleAttack()
    {
        if (!bulletPrefab || !bulletSpawn) return;

        remainingBullets -= 1;
        muzzleFlash.GetComponent<ParticleSystem>().Play();
        animator.SetTrigger("RECOIL");

        Vector3 shootingDirection = CalculateDirectionAndSpread(bulletSpawn.forward, isAiming ? spreadAngleDegreesADS : spreadAngleDegrees).normalized;

        var bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        bullet.GetComponent<Bullet>().bulletDamage = baseDamage;
        var rb = bullet.GetComponent<Rigidbody>();
        bullet.transform.forward = shootingDirection;
        if (rb) rb.velocity = shootingDirection * bulletVelocity;
        Destroy(bullet, bulletPrefabLifeTime);

        if (remainingBullets <= 0) Reload();
    }

    public override void Reload()
    {
        if (remainingBullets >= magazineSize || isReloading) return;
        isReloading = true;
        animator.SetTrigger("RELOAD");
        Invoke("ReloadCompleted", reloadTime);
    }

    private void ReloadCompleted()
    {
        remainingBullets = magazineSize;
        isReloading = false;
    }

    public Vector3 CalculateDirectionAndSpread(Vector3 forward, float spreadAngleDegrees)
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(100);
        }

        Vector3 direction = targetPoint - bulletSpawn.position;

        float spreadRad = spreadAngleDegrees * Mathf.Deg2Rad;
        float cosAngle = Mathf.Cos(spreadRad);

        // Pick a random direction within the cone
        float z = Random.Range(cosAngle, 1f);
        float theta = Random.Range(0f, 2f * Mathf.PI);
        float r = Mathf.Sqrt(1 - z * z);

        Vector3 localDir = new Vector3(r * Mathf.Cos(theta), r * Mathf.Sin(theta), z);
        Quaternion rot = Quaternion.FromToRotation(Vector3.forward, direction);
        return rot * localDir;
    }

    private IEnumerator FiringCoroutine()
    {
        // Single immediate shot (covers quick taps)
        SingleAttack();

        if (!automatic)
        {
            // Semi-auto: just one bullet per press
            firingLoop = null;
            yield break;
        }

        float interval = 1f / Mathf.Max(0.01f, fireRate);

        // Keep firing while button held (InputManager will stop us on release)
        while (true)
        {
            if (isReloading) yield break;
            yield return new WaitForSeconds(interval);
            if (allowFireWhileAiming || !isAiming)
                SingleAttack();
        }
    }
}