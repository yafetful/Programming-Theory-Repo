using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public int playerAttackPower = 10;
    public int enemyAttackPower = 8;
    public int playerHealth = 100;
    public int enemyHealth = 100;

    // Calculate Damage
    int CalculateDamage(int attackPower, int defense)
    {
        return Mathf.Max(0, attackPower - defense);
    }

    // Player AttackEnemy
    public void PlayerAttack()
    {
        int damage = CalculateDamage(playerAttackPower, 0); // 假设敌人没有防御
        enemyHealth -= damage;
        Debug.Log("敌人受到 " + damage + " 点伤害，剩余生命值：" + enemyHealth);
    }

    // Enemy Attack Player
    public void EnemyAttack()
    {
        int damage = CalculateDamage(enemyAttackPower, 0); // 假设玩家没有防御
        playerHealth -= damage;
        Debug.Log("玩家受到 " + damage + " 点伤害，剩余生命值：" + playerHealth);
    }

        // Update is called once per frame
    void Update()
    {
        
    }
}
