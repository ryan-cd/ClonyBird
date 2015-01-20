using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour {

	static int score = 0;
	static int highScore = 0;

	static Score instance;

	static public void addPoint() {
		if(instance.bird.dead)
			return;
		score++;
		if(score > highScore)
			highScore = score;
	}

	BirdMovement bird;
	// Use this for initialization
	void Start () {
		instance = this;
		GameObject player_go = GameObject.FindGameObjectWithTag("Player");
		if(player_go == null) {
			Debug.Log("Could not find an object with tag 'Player'.");
		}
		bird = player_go.GetComponent<BirdMovement>();
		score = 0;
		//The 0 is the default value
		highScore = PlayerPrefs.GetInt ("highScore", 0);
	}

	void OnDestroy() {
		instance = null;
		PlayerPrefs.SetInt ("highScore", highScore);
	}

	
	// Update is called once per frame
	void Update () {
		guiText.text = "Score: " + score + "\nHigh Score: " + highScore;
	}
}
