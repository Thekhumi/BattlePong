using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warp : MonoBehaviour {
	private bool _out;
	private GameObject[] _portals;
	private Ball _ball;
	private int _rand;
	private Vector2 _origVel;
	private bool _startDelay;
	private Color _color;
	[SerializeField] bool _isBubble;
	[SerializeField] private bool _onScreen;
	[SerializeField] private float _ballDelay;
	[SerializeField] private float _delay;
	[SerializeField] AudioClip _clipWarp;


	void Start () {
		_portals = GameObject.FindGameObjectsWithTag ("Warp");
		_ball = GameObject.FindGameObjectWithTag ("Ball").GetComponent<Ball> ();
		_out = false;
		_onScreen = false;
		_startDelay = false;
		Invoke ("StartDelay", _delay);
		_color = new Color (0.0f, 0.0f, 0.0f, 0.5f);
		gameObject.GetComponent<SpriteRenderer> ().color -= _color;
	}

	void Update(){
		if (gameObject.GetComponent<Renderer> ().isVisible) {
			_onScreen = true;
		} else {
			_onScreen = false;
		}
	}

	void OnTriggerEnter2D(Collider2D otro){
		if(_startDelay){
			if (!_out) {
				if (otro.tag == "Ball") {
					if (!_isBubble) {
						MusicManager.Instance.playSound (_clipWarp);
					}
					_origVel = otro.GetComponent<Rigidbody2D> ().velocity;
					_ball.gameObject.GetComponent<TrailRenderer> ().enabled = false;
					_ball.Stop ();
					_rand = Randomizer();
					while (_portals [_rand] == gameObject || !_portals [_rand].GetComponent<Warp>().OnScreen) {
						_rand = Randomizer();
					}
					_portals [_rand].GetComponent<Warp> ().Out = true;
					otro.transform.position = _portals [_rand].transform.position - (new Vector3 (0f, 0f, 2f));
					otro.GetComponent<Rigidbody2D> ().velocity = _origVel;
					Invoke ("Reactivate", _ballDelay);
				}
			}
		}
	}
	void OnTriggerExit2D(Collider2D otro){
		if (_out) {
			_out = false;
		}
	}
	public void StartDelay(){
		_startDelay = true;
		gameObject.GetComponent<SpriteRenderer> ().color += _color;
	}
	public void DeactivateDelay(){
		_startDelay = false;
		gameObject.GetComponent<SpriteRenderer> ().color -= _color;
		Invoke ("StartDelay", _delay);
	}
	private void Reactivate(){
		_ball.gameObject.transform.position+=(new Vector3 (0f, 0f, 2f));
		_ball.Trail ();
	}
	private int Randomizer(){
		int rand = Random.Range (0, _portals.Length);
		return rand;
	}
	public bool Out{
		get{ return _out; }
		set{ _out = value; }
	}
	public bool OnScreen{
		get{ return _onScreen; }
		set{ _onScreen = value; }
	}
}
