using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour {

	float delay;
	AudioClip clip;
	void Awake () {
		clip = GetComponent<AudioSource>().clip;
	}

	void OnCollisionEnter2D(Collision2D otro){
		if (otro.gameObject.tag == "Ball") {
			AudioSource.PlayClipAtPoint (clip, transform.position);
			gameObject.SetActive (false);
		}
	}
		
	// Update is called once per frame
	void Update () {
		
	}
}
