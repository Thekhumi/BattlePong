using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {

	[SerializeField] float _speed;
	[SerializeField] int _dirMultiplier;
	void Update () {
		transform.Translate (_speed * _dirMultiplier * Time.deltaTime,0f,0f);
	}
	public int dirMultiplier{
		get{ return _dirMultiplier; }
		set{ _dirMultiplier = value; }
	}

}
