using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColdDownManager : MonoBehaviour
{
    private Dictionary<GameObject, float> bulletCooldowns = new Dictionary<GameObject, float>();

    // 在 Update 方法中更新冷却时间
    void Update()
    {
        List<GameObject> keys = new List<GameObject>(bulletCooldowns.Keys);
        foreach (GameObject bullet in keys)
        {
            if (bulletCooldowns[bullet] > 0)
            {
                bulletCooldowns[bullet] -= Time.deltaTime;
            }
        }
    }

    public void RegisterBullet(GameObject bullet)
    {
        if (!bulletCooldowns.ContainsKey(bullet))
        {
            Bullet bulletComponent = bullet.GetComponent<Bullet>();
            if (bulletComponent != null)
            {
                bulletCooldowns[bullet] = 0f;
                Debug.Log("Registered bullet with cooling time: " + bulletComponent.GetCoolingTime);
            }
            else
            {
                Debug.LogError("The provided GameObject does not have a Bullet component.");
            }
        }
    }

    public bool IsCooldownComplete(GameObject bullet)
    {
        if (bulletCooldowns.ContainsKey(bullet))
        {
            return bulletCooldowns[bullet] <= 0;
        }
        return false;
    }

    public void StartCooldown(GameObject bullet)
    {
        if (bulletCooldowns.ContainsKey(bullet))
        {
            Bullet bulletComponent = bullet.GetComponent<Bullet>();
            if (bulletComponent != null)
            {
                bulletCooldowns[bullet] = bulletComponent.GetCoolingTime;
                Debug.Log("Started cooldown for bullet with cooling time: " + bulletComponent.GetCoolingTime);
            }
        }
    }
}