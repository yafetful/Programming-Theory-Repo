using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (Attack))]
public abstract class Enemy : MonoBehaviour
{
    private Animator animator;

    protected Attack attack;

    [SerializeField]
    protected float speed;
    [SerializeField]
    protected int health;
    [SerializeField]
    protected int attackPower;

    private bool isAttacking = false;

    // Start is called before the first frame update
    protected virtual void Awake()
    {
        attack = GetComponent<Attack>();
        animator = GetComponent<Animator>();
        animator.SetFloat("MoveSpeed", speed);
    }

    public int Health{
        get{
            return health;
        }
        set{
            health = value;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 targetPosition =
            new Vector3(transform.position.x, transform.position.y, 45f);
        MoveToPosition (targetPosition);
    }

    protected virtual void MoveToPosition(Vector3 targetPosition)
    {
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
        animator.SetTrigger (animationName);
    }

    private IEnumerator AttackRoutine()
    {
        isAttacking = true;
        while(true){
            attack.EnemyAttack(attackPower);
            yield return new WaitForSeconds(1f);
        }
    }

    public void TakeDamage(int damage){
        health -= damage;
        if(health <= 0){
            Destroy(gameObject);
        }
    }
}
