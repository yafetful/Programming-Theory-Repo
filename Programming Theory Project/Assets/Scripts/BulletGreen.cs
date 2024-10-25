using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletGreen : Bullet
{
    protected override void Awake()
    {
        base.Awake();
        speed = 80f;
        damage = 20;
        coolingTime = 0.5f;
    }
}
