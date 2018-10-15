using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlappyBall : MonoBehaviour {

	[SerializeField] private Sprite _flappy1Center;
	[SerializeField] private Sprite _flappy2Center;
	[SerializeField] private Sprite _flappy1Left;
	[SerializeField] private Sprite _flappy2Left;
	[SerializeField] private Sprite _flappy1Right;
	[SerializeField] private Sprite _flappy2Right;
	[SerializeField] private float _flapDelay;
	private SpriteRenderer _renderer;
	private bool _flap;
	Rigidbody2D _body;
	private enum ScreenSwitch {CENTER, LEFT, RIGHT};
	private ScreenSwitch _screen;

	void Awake () {
		_renderer = gameObject.GetComponent<SpriteRenderer> ();
		_body = gameObject.GetComponent<Rigidbody2D> ();
	}

	void Start () {
		_renderer.sprite = _flappy1Center;
		_flap = true;
		Invoke ("Flap",_flapDelay);
		_screen = ScreenSwitch.CENTER;
	}
	void Update(){
		if (_body.velocity.x<0) {
			_renderer.flipX = false;
		}
		if (_body.velocity.x>0) {
			_renderer.flipX = true;
		}
		if (gameObject.transform.position.x > -12 && gameObject.transform.position.x < 12) {
			_screen = ScreenSwitch.CENTER;
		}
		if (gameObject.transform.position.x > 12) {
			_screen = ScreenSwitch.RIGHT;
		}
		if (gameObject.transform.position.x < -12) {
			_screen = ScreenSwitch.LEFT;
		}
		Debug.Log (_screen);
	}
	private void Flap(){
		switch (_screen) {
		case ScreenSwitch.LEFT:
			switch (_flap) {
			case true:
				_renderer.sprite = _flappy2Left;
				_flap = false;
				break;
			case false:
				_renderer.sprite = _flappy1Left;
				_flap = true;
				break;
			}
			break;
		case ScreenSwitch.CENTER:
			switch (_flap) {
			case true:
				_renderer.sprite = _flappy2Center;
				_flap = false;
				break;
			case false:
				_renderer.sprite = _flappy1Center;
				_flap = true;
				break;
			}
			break;
		case ScreenSwitch.RIGHT:
			switch (_flap) {
			case true:
				_renderer.sprite = _flappy2Right;
				_flap = false;
				break;
			case false:
				_renderer.sprite = _flappy1Right;
				_flap = true;
				break;
			}
			break;
		}
		Invoke ("Flap",_flapDelay);
	}
}
