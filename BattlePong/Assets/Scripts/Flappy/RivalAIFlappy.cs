using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RivalAIFlappy : MonoBehaviour {

	[SerializeField] GameObject _ball;
	[SerializeField] private float _velocity;
	[SerializeField] private float _waitSpeed = 0f;
	[SerializeField] private float _minDistance = 10f;
	private float _dif;
	private float _direction;
	private SpriteRenderer _this;
	private SpriteRenderer _wallBound;
	private Camera _cam;
	private Rigidbody2D _rb;

	void Awake(){
		_rb = GetComponent<Rigidbody2D> ();
		_cam = Camera.main;
		_this = GetComponent<SpriteRenderer> ();
		_wallBound=GameObject.FindGameObjectsWithTag ("BoundWall")[0].GetComponent<SpriteRenderer> ();
	}

	void Update () {
		if (_ball.transform.position.x - transform.position.x < _minDistance &&_ball.GetComponent<Rigidbody2D>().velocity.x < 0 &&
			_ball.transform.position.y > transform.position.y && _rb.velocity.y < _waitSpeed){
			_rb.velocity = Vector2.zero;
			_rb.AddForce (new Vector2 (0f, _velocity),ForceMode2D.Impulse);
		}
		BoundsCheck();
	}
	protected void BoundsCheck(){
		if(_this.bounds.max.y > _cam.ViewportToWorldPoint(Vector3.one).y - _wallBound.bounds.extents.y*2){
			transform.position = new Vector3(transform.position.x,
				_cam.ViewportToWorldPoint(Vector3.one).y - _this.bounds.extents.y - _wallBound.bounds.extents.y*2,
				transform.position.z);

		}
		else if(_this.bounds.min.y < _cam.ViewportToWorldPoint(Vector3.zero).y + _wallBound.bounds.extents.y*2){
			transform.position = new Vector3(transform.position.x,
				_cam.ViewportToWorldPoint (Vector3.zero).y + _this.bounds.extents.y + _wallBound.bounds.extents.y*2,
				transform.position.z);
		}
	}
}
