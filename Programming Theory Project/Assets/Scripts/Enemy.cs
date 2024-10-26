using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Attack))]
public abstract class Enemy : MonoBehaviour
{
    private Animator animator;

    protected Attack attack;

    protected int scoreValue;
    [SerializeField]
    protected float speed;
    [SerializeField]
    protected int health;
    [SerializeField]
    protected int attackPower;

    private bool isAttacking = false;
    public bool isDead = false;
    public Transform centerPoint;

    // INHERITANCE
    protected virtual void Awake()
    {
        attack = GetComponent<Attack>();
        animator = GetComponent<Animator>();
        animator.SetFloat("MoveSpeed", speed);

    }
    // ENCAPSULATION
    public int Health
    {
        get
        {
            return health;
        }
        set
        {
            health = value;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 targetPosition = new(transform.position.x, transform.position.y, 45f);
        MoveToPosition(targetPosition);
    }

    protected virtual void MoveToPosition(Vector3 targetPosition)
    {
        if (isDead)
        {
            return;
        }
        transform.position =
            Vector3
                .MoveTowards(transform.position,
                targetPosition,
                speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f && !isAttacking)
        {
            SwitchAnimation("Attack");
            StartCoroutine(AttackRoutine());
        }
    }

    private void SwitchAnimation(string animationName)
    {
        animator.SetTrigger(animationName);
    }

    private IEnumerator AttackRoutine()
    {
        isAttacking = true;
        while (isAttacking)
        {
            attack.EnemyAttack(attackPower);
            yield return new WaitForSeconds(1f);
            isAttacking = false;
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            ScoreManager.instance.AddScore(scoreValue);
            SwitchAnimation("Dead");
            StartCoroutine(DeadRoutine());
            Debug.Log("Enemy is dead." + scoreValue);
        }
    }

    IEnumerator DeadRoutine()
    {
        isDead = true;
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
