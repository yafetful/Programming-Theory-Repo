using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ScoreDisplay : MonoBehaviour
{
    public TMPro.TextMeshProUGUI scoreText;
    // Update is called once per frame
    void Update()
    {
        if (ScoreManager.instance != null)
        {
            scoreText.text = "Score: " + ScoreManager.instance.GetScore();
        }
    }
}
