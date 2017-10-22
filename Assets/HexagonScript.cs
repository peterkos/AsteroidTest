using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hexscript : MonoBehaviour {

	public SpriteRenderer hexSprite;
	public Camera camera;


	// Use this for initialization
	void Start () {
		hexSprite = gameObject.GetComponent<SpriteRenderer>();
		camera = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnBecameInvisible() {

		// Check if object is also BELOW the screen
		// This is because objects are spawned off the top of the screen, which
		// would despawn the object immediatly using the default check.
		
		Vector3 heightTest = camera.ScreenToWorldPoint(new Vector3(0, 0, 0));

		// Compare to check if the entire hexagon is offscreen
		print(hexSprite.transform.position.y);
		if ((hexSprite.transform.position.y + hexSprite.transform.localScale.y) < 0) {
			Destroy(gameObject);
			print("HELL YEA");

		}
	}

}
