using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {

	[SerializeField] AudioClip _clipCollide; 

	void OnCollisionEnter2D(Collision2D otro){
		if (otro.gameObject.tag == "Ball") {
			MusicManager.Instance.playSound (_clipCollide);
		}
	}
}
