using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private GameObject obstacle;

    private int obstacleHealth;

    private void Start()
    {
    }


    // Calculate Damage
    int CalculateDamage(int attackPower, int defense)
    {
        return Mathf.Max(0, attackPower - defense);
    }

    // Player AttackEnemy
    public void PlayerAttack(int playerAttackPower, Enemy targetEnemy)
    {
        if(targetEnemy != null){    
            int damage = CalculateDamage(playerAttackPower, 0);
            targetEnemy.TakeDamage(damage);
            Debug.Log("Enemy took " + damage + " damage, remaining health: " + targetEnemy.Health);
        }
    }

    // Enemy Attack Player
    public void EnemyAttack(int enemyAttackPower)
    {
        int damage = CalculateDamage(enemyAttackPower, 0);
        Obstacle.Instance.ObstacleHealth -= damage;
        Debug.Log("Player took " + damage + " damage, remaining health: " + Obstacle.Instance.ObstacleHealth);
    }
}
