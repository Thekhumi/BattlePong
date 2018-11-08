using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {

	[SerializeField] float _speed;
	[SerializeField] int _dirMultiplier;
	void Update () {
		transform.Translate (_speed * _dirMultiplier * Time.deltaTime,0f,0f);
	}

	void OnTriggerEnter2D(Collider2D otro){
		if (otro.gameObject.tag == "Bumper" || otro.gameObject.layer == 9 || otro.gameObject.layer == 10 || otro.gameObject.tag == "BreakableWall" || otro.gameObject.tag == "Ball") {
			Destroy(gameObject);
		}
	}
	public int dirMultiplier{
		get{ return _dirMultiplier; }
		set{ _dirMultiplier = value; }
	}

}
