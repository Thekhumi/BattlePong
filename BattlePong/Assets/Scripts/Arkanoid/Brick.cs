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
	void OnTriggerEnter2D(Collider2D otro){
		if (otro.gameObject.tag == "Laser") {
			AudioSource.PlayClipAtPoint (clip, transform.position);
			spawnPowerUp (otro.GetComponent<Laser> ().dirMultiplier > 0 ? 1 : -1);
			gameObject.SetActive (false);
		}
	}	

	/*void OnCollisionEnter2D(Collision2D otro){
		if (otro.gameObject.tag == "Ball"||otro.gameObject.tag=="MultiBall") {
			
		}
	}
	*/

	public void breakBrick(GameObject otro){
		AudioSource.PlayClipAtPoint (clip, transform.position);
		spawnPowerUp (otro.GetComponent<Ball> ().Velocity.x > 0 ? 1 : -1);
		gameObject.SetActive (false);
	}
	void spawnPowerUp(int multiplier){
		float chance = Random.value;
		if (_dropChance / 100 > chance) {
			_powerUp = Instantiate (_prefab);
			_powerUp.transform.position = transform.position;
			_powerUp.GetComponent<PowerUp> ().Speed *= multiplier;
		}
	}
}
