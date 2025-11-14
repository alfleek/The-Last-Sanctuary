using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletDamage;
    public GameObject bloodSprayPrefab;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Target"))
        {
            Debug.Log("hit " + collision.gameObject.name + " !");
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Zombie"))
        {

            if (!collision.transform.root.gameObject.GetComponent<ZombieNavigation>().dead)
            {
                collision.transform.root.gameObject.GetComponent<ZombieNavigation>().TakeDamage(bulletDamage);
            }
            
            CreateBloodSpray(collision);
            Destroy(gameObject);
        }

        Destroy(gameObject);

    }

    private void CreateBloodSpray(Collision collision)
    {
        ContactPoint contact = collision.contacts[0];

        GameObject bloodSpray = Instantiate(bloodSprayPrefab, contact.point, Quaternion.LookRotation(contact.normal));

        bloodSpray.transform.SetParent(collision.gameObject.transform);
    }
}
