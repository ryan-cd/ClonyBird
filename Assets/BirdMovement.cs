using UnityEngine;
using System.Collections;

public class BirdMovement : MonoBehaviour {
	Vector3 velocity = Vector3.zero;
	/*public Vector3 flapVelocity;
	public float maxSpeed = 5f;
	public float forwardSpeed = 1f;*/
	public float flapSpeed = 100f;
	public float forwardSpeed = 1f;

	bool didFlap = false;
	
	Animator animator;

	public bool dead = false;
	float deathCooldown; //so you can't accidentally respawn yourself before seeing your score

	public bool godMode = false;

	// Use this for initialization
	void Start () {
		animator = transform.GetComponentInChildren<Animator>();
		//animator = GetComponent<Animator>();

		if(animator == null) {
			Debug.LogError("Didn't find animator!");
		}
	}

	// Do Graphic & Input updates here
	void Update() {
		if(dead) {
			deathCooldown -= Time.deltaTime;

			if(deathCooldown <= 0) {
				StartScreenScript.sawOnce = false;
				if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) {
					Application.LoadLevel( Application.loadedLevel);
					StartScreenScript.sawOnce = true;
				}
			}
		} 
		else {
			if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) {
				didFlap = true;
			}
		}
	}

	//Do physics engine updates here
	// Update is called once per frame
	void FixedUpdate () {
		/*velocity.x = forwardSpeed;

		if(didFlap == true) {
			didFlap = false;
			//First cancel downward movement, then flap. This way you won't flap and still go down
			if(velocity.y < 0)
				velocity.y = 0;
			velocity += flapVelocity;
		}

		velocity = Vector3.ClampMagnitude(velocity, maxSpeed);

		rigidbody2D.AddForce(velocity);
		//transform.position += velocity * Time.deltaTime;

		float angle = 0;
		if(velocity.y < 0) {
			angle = Mathf.Lerp (0, -90, -velocity.y / maxSpeed);
		}

		transform.rotation = Quaternion.Euler (0, 0, angle);*/
		if(dead)
			return;

		rigidbody2D.AddForce(Vector2.right * forwardSpeed);

		if(didFlap) {
			rigidbody2D.AddForce(Vector2.up * flapSpeed);
			
			animator.SetTrigger("DoFlap");
			didFlap = false;
		}

		if(rigidbody2D.velocity.y > 0) { 
			transform.rotation = Quaternion.Euler (0, 0, 0);
		} else {
			float angle = Mathf.Lerp (0, -90, (-rigidbody2D.velocity.y / 3f) );
			transform.rotation = Quaternion.Euler (0, 0, angle);
		}

	}

	void OnCollisionEnter2D(Collision2D collision) {
		if(godMode)
			return;
		animator.SetTrigger ("Death");
		dead = true;
		deathCooldown = 0.5f;
	}
}
