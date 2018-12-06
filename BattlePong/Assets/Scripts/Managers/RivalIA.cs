using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RivalIA : MonoBehaviour {

	[SerializeField] GameObject _ball;
	private float _velocity;
	[SerializeField] private float _hitVariance;
	private float _minDistance = 10.0f;
	[Header("Difficulty Variables")]
	[SerializeField] private float _easyVelocity;
	[SerializeField] private float _minDistanceEasy;
	[SerializeField] private float _normalVelocity;
	[SerializeField] private float _minDistanceNormal;
	[SerializeField] private float _hardVelocity;
	[SerializeField] private float _minDistanceHard;
	private float  _targetVariance;
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
	void Start(){
		updateDifficulty ();
		refreshVariance ();
	}

	void OnTriggerEnter2D(Collider2D otro){
		if (otro.gameObject.tag == "Ball") {
			refreshVariance ();
		}
	}
	void Update () {
		_target = new Vector3 (transform.position.x, _ball.transform.position.y + _targetVariance, transform.position.z);
		if (_ball.GetComponent<Rigidbody2D> ().velocity.x < 0 && _ball.transform.position.x - transform.position.x < _minDistance) {
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

	private void refreshVariance(){
		_targetVariance = Random.Range (-_hitVariance / 2, _hitVariance / 2);
	}
	private void updateDifficulty(){
		PlayerManager.Diff _diff = PlayerManager.Instance.Difficulty;
		switch (_diff) {
		case PlayerManager.Diff.EASY:
			_velocity = _easyVelocity;
			_minDistance = _minDistanceEasy;
			break;
		case PlayerManager.Diff.NORMAL:
			_velocity = _normalVelocity;
			_minDistance = _minDistanceNormal;
			break;
		case PlayerManager.Diff.HARD:
			_velocity = _hardVelocity;
			_minDistance = _minDistanceHard;
			break;
		}
	}
}
