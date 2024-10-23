using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public static Obstacle Instance { get; private set; }
    private int obstacleHealth = 100;

    public int ObstacleHealth
    {
        get { return obstacleHealth; }
        set { obstacleHealth = value; }
    }

    private void Awake(){
        if(Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else{
            Destroy(gameObject);
        }
    }
}
