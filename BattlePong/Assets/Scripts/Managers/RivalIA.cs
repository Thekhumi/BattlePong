using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RivalIA : MonoBehaviour {

	[SerializeField] GameObject _ball;
	[SerializeField] private float _velocity;
	private float _dif;
	private float _direction;
	private SpriteRenderer _this;
	private SpriteRenderer _wallBound;
	private Vector3 _target;
	private Camera _cam;

	void Awake(){
		_cam = Camera.main;
		_this = GetComponent<SpriteRenderer> ();
		_wallBound=GameObject.FindGameObjectsWithTag ("BoundWall")[0].GetComponent<SpriteRenderer> ();
	}

	void Update () {
		_target = new Vector3 (transform.position.x, _ball.transform.position.y, transform.position.z);
		if (_ball.GetComponent<Rigidbody2D> ().velocity.x < 0) {
			transform.position = Vector3.MoveTowards (transform.position, _target, _velocity * Time.deltaTime);
		}
		BoundsCheck ();
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
