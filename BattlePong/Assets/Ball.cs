using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {
	[SerializeField] float speed = 5f;
	// Use this for initialization
	void Start () {
		float sx = Random.Range (0, 2) == 0 ? -1 : 1;
		float sy = Random.Range (0, 2) == 0 ? -1 : 1;

		GetComponent<Rigidbody2D> ().velocity = new Vector2 (speed * sx, speed * sy);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
