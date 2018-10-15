using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {
	[SerializeField] float speed = 5f;
	[SerializeField] float sum = 0f;
	[SerializeField] GameManager _manager;
	[SerializeField] float _minSpeedX = 0;
	[SerializeField] float _bounceControl = 10f;
	[SerializeField] float _inmuneTime = 0.3f;
	[SerializeField] float _boostTime = 3f;
	float _boostTimer;
	float _inmuneTimer;
	private float _originalSpeed;
	private bool _scored = false;
	float sx;
	float sy;

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
		if (otro.gameObject.tag == "BreakableWall") {
			otro.gameObject.SetActive (false);
		}
		else{
			switch (otro.gameObject.layer) {
			case 8:
				speed += sum;
				break;
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
			if (_inmuneTimer < 0) {
				body.velocity = new Vector2 (-body.velocity.x, (transform.position.y - otro.transform.position.y) * _bounceControl);
				speed += sum;
				_inmuneTimer = _inmuneTime;
				_boostTimer = _boostTime;
			}
			break;
		}
	}
	void Update(){
		_inmuneTimer -= Time.deltaTime;
		_boostTimer -= Time.deltaTime;
		if(_boostTimer <= 0){
			_boostTimer = 0;
		}
	}
	void FixedUpdate () {
		minSpeedCheck ();
		body.velocity = (speed + _boostTimer) * (body.velocity.normalized);
	}
	public void Reset(){
		speed = _originalSpeed;
		sx = Random.Range (0, 2) == 0 ? -1 : 1;
		sy = Random.Range (0, 2) == 0 ? -1 : 1;
		body.velocity = new Vector2 (speed * sx, speed * sy);
		_scored = false;
	}
	public void SoftReset(){
		speed = _originalSpeed;
		body.velocity = new Vector2 (speed * sx, speed * sy);
	}
	public void Stop(){
		body.velocity = Vector2.zero;
	}
	public void ResetPosition(){
		transform.position = transform.parent.position;
	}
	public bool Scored{
		get{return _scored;}
		set{_scored=value;}
	}
	public void minSpeedCheck(){
		if (body.velocity.x > 0 && body.velocity.x < _minSpeedX) {
			body.velocity = new Vector2 (_minSpeedX, body.velocity.y);
		} else if (body.velocity.x < 0 && body.velocity.x > -_minSpeedX) {
			body.velocity = new Vector2 (-_minSpeedX, body.velocity.y);
		}
	}
}
