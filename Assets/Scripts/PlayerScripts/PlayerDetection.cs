using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    public float detectionRadius;
    public InputManager input;
    public PlayerMotor player;
    private Weapon weapon;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMotor>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float GetDetectionRadius()
    {
        detectionRadius = player.speed;
        weapon = input.equippedWeapon;
        if (weapon && weapon.attackDetectionTimer > 0)
            detectionRadius += weapon.attackDetectionRange;
        return detectionRadius;
    }
}
