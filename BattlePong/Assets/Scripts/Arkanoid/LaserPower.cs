using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPower : MonoBehaviour {
	[SerializeField] GameObject _prefab;
	[SerializeField] float _rateOfFire = 1f;
	[SerializeField] int _maxAmmo = 5;
	float _timer;
	bool _isLeft;
	[SerializeField] bool _active = false;
	float _shootTimer;
	GameObject _laser;
	int _ammo = 5;
	void Awake(){
		_isLeft = GetComponent<Bumper> ().isLeft;
	}

	void Update () {
		_shootTimer -= Time.deltaTime;
		if(_shootTimer <= 0){
			_shootTimer = 0;
			if (_active) {
				if (Input.GetButton ("Fire1") && _isLeft) {
					Shoot ();
				}  
				if (Input.GetButton ("Fire2") && !_isLeft) {
					Shoot ();
				}
			}
		}
		if (_ammo <= 0) {
			_active = false;
		}
	}

	public void Shoot(){
		int spawnSide = 2;
		int multiplier = 1;
		if (!_isLeft) {
			spawnSide = -spawnSide;
			multiplier = -multiplier;
		}
		_laser = Instantiate (_prefab);
		_laser.GetComponent<Laser> ().dirMultiplier = multiplier;
		_laser.transform.position = transform.position;
		_laser.transform.Translate (spawnSide, 0, 0);
		_shootTimer = _rateOfFire;
		_ammo--;
	}
	public bool Active{
		get{ return _active; }
		set{ _active = value; }
	}

	public bool Ready{
		get{ return (_active && _shootTimer <= 0);}
	}
	public void Reload(){
		_ammo = _maxAmmo;
		_active = true;
	}
}
