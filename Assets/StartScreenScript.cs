using UnityEngine;
using System.Collections;

public class StartScreenScript : MonoBehaviour {

	public static bool sawOnce = false;
	Transform player;

	// Use this for initialization
	void Start () {
		GameObject player_go = GameObject.FindGameObjectWithTag("Player");
		
		if(player_go == null) {
			Debug.LogError("Couldn't find an object with tag 'Player'!");
			return;
		}
		
		player = player_go.transform;
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.timeScale == 0 && Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) {
			Time.timeScale = 1;
			GetComponent<SpriteRenderer>().enabled = false;
			sawOnce = true;
		}

		if(!sawOnce) {
			GetComponent<SpriteRenderer>().enabled = true; // Make it visible
			Time.timeScale = 0; // Pause game
		}

		if(player != null) {
			Vector3 pos = transform.position;
			pos.x = player.position.x;
			//Debug.Log(player.position.x);
			transform.position = pos;
		}
	}
}
