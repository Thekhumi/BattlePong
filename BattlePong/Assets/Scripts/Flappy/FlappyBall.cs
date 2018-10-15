using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlappyBall : MonoBehaviour {

	[SerializeField] private Sprite _flappy1;
	[SerializeField] private Sprite _flappy2;
	[SerializeField] private float _flapDelay;
	private SpriteRenderer _renderer;
	private bool _flap;
	Rigidbody2D _body;

	void Awake () {
		_renderer = gameObject.GetComponent<SpriteRenderer> ();
		_body = gameObject.GetComponent<Rigidbody2D> ();
	}

	void Start () {
		_renderer.sprite = _flappy1;
		_flap = true;
		Invoke ("Flap",_flapDelay);
	}
	void Update(){
		if (_body.velocity.x<0) {
			_renderer.flipX = false;
		}
		if (_body.velocity.x>0) {
			_renderer.flipX = true;
		}
	}
	private void Flap(){
		switch (_flap) {
		case true:
			_renderer.sprite = _flappy2;
			_flap = false;
			break;
		case false:
			_renderer.sprite = _flappy1;
			_flap = true;
			break;
		}
		Invoke ("Flap",_flapDelay);
	}
}
