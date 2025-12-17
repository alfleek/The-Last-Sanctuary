using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAnimationRelay : MonoBehaviour
{
    private ZombieHand[] hands;

    private void Awake()
    {
        hands = GetComponentsInChildren<ZombieHand>();
    }

    public void BeginAttackWindow()
    {
        foreach (var hand in hands)
            hand.BeginAttackWindow();
    }

    public void EndAttackWindow()
    {
        foreach (var hand in hands)
            hand.EndAttackWindow();
    }
}
