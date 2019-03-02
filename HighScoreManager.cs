using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreManager : MonoBehaviour {

    Text highScore;

	private void Start () {
        highScore = GetComponent<Text>();
        UpdateScore();
	}
	
    private void UpdateScore()
    {
        highScore.text = "High Score: " + GameControl.highScore.ToString();
        Debug.Log(highScore.text);

    }

}
