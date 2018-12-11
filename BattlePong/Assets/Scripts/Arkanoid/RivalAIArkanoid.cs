using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RivalAIArkanoid : MonoBehaviour {
	[SerializeField] GameObject _ball;
	[SerializeField] GameObject _multiball1;
	[SerializeField] GameObject _multiball2;
	private float _velocity;
	[SerializeField] private float _hitVariance;
	float _minDistance;
	[SerializeField] float _stunTime = 2f;

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
	float _stunTimer;
	private SpriteRenderer _this;
	private SpriteRenderer _wallBound;
	private GameObject _target;
	private Camera _cam;

	void Awake(){
		_cam = Camera.main;
		_this = GetComponent<SpriteRenderer> ();
		_wallBound=GameObject.FindGameObjectsWithTag ("BoundWall")[0].GetComponent<SpriteRenderer> ();
		_target = _ball;
	}

	void Start(){
		updateDifficulty ();
		refreshVariance ();
	}

	void Update () {
		if (_stunTimer <= 0 &&_target != null && _target.activeSelf && _target.GetComponent<Rigidbody2D> ().velocity.x < 0 && transform.position.x - _target.transform.position.x < _minDistance) {
			GetComponent<Bumper> ().spark.Stop();
			GoToTarget ();
		} else {
			_target = searchClosest("PowerUp");
			if (_target != null && transform.position.x - _target.transform.position.x < _minDistance) {
				GoToTarget ();

			} else {
				if (GetComponent<LaserPower> ().Ready) {
					GetComponent<LaserPower> ().Shoot ();
				}
			}
		}
		_stunTimer -= Time.deltaTime;
		BoundsCheck ();
		searchBall ();
	}
		
	private void GoToTarget(){
		Vector3 targetPosition = new Vector3 (transform.position.x, _target.transform.position.y + _targetVariance, transform.position.z);
		transform.position = Vector3.MoveTowards (transform.position, targetPosition, _velocity * Time.deltaTime);
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

	public void stun(){
		_stunTimer = _stunTime;
	}
	private void searchBall(){
		float ballPosition = _ball.transform.position.x;
		float multiBall1Position = _multiball1.transform.position.x;
		float multiBall2Position = _multiball2.transform.position.x;
			
		if (transform.position.x - ballPosition > transform.position.x - multiBall1Position) {
			_target = _ball;
		}
		else if(transform.position.x - multiBall1Position > transform.position.x - multiBall2Position){
			_target = _multiball1;
		}
		else{	
			_target = _multiball2;
		}
	}

	private GameObject searchClosest(string tag){
		GameObject[] objects;
		objects = GameObject.FindGameObjectsWithTag (tag);
		float distance = Mathf.Infinity;
		GameObject closest = null;
		foreach (GameObject go in objects) {
			if (go.transform.position.x > transform.position.x) {
				float diff = go.transform.position.x - transform.position.x;
				float curDistance = diff;
				if (curDistance < distance) {
					closest = go;
					distance = curDistance;
				}
			}
		}
		return closest;
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


