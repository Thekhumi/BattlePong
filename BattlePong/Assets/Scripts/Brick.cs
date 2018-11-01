using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour {
	[SerializeField] GameObject _prefab;
	[SerializeField] float _dropChance = 20f;
	float delay;
	AudioClip clip;
	GameObject _powerUp;
	void Awake () {
		clip = GetComponent<AudioSource>().clip;
	}

	void OnCollisionEnter2D(Collision2D otro){
		if (otro.gameObject.tag == "Ball") {
			AudioSource.PlayClipAtPoint (clip, transform.position);
			float chance = Random.value;
			if (_dropChance / 100 > chance) {
				_powerUp = Instantiate (_prefab);
				_powerUp.transform.position = transform.position;
				int multiplier = otro.collider.GetComponent<Ball> ().Velocity.x > 0 ? 1 : -1;
				_powerUp.GetComponent<PowerUp> ().Speed *= multiplier;
			}
			gameObject.SetActive (false);
		}
	}
}
