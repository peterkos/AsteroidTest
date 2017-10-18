using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour {

	// Unorganized ivars
	public float speed;
	public Rigidbody2D rigid2D;
	public ParticleSystem ps;
	public bool isColliding;
	public Rigidbody2D hexagon;
	public Camera camera;


	void Start () {
		rigid2D = GetComponent<Rigidbody2D>();
		camera = Camera.main;
		speed = 10;
		isColliding = false;

		InvokeRepeating("spawnHexagon", 1.0f, 0.4f);
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetKeyDown(KeyCode.RightArrow))
		rigid2D.velocity = new Vector2(speed, rigid2D.velocity.y);

		if(Input.GetKeyDown(KeyCode.LeftArrow))
		rigid2D.velocity = new Vector2(-speed, rigid2D.velocity.y);

		if(Input.GetKeyDown(KeyCode.UpArrow)) {
			// If pushing object, push it faster
			rigid2D.velocity = new Vector2(rigid2D.velocity.x, isColliding ? speed : speed * 1.3f);
		}

		if(Input.GetKeyDown(KeyCode.DownArrow))
		rigid2D.velocity = new Vector2(rigid2D.velocity.x, -speed);


		// Cancel velocity in respective direction if key is not being held anymore
		if (Input.anyKey == true) {
			if(Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow)) {
				rigid2D.velocity = new Vector2(0, rigid2D.velocity.y);
			} else if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.UpArrow)) {
				rigid2D.velocity = new Vector2(rigid2D.velocity.x, 0);
			}

		// Otherwise, stop the triangle if nothing is held down
		} else {
			rigid2D.velocity = new Vector2(0, 0);
			//TODO: Hanlde collision velocity
		}


	}

	void spawnHexagon() {
		// TODO: Restrict margin so the entire object is accessible for the player onscreen
		float rnd = Random.Range(0.0f, 1.0f);
		Vector3 spawnPos = new Vector3(Screen.width * rnd, Screen.height + 100, 0);
		Vector3 pos = camera.ScreenToWorldPoint(spawnPos);
		pos.z = 0;

			// Instantiate clone
		Rigidbody2D hexagonClone = Object.Instantiate(hexagon, pos, transform.rotation) as Rigidbody2D;
	}


	void OnCollisionEnter2D(Collision2D coll) {
		print("Collision!");
		if (coll.gameObject.tag == "Enemy") {

			// Turn on particle effect
			if (gameObject.GetComponent<ParticleSystem>() == null) {
				gameObject.AddComponent(typeof(ParticleSystem));	
			}

			ps = gameObject.GetComponent<ParticleSystem>();

			// Configure the particle system
			var main = ps.main;
			var shape = ps.shape;
			var emission = ps.emission;
			var lv = ps.limitVelocityOverLifetime;
			var inv = ps.inheritVelocity;

			lv.enabled = true;
			lv.separateAxes = true;
			lv.dampen = 0.2f;

			inv.enabled = true;
			inv.mode = ParticleSystemInheritVelocityMode.Current;

			main.gravityModifier = 10;
			main.startSize = 0.1f;
			main.simulationSpace = ParticleSystemSimulationSpace.World;

			emission.enabled = true;
			emission.rateOverTime = 300;
			
			shape.angle = 30;
			shape.radius = 0.01f;

			print("hell yea");

		}
	}

	void OnCollisionExit2D(Collision2D coll) {
		print("Anti-Collision!");
		if (coll.gameObject.tag == "Enemy") {
				// print("Destroyed.");
				// Destroy(GetComponent<ParticleSystem>());

				// Slow down the triangle to regular speed
			speed = 10;

				// Turn off rocket
			var emission = ps.emission;
			emission.enabled = false;
		}

	}
}
