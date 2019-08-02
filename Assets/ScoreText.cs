using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour {

    int score = 0;
    Text scoreText;

	// Use this for initialization
	void Start () {
        scoreText = GetComponent<Text>();
        UpdateScoreText();
    }
	
	// Update is called once per frame
	public void ScoreHit(int points)
    {
        score += points;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        //Testing version control
        scoreText.text = score.ToString("D8");
    }
}
