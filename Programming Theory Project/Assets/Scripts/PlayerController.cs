using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject bulletRedPrefab;
    public ParticleSystem fireEffect;
    public Transform firePoint;
    private const float standSpeed = 0.5f;
    private const float moveSpeed = 2.0f;
    private Animator animator;
    private ColdDownManager coldDownManager;

    public bool isGameAlive;

    void Start()
    {
        // Initialize the player
        animator = GetComponent<Animator>();
        coldDownManager = GetComponent<ColdDownManager>();
        transform.position = new Vector3(0, 0, 25); // Set the player's initial position
        StartCoroutine(MoveToPosition(new Vector3(0, 0, 40), 3.0f)); // Move the player to the initial position
    }

    void Update()
    {
        if (isGameAlive)
        {
            HandleInput();
        }
    }

    private IEnumerator MoveToPosition(Vector3 target, float duration)
    {
        Vector3 startPosition = transform.position;
        float elapsed = 0;
        SwitchSpeed(moveSpeed);
        while (elapsed < duration)
        {
            transform.position =
                Vector3.Lerp(startPosition, target, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = target;
        SwitchSpeed(standSpeed);
        isGameAlive = true;
    }

    private void SwitchSpeed(float speed)
    {
        animator.SetFloat("Speed", speed);
    }
    IEnumerator GunMuzzle()
    {
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("isShooting", false);
    }

    private void TryShoot(GameObject bulletType)
    {
        
        if (!coldDownManager.IsBulletCooling(bulletType))
        {
            animator.SetBool("isShooting", true);
            Shoot(bulletType);
            coldDownManager.RegisterBullet(bulletType);
        }else
        {
            Debug.Log("Bullet is cooling down.");
        }
    }

    private void Shoot(GameObject bulletType)
    {
        GameObject targetEnemy = GetTargetEnemy();
        if (targetEnemy != null)
        {
            bool isDead = targetEnemy.GetComponent<Enemy>().isDead;
            if (!isDead)
            {
                Instantiate(fireEffect, firePoint.position, firePoint.rotation);
                GameObject bullet = Instantiate(bulletType, firePoint.position, firePoint.rotation);
                Bullet bulletComponent = bullet.GetComponent<Bullet>();
                Transform targetTransform = targetEnemy.GetComponent<Enemy>().centerPoint;
                bulletComponent.ShootTowards(targetTransform);
                StartCoroutine(GunMuzzle());
            }
            else
            {
                return;
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
            bool isDead = enemy.GetComponent<Enemy>().isDead;
            float distance =
                Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < minDistance && !isDead)
            {
                targetEnemy = enemy;
                minDistance = distance;
            }
        }
        return targetEnemy;
    }
    private void HandleInput()
    {
        switch (Input.inputString)
        {
            case "1":
                TryShoot(bulletPrefab);
                break;
            case "2":
                TryShoot(bulletRedPrefab);
                break;
        }
    }
}
