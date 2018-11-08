using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour {
	[SerializeField] private float _delay;
	[SerializeField] private float _jumpDelay;
	[SerializeField] Sprite _green;
	[SerializeField] Sprite _jump1;
	[SerializeField] Sprite _jump2;
	[SerializeField] Sprite _red;
	[SerializeField] float _impulse;
	private bool _active;
	private int _count;
	private Rigidbody2D _body;
	private SpriteRenderer _objSprite;

	void Awake(){
		_objSprite= GetComponent<SpriteRenderer>();
	}
	void Start(){
		_active = true;
		_count = 1;
	}
	void OnCollisionEnter2D(Collision2D otro){
		if (_active) {
			if (otro.gameObject.tag == "PinballBall") {
				_body=otro.gameObject.GetComponent<Rigidbody2D> ();
				_body.AddForce (transform.up*_impulse,ForceMode2D.Impulse);
				Jump ();
			}
		}
	}
	private void Jump(){
		switch (_count) {
		case 1:
			_objSprite.sprite = _jump1;
			_count++;
			Invoke ("Jump", _jumpDelay);
			break;
		case 2:
			_objSprite.sprite = _green;
			_count++;
			Invoke ("Jump", _jumpDelay);
			break;
		case 3:
			_objSprite.sprite = _jump2;
			_count++;
			Invoke ("Deactivate",_delay);
			break;
		}
	}
	private void Deactivate(){
		_active = false;
		_objSprite.sprite = _red;
		_objSprite.color -= new Color (0.0f, 0.0f, 0.0f, 0.7f);
		gameObject.GetComponent<BoxCollider2D> ().enabled = false;
	}
	public void Reactivate(){
		_active = true;
		_count = 1;
		_objSprite.sprite = _green;
		_objSprite.color = new Color (1.0f, 1.0f, 1.0f, 1.0f);
		gameObject.GetComponent<BoxCollider2D> ().enabled = true;
	}
}
