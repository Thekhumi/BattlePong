using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {

	AudioSource audioSrc;
	void Awake () {
		audioSrc = GetComponent<AudioSource> ();
	}

	void OnCollisionEnter2D(Collision2D otro){
		if (otro.gameObject.tag == "Ball") {
			audioSrc.Play ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
