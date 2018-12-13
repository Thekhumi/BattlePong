using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleEnemy : MonoBehaviour {

	[SerializeField] Sprite _normal1;
	[SerializeField] Sprite _normal2;
	[SerializeField] Sprite _bubble1;
	[SerializeField] Sprite _bubble2;
	[SerializeField] float _flickerTime;
	[SerializeField] Transform _targetOut;
	[SerializeField] Transform _targetIn;
	[SerializeField] float _moveVel;
	[SerializeField] float _popOutTime;
	[SerializeField] int _sprite;
	bool _bubbled;

	private SpriteRenderer _thisSprite;
	void Start () {
		Invoke ("Flicker", _flickerTime);
		_thisSprite = gameObject.GetComponent<SpriteRenderer> ();
		_bubbled = false;
		_sprite = 1;
	}
	void Update () {
		if (_bubbled) {
			gameObject.transform.position = Vector3.MoveTowards (transform.position, _targetOut.position, _moveVel);
		}
		if (!_bubbled) {
			gameObject.transform.position = Vector3.MoveTowards (transform.position, _targetIn.position, _moveVel);
		}
	}
	void OnCollisionEnter2D (Collision2D otro){
		_sprite = 3;
		_bubbled = true;
		Invoke ("PopOut", _popOutTime);
	}
	void Flicker(){
		Debug.Log (_sprite);
		switch (_sprite) {
		case 1:
			_thisSprite.sprite = _normal2;
			_sprite = 2;
			break;
		case 2:
			_thisSprite.sprite = _normal1;
			_sprite = 1;
			break;
		case 3:
			_thisSprite.sprite = _bubble2;
			_sprite = 4;
			break;
		case 4:
			_thisSprite.sprite = _bubble1;
			_sprite = 3;
			break;
		}
		Invoke ("Flicker", _flickerTime);
	}
	private void PopOut(){
		_bubbled = false;
		_sprite = 1;
	}
}
