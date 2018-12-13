using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tube : MonoBehaviour {
	float _startTime;
	[SerializeField] bool _isRising = false;
	[SerializeField] float _switchTime = 3f;
	[SerializeField] float speed = 1;
	[SerializeField] bool _isBubble;
	float timer;
	// Use this for initialization
	void Start () {
		if (!_isBubble) {
			_startTime = Random.Range (1f, 3f);
		} else {
			_startTime = 0f;
		}
		Invoke ("Move", _startTime);
	}
	
	// Update is called once per frame
	void Update () {
		if (_isRising) {
			transform.Translate (0, speed * Time.deltaTime, 0);
		} else {
			transform.Translate (0, -speed * Time.deltaTime, 0);
		}
	}

	void Move(){
		_isRising = !_isRising;
		Invoke ("Move", _switchTime);
	}
}
