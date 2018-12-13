using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour {

	[SerializeField] Transform _ball;
	[SerializeField] private bool _active;
	[SerializeField] float _mag;
	[SerializeField] Transform _outTarget;
	[SerializeField] Transform _inTarget;
	[SerializeField] Transform _leftB;
	[SerializeField] Transform _rightB;
	[SerializeField] Sprite _whiteGhost;
	[SerializeField] Sprite _blueGhost;
	[SerializeField] Sprite _redGhost;
	private bool _inside;
	private bool _doubting;
	private bool _atkLeft;
	private float _timer;
	Rigidbody2D _rb;
	SpriteRenderer _rend;

	void Awake (){
		_rb = _ball.GetComponent<Rigidbody2D> ();
		_rend = gameObject.GetComponent<SpriteRenderer> ();
	}

	void Start () {
		_active = false;
		_atkLeft = false;
		_timer = 0;
		_doubting = true;
		_inside = false;
		transform.position = _outTarget.position;
	}

	void Update () {
		if (_active) {
			if (!_inside) {
				transform.position = Vector3.MoveTowards (transform.position, _inTarget.position, _mag * Time.deltaTime);
				if (transform.position == _inTarget.position) {
					_inside = true;
				}
			}

			if (_inside) {
				_timer += Time.deltaTime;

				if (_timer >= 10) {
					_doubting = false;
				}

				switch (_doubting) {
				case true:
					transform.position = Vector3.MoveTowards (transform.position, _ball.position, _mag * Time.deltaTime);
					if (_rb.velocity.x < 0) {
						_rend.flipX = false;
						_atkLeft = true;
					}
					if (_rb.velocity.x > 0) {
						_rend.flipX = true;
						_atkLeft = false;
					}
					break;

				case false:
					switch (_atkLeft) {
					case true:
						transform.position = Vector3.MoveTowards (transform.position, (_leftB.position + new Vector3 (-2f, 0f, 0f)), _mag * Time.deltaTime);
						gameObject.GetComponent<SpriteRenderer> ().sprite = _redGhost;
						break;
					case false:
						transform.position = Vector3.MoveTowards (transform.position, (_rightB.position + new Vector3 (2f, 0f, 0f)), _mag * Time.deltaTime);
						gameObject.GetComponent<SpriteRenderer> ().sprite = _blueGhost;
						break;
					}
					break;
				}
			}
		}
	}
	public void Reset(){
		gameObject.GetComponent<SpriteRenderer> ().sprite = _whiteGhost;
		_active = false;
		_doubting = true;
		_timer = 0;
		_inside = false;
		transform.position = _outTarget.position;
	}
	public bool Active{
		get{ return _active;}
		set{ _active = value;}
	}
}
