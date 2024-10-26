using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowZmobie : Enemy
{
    // INHERITANCE
    protected override void Awake(){
        base.Awake();
        speed = 0.8f;
        health = 100;
        attackPower = 10;
        scoreValue = 10;
    }
}
