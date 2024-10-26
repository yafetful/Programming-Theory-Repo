using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zmobie : Enemy
{
    protected override void Awake(){
        base.Awake();
        speed = 1.5f;
        health = 200;
        attackPower = 5;
        scoreValue = 40;
    }
}
