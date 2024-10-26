using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof (Attack))]
public abstract class Bullet : MonoBehaviour
{
    public ParticleSystem explosionEffect;
    protected float speed;
    protected int damage;
    public float coolingTime = 2f;
    private Rigidbody rb;
    private Attack attack;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody>();
        attack = GetComponent<Attack>();
    }

    public void ShootTowards(Transform target)
    {
        if (rb == null)
        {
            Debug.LogError("Rigidbody is not initialized.");
            return;
        }

        Vector3 direction = (target.position - transform.position).normalized;
        rb.velocity = direction * speed; 
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Zombie"))
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
            Enemy enemy = other.GetComponent<Enemy>();
            attack.PlayerAttack(damage,enemy);
            Destroy(gameObject);
        }
    }
}
