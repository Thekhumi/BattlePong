using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {
	[SerializeField] float speed = 5f;
	Rigidbody2D body;
	// Use this for initialization
	void Start () {
		float sx = Random.Range (0, 2) == 0 ? -1 : 1;
		float sy = Random.Range (0, 2) == 0 ? -1 : 1;
		body = GetComponent<Rigidbody2D>();
		body.velocity = new Vector2 (speed * sx, speed * sy);
	}

	void OnCollisionEnter2D(Collision2D otro){
		Debug.Log (otro.gameObject.name);	
	}

	void LateUpdate () {
		body.velocity = speed * (body.velocity.normalized);
	}
}
