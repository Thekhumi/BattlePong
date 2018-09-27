using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {
	[SerializeField] float speed = 5f;
	[SerializeField] float sum = 0f;
	[SerializeField] GameManager _manager;
	private float _originalSpeed;
	private bool _scored = false;
	float sx;
	float sy;

	Rigidbody2D body;
	// Use this for initialization
	void Start () {
		sx = Random.Range (0, 2) == 0 ? -1 : 1;
		sy = Random.Range (0, 2) == 0 ? -1 : 1;
		body = GetComponent<Rigidbody2D>();
		body.velocity = new Vector2 (speed * sx, speed * sy);
		_originalSpeed = speed;
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

	void FixedUpdate () {
		body.velocity = speed * (body.velocity.normalized);
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
		speed = 0;
	}
	public void ResetPosition(){
		transform.position = transform.parent.position;
	}
	public bool Scored{
		get{return _scored;}
		set{_scored=value;}
	}
}
