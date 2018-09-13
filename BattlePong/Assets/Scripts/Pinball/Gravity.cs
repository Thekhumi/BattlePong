using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour {
	private Vector2 _leftGravity=new Vector2(-9.8f,0.0f);
	private Vector2 _rightGravity=new Vector2(9.8f,0.0f);
	private Vector3 _half=new Vector3(0.5f,0.0f,0.0f);
	private Camera _camera;
	private GameObject _ball;

	void Start(){
		_ball = GameObject.FindGameObjectWithTag ("PinballBall");
		_camera = Camera.main;
	}
	void Update(){
		if (_ball.transform.position.x < _camera.ViewportToWorldPoint (_half).x) {
			Physics2D.gravity = _leftGravity;
		}
		if (_ball.transform.position.x > _camera.ViewportToWorldPoint (_half).x) {
			Physics2D.gravity = _rightGravity;
		}
	}
}