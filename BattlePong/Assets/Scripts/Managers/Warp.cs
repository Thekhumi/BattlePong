﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warp : MonoBehaviour {
	private bool _out;
	private GameObject[] _portals;
	private Ball _ball;
	private int _rand;
	[SerializeField] private bool _onScreen;
	[SerializeField] private float _ballDelay;

	void Start () {
		_portals = GameObject.FindGameObjectsWithTag ("Warp");
		_ball = GameObject.FindGameObjectWithTag ("Ball").GetComponent<Ball> ();
		_out = false;
		_onScreen = false;
	}

	void Update(){
		if (gameObject.GetComponent<Renderer> ().isVisible) {
			_onScreen = true;
		} else {
			_onScreen = false;
		}
	}

	void OnTriggerEnter2D(Collider2D otro){
		if (!_out) {
			if (otro.tag == "Ball") {
				_ball.Stop ();
				_rand = Randomizer();
				while (_portals [_rand] == gameObject || !_portals [_rand].GetComponent<Warp>().OnScreen) {
					_rand = Randomizer();
				}
				_portals [_rand].GetComponent<Warp> ().Out = true;
				otro.transform.position = _portals [_rand].transform.position - (new Vector3 (0f, 0f, 2f));
				Invoke ("Reactivate", _ballDelay);
			}
		}
	}
	void OnTriggerExit2D(Collider2D otro){
		if (_out) {
			_out = false;
		}
	}
	private void Reactivate(){
		_ball.gameObject.transform.position+=(new Vector3 (0f, 0f, 2f));
		_ball.SoftReset();
	}
	private int Randomizer(){
		int rand = Random.Range (0, _portals.Length);
		return rand;
	}
	public bool Out{
		get{ return _out; }
		set{ _out = value; }
	}
	public bool OnScreen{
		get{ return _onScreen; }
		set{ _onScreen = value; }
	}
}