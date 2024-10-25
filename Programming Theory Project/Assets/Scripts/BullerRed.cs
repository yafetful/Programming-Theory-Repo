using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullerRed : Bullet
{
    protected override void Awake()
    {
        base.Awake();
        speed = 50f;
        damage = 100;
        coolingTime = 1f;
    }
}
