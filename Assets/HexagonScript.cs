using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexagonScript : MonoBehaviour {


	public SpriteRenderer hexSprite;
	public Camera camera;
	public PlayerScript playerScript;


	void Start () {

		// Grab the main PlayerScript
		GameObject triangle = GameObject.Find("Triangle");
		playerScript = triangle.GetComponent<PlayerScript>();

		hexSprite = gameObject.GetComponent<SpriteRenderer>();
		camera = Camera.main;
	}

	
	void Update () {
		
	}

	
	void OnBecameInvisible() {

		// First, check if object is also BELOW the screen
		// This is because objects are spawned off the top of the screen, which
		// would despawn the object immediately using the default check.
		if ((hexSprite.transform.position.y + hexSprite.transform.localScale.y) < 0) {
			// Destroy an instance of the hexagon prefab
			// rather than the prefab itself
			Destroy(playerScript.hexagonInstance);
		}
	}

}
