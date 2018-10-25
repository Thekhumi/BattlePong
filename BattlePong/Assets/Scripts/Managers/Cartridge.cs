using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Cartridge : MonoBehaviour {
	[SerializeField] GameObject[] _cartuchos;
	[SerializeField] Sprite[] _spriteLight;
	[SerializeField] Sprite[] _spriteOff;
	[SerializeField] Sprite[] _textSprites;
	[SerializeField] Transform [] _target;
	[SerializeField] Transform _mainTarget;
	[SerializeField] Transform _insertedTarget;
	[SerializeField] Transform _aBitUp;
	[SerializeField] SpriteRenderer _text;
	[SerializeField] SceneChange _scene;
	[SerializeField] ParticleSystem[] _particles;
	[SerializeField] float _vel;
	[SerializeField] float _waitTime;
	private SpriteRenderer[] _cartSprite;
	private int _cont;
	private int _activated;
	private bool _press;
	private bool _checked;

	void Awake(){
		_cartSprite = new SpriteRenderer[_cartuchos.Length];
		for (int i = 0; i < _cartuchos.Length; i++) {
			_cartSprite [i] = _cartuchos [i].GetComponent<SpriteRenderer> ();
		}
		foreach (var part in _particles) {
			part.Stop ();
		}
	}
	void Start(){
		_cont = 0;
		_activated = 0;
		_press = false;
		_checked = false;
	}
	void Update(){
		if (Input.GetButtonDown ("Left")||Input.GetButtonDown ("Down")) {
			if (_cont != 0) {
				_cont--;
			} else {_cont = 4;}
		}
		if (Input.GetButtonDown ("Right")||Input.GetButtonDown ("Up")) {
			if (_cont != 4) {
				_cont++;
			} else {_cont = 0;}
		}
		if (Input.GetButtonDown ("Submit")) {
			_press = true;
		}
		if (_press) {
			switch (_activated) {
			case 0:
				for (int i = 0; i < _cartuchos.Length; i++) {
					if (i == _cont) {
						_cartuchos [i].transform.position = Vector3.MoveTowards (_cartuchos [i].transform.position, _mainTarget.position, _vel/2);
					} else {
						_cartuchos [i].transform.position = Vector3.MoveTowards (_cartuchos [i].transform.position, _target [i].position, _vel);
						if (_cartuchos [_cont].transform.position == _mainTarget.position) {
							Invoke ("Wait", _waitTime);
						}
					}
				}
				break;
			case 1:
				_cartuchos [_cont].transform.position = Vector3.MoveTowards (_cartuchos [_cont].transform.position, _aBitUp.position, _vel / 3);
				if (_cartuchos [_cont].transform.position == _aBitUp.position) {
					_activated = 2;
				}
				break;
			case 2:
				_cartuchos [_cont].transform.position = Vector3.MoveTowards (_cartuchos [_cont].transform.position, _insertedTarget.position, _vel / 3);
				if (_cartuchos [_cont].transform.position == _insertedTarget.position) {
					_checked = false;
					_activated = 3;
				}
				break;
			case 3:
				Particles ();
				break;
			}
		}
		for (int i = 0; i < _cartuchos.Length; i++) {
			if (i == _cont) {
				_cartSprite [i].sprite = _spriteLight [i];
			} else {
				_cartSprite [i].sprite = _spriteOff [i];
			}
		}
		_text.sprite = _textSprites [_cont];
	}
	private void Wait(){
		if (!_checked) {
			_activated = 1;
			_checked = true;
		}
	}
	private void Particles(){
		if (!_checked) {
			foreach (var part in _particles) {
				part.Play ();
			}
			Invoke ("Scene", 0.5f);
			_checked=true;
		}
	}
	private void Scene(){
			_scene.LoadScene (_cont+2);
	}
}