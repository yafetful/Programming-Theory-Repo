using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 1000f;
    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody component not found on bullet.");
        }

    }

    public void ShootTowards(Vector3 target)
    {
        if (rb == null)
        {
            Debug.LogError("Rigidbody is not initialized.");
            return;
        }

        Vector3 direction = (target - transform.position).normalized;
        rb.velocity = direction * speed; 
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Zombie"))
        {
            other.GetComponent<Enemy>().TakeDamage(10);
            Destroy(gameObject);
        }
    }
}
