using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColdDownManager : MonoBehaviour
{
    private Dictionary<GameObject, float> coldDown = new Dictionary<GameObject, float>();

    void Update()
    {
        List<GameObject> keys = new List<GameObject>(coldDown.Keys);
        foreach (GameObject bulletPrefab in keys)
        {
            if (coldDown[bulletPrefab] > 0)
            {
                coldDown[bulletPrefab] -= Time.deltaTime;
            }
        }
    }

    public void RegisterBullet(GameObject bulletPrefab)
    {
        Bullet bulletComponent = bulletPrefab.GetComponent<Bullet>();
        if (bulletComponent != null)
        {
            coldDown[bulletPrefab] = bulletComponent.coolingTime;
            Debug.Log("Bullet cooling time: " + bulletComponent.coolingTime);
        }
        else
        {
            Debug.LogError("Bullet script is not found.");
        }
    }

    public bool IsBulletCooling(GameObject bulletPrefab)
    {
        if (coldDown.ContainsKey(bulletPrefab))
        {
            return coldDown[bulletPrefab] > 0;
        }
        return false;
    }
}