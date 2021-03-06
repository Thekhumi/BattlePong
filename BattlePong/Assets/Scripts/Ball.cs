﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {
	[SerializeField] float speed = 5f;
	[SerializeField] float sum = 0f;
	[SerializeField] float _minSpeedX = 0;
	[SerializeField] float _bounceControl = 10f;
	[SerializeField] float _boostTime = 3f;
	[SerializeField] float _permanentBoostMax = 5f;
	[SerializeField] float _permanentBoostSum = 1f;
	float _boostTimer;
	private float _originalSpeed;
	private bool _scored = false;
	private float _permanentBoost = 0f;
	float sx;
	float sy;
	bool stop;

	Rigidbody2D body;

	void Awake(){
		body = GetComponent<Rigidbody2D>();
	}

	void Start () {
		_originalSpeed = speed;
		if (_minSpeedX > speed) {
			_minSpeedX = speed;
		}
	}

	void OnCollisionEnter2D(Collision2D otro){
		if (otro.gameObject.tag == "BreakableWall") {
			otro.gameObject.GetComponent<Brick> ().breakBrick (gameObject);
		}
		if (otro.gameObject.tag == "END GAME") {
			switch (otro.gameObject.layer) {
			case 9:
				Stop ();
				break;
			case 10:
				Stop ();
				break;
			}
		}
		else{
			switch (otro.gameObject.layer) {
			case 9:
				_scored = true;
				break;
			case 10:
				_scored = true;
				break;
			}
		}
	}

	void OnTriggerEnter2D(Collider2D otro){
		switch (otro.gameObject.tag) {
		case "Bumper":
				if (otro.GetComponent<Bumper> ().isLeft) {
					body.velocity = new Vector2 (Mathf.Abs (body.velocity.x), (transform.position.y - otro.transform.position.y) * _bounceControl);
				} else {
					body.velocity = new Vector2 (-Mathf.Abs (body.velocity.x), (transform.position.y - otro.transform.position.y) * _bounceControl);
				}
				speed += sum;
				_boostTimer = _boostTime;
			break;
		}
	}
	void Update(){
		_boostTimer -= Time.deltaTime;
		if(_boostTimer < 0){
			_boostTimer = 0;
		}
	}
	void FixedUpdate () {
		minSpeedCheck ();
		body.velocity = (speed + _boostTimer + _permanentBoost) * (body.velocity.normalized);
	}
	public void Reset(){
		gameObject.GetComponent<TrailRenderer> ().enabled = true;
		speed = _originalSpeed;
		sx = Random.Range (0, 2) == 0 ? -1 : 1;
		sy = Random.Range (0, 2) == 0 ? -1 : 1;
		body.velocity = new Vector2 (speed * sx, speed * sy);
		_scored = false;
		stop = false;
	}
	public void SoftReset(){
		speed = _originalSpeed;
		body.velocity = new Vector2 (speed * sx, speed * sy);
		stop = false;
	}
	public void Stop(){
		gameObject.GetComponent<TrailRenderer> ().enabled = false;
		body.velocity = Vector2.zero;
		stop = true;
	}
	public void Trail(){
		gameObject.GetComponent<TrailRenderer> ().enabled = true;
	}
	public void ResetPosition(){
		gameObject.GetComponent<TrailRenderer> ().enabled = false;
		transform.position = transform.parent.position;
	}
	public bool Scored{
		get{return _scored;}
		set{_scored=value;}
	}

	public float permanentBoost{
		get{return _permanentBoost;}
		set{_permanentBoost=value;}
	}
	public float permanentBoostSum{
		get{return _permanentBoostSum;}
	}
	public float permanentBoostMax{
		get{return _permanentBoostMax;}
	}
	public Vector2 Velocity{
		get{return body.velocity;}
	}
	public void minSpeedCheck(){
		//Debug.Log (Mathf.Abs (body.velocity.x) + " " +  _minSpeedX);
		if (!stop && Mathf.Abs(body.velocity.x) < _minSpeedX) {
			if (body.velocity.x > 0) {
				body.velocity = new Vector2 (_minSpeedX, body.velocity.y);
		//		Debug.Log ("MIN SPEED CHECK" + Mathf.Abs(body.velocity.x));
			} else if (body.velocity.x < 0) {
				body.velocity = new Vector2 (-_minSpeedX, body.velocity.y);
		//		Debug.Log ("MIN SPEED CHECK"+ Mathf.Abs(body.velocity.x));
			}
			else{
				sx = Random.Range (0, 2) == 0 ? -1 : 1;
				body.velocity = new Vector2 (_minSpeedX * sx, body.velocity.y);
		//		Debug.Log ("ZERO SPEED");
			}
		}
	}
	public void MultiActive(GameObject mainBall){
		transform.position = mainBall.transform.position;
		gameObject.SetActive (true);
		if (mainBall.GetComponent<Rigidbody2D> ().velocity.x > 0) { sx = 1; }
		if (mainBall.GetComponent<Rigidbody2D> ().velocity.x < 0) { sx = -1; }
		sy = Random.Range (0, 2) == 0 ? -1 : 1;
		Trail ();
		body.velocity = new Vector2 (speed * sx, speed * sy);
		_scored = false;
		stop = false;
	}
	public void MultiStop(){
		gameObject.GetComponent<TrailRenderer> ().enabled = false;
		Stop ();
		ResetPosition ();
		gameObject.SetActive (false);
	}
}
