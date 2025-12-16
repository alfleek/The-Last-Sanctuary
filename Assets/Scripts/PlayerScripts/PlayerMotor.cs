using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool isGrounded;

    private float speed;
    public float walkSpeed = 5f;
    public float sprintSpeed = 8f;
    public float crouchSpeed = 3f;

    public float gravity = -9.8f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.2f;
    public LayerMask groundMask;

    private bool crouching = false;
    private bool lerpCrouching = false;
    private float crouchTimer = 0;
    private bool sprinting;

    [Header("Vitals")]
    public float maxHealth = 100f;
    public float maxStamina = 100f;
    public float maxHunger = 100f;
    public float maxThirst = 100f;

    private float health;
    private float stamina;
    private float hunger;
    private float thirst;

    [Header("Depletion Rates")]
    public float staminaDrainSprint = 0.01f;  // per second
    public float staminaRegen = 0.5f;          // per second
    public float hungerDrainMove = 1f;     // per second
    public float thirstDrainMove = 0.6f;     // per second

    [Header("Health Penalty")]
    public float healthDrainInterval = 420f; // 7 minutes
    public float healthDrainAmount = 5f;

    private float hungerHealthTimer = 0f;
    private float thirstHealthTimer = 0f;

    private bool isMoving = false;
    private bool isDead = false;


    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        speed = walkSpeed;

        health = maxHealth;
        stamina = maxStamina;
        hunger = maxHunger;
        thirst = maxThirst;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead) return;

        isGrounded = controller.isGrounded;

        HandleCrouch();
        HandleSpeed();
        HandleVitals();

        if (health <= 0)
            Die();


    }

    public void HandleSpeed()
    {
        if (sprinting)
            speed = sprintSpeed;
        else if (crouching)
            speed = crouchSpeed;
        else
            speed = walkSpeed;
    }

    public void HandleCrouch()
    {
        if (lerpCrouching)
        {
            crouchTimer += Time.deltaTime;
            float p = crouchTimer / 1;
            p *= p;
            if (crouching)
                controller.height = Mathf.Lerp(controller.height, 1, p);
            else
                controller.height = Mathf.Lerp(controller.height, 2, p);

            if (p > 1)
            {
                lerpCrouching = false;
                crouchTimer = 0f;
            }

        }
    }

    public void HandleVitals()
    {
        // STAMINA
        if (sprinting || isMoving)
        {
            stamina -= staminaDrainSprint * Time.deltaTime;
            if (stamina <= 0)
            {
                stamina = 0;
                sprinting = false;
            }
        }
        else
        {
            stamina += staminaRegen * Time.deltaTime;
        }
        stamina = Mathf.Clamp(stamina, 0, maxStamina);

        // HUNGER & THIRST
        if (isMoving)
        {
            hunger -= hungerDrainMove * Time.deltaTime;
            thirst -= thirstDrainMove * Time.deltaTime;
        }

        hunger = Mathf.Clamp(hunger, 0, maxHunger);
        thirst = Mathf.Clamp(thirst, 0, maxThirst);

        // HEALTH DRAIN FROM LOW HUNGER
        if (hunger < 25f)
        {
            hungerHealthTimer += Time.deltaTime;
            if (hungerHealthTimer >= healthDrainInterval)
            {
                health -= healthDrainAmount;
                hungerHealthTimer = 0f;
            }
        }
        else
        {
            hungerHealthTimer = 0f;
        }

        // HEALTH DRAIN FROM LOW THIRST
        if (thirst < 25f)
        {
            thirstHealthTimer += Time.deltaTime;
            if (thirstHealthTimer >= healthDrainInterval)
            {
                health -= healthDrainAmount;
                thirstHealthTimer = 0f;
            }
        }
        else
        {
            thirstHealthTimer = 0f;
        }

        health = Mathf.Clamp(health, 0, maxHealth);
    }

    public void Die()
    {
        isDead = true;
        speed = 0f;
        sprinting = false;

        Debug.Log("Player has died.");


    }

    public float getHealth() => health;
    public float getStamina() => stamina;
    public float getHunger() => hunger;
    public float getThirst() => thirst;

    public float getMaxHealth() => maxHealth;
    public float getMaxStamina() => maxStamina;
    public float getMaxHunger() => maxHunger;
    public float getMaxThirst() => maxThirst;

    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirection = new Vector3(input.x, 0, input.y);
        isMoving = moveDirection.magnitude > 0.1f;

        controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);

        playerVelocity.y += gravity * Time.deltaTime;

        if (isGrounded && playerVelocity.y < 0)
            playerVelocity.y = -2f;

        controller.Move(playerVelocity * Time.deltaTime);
    }

    public void Jump()
    {
        if (isGrounded)
        {
            isMoving = true;
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }
    }

    public void Crouch()
    {
        if (sprinting)
            sprinting = false;
        crouching = !crouching;
        crouchTimer = 0f;
        lerpCrouching = true;
    }

    public void Sprint()
    {
        
        if (crouching)
        {
            isMoving = true;
            Crouch();    
        }
        
        sprinting = !sprinting;
        
    }
}
