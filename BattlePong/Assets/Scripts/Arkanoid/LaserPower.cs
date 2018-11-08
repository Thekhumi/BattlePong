using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPower : MonoBehaviour {
	[SerializeField] GameObject _prefab;
	[SerializeField] float _rateOfFire = 1f;
	float _timer;
	bool _isLeft;
	[SerializeField] bool _active = false;
	float _shootTimer;
	GameObject _laser;
	void Awake(){
		_isLeft = GetComponent<Bumper> ().isLeft;
	}

	void Update () {
		_shootTimer -= Time.deltaTime;
		if(_shootTimer <= 0){
			_shootTimer = 0;
			if (_isLeft && _active) {
				if (Input.GetButton ("Fire1")) {
					_laser = Instantiate(_prefab);
					_laser.GetComponent<Laser> ().dirMultiplier = 1;
					_laser.transform.position = transform.position;
					_shootTimer = _rateOfFire;
				}
			} else {
				if (Input.GetButton ("Fire2")) {
					_laser = Instantiate (_prefab);
					_laser.GetComponent<Laser> ().dirMultiplier = -1;
					_laser.transform.position = transform.position;
					_shootTimer = _rateOfFire;
				}
			}
		}

	}
	public bool Active{
		get{ return _active; }
		set{ _active = value; }
	}
}
