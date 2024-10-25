using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;

    private bool isShooting;
    private const float standSpeed = 0.5f;
    private const float moveSpeed = 2.0f;
    private int playerAttackPower = 10;

    private Attack attack;
    private Animator animator;

    void Start()
    {
        // Initialize the player
        animator = GetComponent<Animator>();
        attack = GetComponent<Attack>();
        transform.position = new Vector3(0, 0, 25); // Set the player's initial position
        StartCoroutine(MoveToPosition(new Vector3(0, 0, 40), 3.0f)); // Move the player to the initial position
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isShooting = true;
            animator.SetBool("isShooting", isShooting);
            // GameObject targetEnemy = GetTargetEnemy();
            // if (targetEnemy != null)
            // {
            //     Enemy zombieComponent = targetEnemy.GetComponent<Enemy>();
            //     attack.PlayerAttack (playerAttackPower, zombieComponent);
            // }
            Shoot();
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
        SwitchSpeed (moveSpeed);
        while (elapsed < duration)
        {
            transform.position =
                Vector3.Lerp(startPosition, target, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = target;
        SwitchSpeed (standSpeed);
    }

    private void SwitchSpeed(float speed)
    {
        animator.SetFloat("Speed", speed);
    }

    private void Shoot(){
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bulletComponent = bullet.GetComponent<Bullet>();
        if (bulletComponent != null){
            GameObject targetEnemy = GetTargetEnemy();
            if (targetEnemy != null){
                bulletComponent.ShootTowards(targetEnemy.transform.position);
            }
        }
    }

    GameObject GetTargetEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Zombie");
        GameObject targetEnemy = null;
        float minDistance = Mathf.Infinity;
        foreach (GameObject enemy in enemies)
        {
            float distance =
                Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < minDistance)
            {
                targetEnemy = enemy;
                minDistance = distance;
            }
        }
        return targetEnemy;
    }
}
