using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    public float lifetime;
    void Start()
    {
        StartCoroutine(DestroySelf(lifetime));
    }

    private IEnumerator DestroySelf(float lifetime)
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }

   
}
