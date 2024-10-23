using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool isShooting;
    private float MoveSpeed;
    private int playerAttackPower = 10;

    private Attack attack;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        attack = GetComponent<Attack>();
        transform.position = new Vector3(0, 0, 25);
        StartCoroutine(MoveToPosition(new Vector3(0, 0, 40), 3.0f));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isShooting = true;
            animator.SetBool("isShooting", isShooting);
            GameObject targetEnemy = GetTargetEnemy();
            if(targetEnemy != null){
                Enemy zombieComponent = targetEnemy.GetComponent<Enemy>();
                attack.PlayerAttack(playerAttackPower, zombieComponent);
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isShooting = false;
            animator.SetBool("isShooting", isShooting);
        }
    }

    private IEnumerator MoveToPosition(Vector3 target, float duration)
    {
        Vector3 startPosition = transform.position;
        float elapsed = 0;
        MoveSpeed = 2.0f;
        animator.SetFloat("Speed", MoveSpeed);
        while (elapsed < duration)
        {
            transform.position = Vector3.Lerp(startPosition, target, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = target;
        MoveSpeed = 0.5f;
        animator.SetFloat("Speed", MoveSpeed);
    }

    GameObject GetTargetEnemy(){
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Zombie");
        GameObject targetEnemy = null;
        float minDistance = Mathf.Infinity;
        foreach(GameObject enemy in enemies){
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if(distance < minDistance){
                targetEnemy = enemy;
                minDistance = distance;
            }
        }
        return targetEnemy;
    }
}
