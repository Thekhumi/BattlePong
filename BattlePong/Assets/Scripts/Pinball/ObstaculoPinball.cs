using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaculoPinball : MonoBehaviour {

	[SerializeField] AudioClip _clipCollide;

	void OnCollisionEnter2D(Collision2D otro){
		if (otro.gameObject.tag == "PinballBall") {
			MusicManager.Instance.playSound (_clipCollide);
		}
	}
}
