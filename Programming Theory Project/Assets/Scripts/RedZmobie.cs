using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedZmobie : Enemy
{
    protected override void Awake(){
        base.Awake();
        speed = 1f;
        health = 150;
        attackPower = 20;
    }
}
