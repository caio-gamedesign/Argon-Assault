using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour {

    [SerializeField] int scorePerHit = 0;

    int score = 0;
    Text scoreText;

	// Use this for initialization
	void Start () {
        scoreText = GetComponent<Text>();
        UpdateScoreText();
    }
	
	// Update is called once per frame
	public void ScoreHit()
    {
        score += scorePerHit;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        scoreText.text = score.ToString("D8");
    }
}
