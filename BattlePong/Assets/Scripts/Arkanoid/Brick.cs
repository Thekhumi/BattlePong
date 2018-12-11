using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour {
	[SerializeField] GameObject _prefab;
	[SerializeField] float _dropChance = 20f;
	[SerializeField] AudioClip _clipBrickBreak;
	float delay;
	GameObject _powerUp;

	void OnTriggerEnter2D(Collider2D otro){
		if (otro.gameObject.tag == "Laser") {
			MusicManager.Instance.playSound (_clipBrickBreak);
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
		MusicManager.Instance.playSound (_clipBrickBreak);
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
