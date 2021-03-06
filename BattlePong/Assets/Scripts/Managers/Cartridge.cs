﻿using System.Collections;
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
	[SerializeField] GameObject[] _arrows;
	[SerializeField] AudioClip _clipMove;
	[SerializeField] AudioClip _clipSelect;
	[SerializeField] Text _difficultyText;
	[SerializeField] Text _zKey;
	private PlayerManager.Diff _diff;
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
		_diff = PlayerManager.Instance.Difficulty;
		updateDiffText ();
		if (PlayerManager.Instance.Players == PlayerManager.Player.ONEPLAYER) {
			_difficultyText.enabled = true;
			_zKey.enabled = true;
		}
	}
	void Start(){
		_cont = 0;
		_activated = 0;
		_press = false;
		_checked = false;
	}
	void Update(){
		if (!_press) {
			Movement ();
		}
		if (Input.GetButtonDown ("Left")||Input.GetButtonDown ("Down")) {
			if (_cont != 0) {
				_cont--;
			} else {_cont = 5;}
			MusicManager.Instance.playSound (_clipMove);
		}
		if (Input.GetButtonDown ("Right")||Input.GetButtonDown ("Up")) {
			if (_cont != 5) {
				_cont++;
			} else {_cont = 0;}
			MusicManager.Instance.playSound (_clipMove);
		}
		if (Input.GetButtonDown ("Submit")) {
			foreach (var arrow in _arrows) {
				arrow.SetActive(false);
			}
			_press = true;
			MusicManager.Instance.playSound (_clipSelect);
		}
		if (PlayerManager.Instance.Players == PlayerManager.Player.ONEPLAYER && Input.GetButtonDown ("Submit2")) {
			if ((int)_diff == 2) {
				_diff = 0;
			} else {
				_diff++;
			}
			PlayerManager.Instance.Difficulty = _diff;
			updateDiffText ();
		}
		if (_press) {
			switch (_activated) {
			case 0:
				for (int i = 0; i < _cartuchos.Length; i++) {
					if (i == _cont) {
						_cartuchos [i].transform.position = Vector3.MoveTowards (_cartuchos [i].transform.position, _mainTarget.position, _vel/3);
						_cartSprite [i].sortingOrder = 1;
					} else {
						_cartuchos [i].transform.position = Vector3.MoveTowards (_cartuchos [i].transform.position, new Vector3(_cartuchos [i].transform.position.x,_cartuchos [i].transform.position.y-50,_cartuchos [i].transform.position.z) , _vel);
						if (_cartuchos [_cont].transform.position == _mainTarget.position) {
							Invoke ("Wait", _waitTime);
						}
					}
				}
				break;
			case 1:
				_cartuchos [_cont].transform.position = Vector3.MoveTowards (_cartuchos [_cont].transform.position, _aBitUp.position, _vel / 5);
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
	public void updateDiffText(){
		switch (_diff) {
		case PlayerManager.Diff.EASY:
			_difficultyText.color = Color.green;
			_difficultyText.text = "EASY";
			break;
		case PlayerManager.Diff.NORMAL:
			_difficultyText.color = Color.white;
			_difficultyText.text = "NORMAL";
			break;
		case PlayerManager.Diff.HARD:
			_difficultyText.color = Color.red;
			_difficultyText.text = "HARD";
			break;
		}
	}
	public int Cont{
		get{return _cont;}
	}
	private void Movement(){
		if (_cont - 1 < 0) {
			_cartuchos [4].transform.position = Vector3.MoveTowards (_cartuchos [4].transform.position, _target [3].position, _vel*3);
			_cartuchos [5].transform.position = Vector3.MoveTowards (_cartuchos [5].transform.position, _target [4].position, _vel*3);
		} else {
			if (_cont - 2 < 0) {
				_cartuchos [5].transform.position = Vector3.MoveTowards (_cartuchos [4].transform.position, _target [3].position, _vel*3);
				_cartuchos [_cont - 1].transform.position = Vector3.MoveTowards (_cartuchos [_cont - 1].transform.position, _target [4].position, _vel*3);
			} else {
				_cartuchos [_cont - 2].transform.position = Vector3.MoveTowards (_cartuchos [_cont - 2].transform.position, _target [3].position, _vel*3);
				_cartuchos [_cont - 1].transform.position = Vector3.MoveTowards (_cartuchos [_cont - 1].transform.position, _target [4].position, _vel*3);
			}
		}

		_cartuchos [_cont].transform.position = Vector3.MoveTowards (_cartuchos [_cont].transform.position, _target [0].position, _vel*3);

		if (_cont + 1 > 5) {
			_cartuchos [0].transform.position = Vector3.MoveTowards (_cartuchos [0].transform.position, _target [1].position, _vel*3);
			_cartuchos [1].transform.position = Vector3.MoveTowards (_cartuchos [1].transform.position, _target [2].position, _vel*3);
		} else {
			if (_cont + 2 > 5) {
				_cartuchos [_cont + 1].transform.position = Vector3.MoveTowards (_cartuchos [_cont + 1].transform.position, _target [1].position, _vel*3);
				_cartuchos [0].transform.position = Vector3.MoveTowards (_cartuchos [0].transform.position, _target [2].position, _vel*3);
			} else {
				_cartuchos [_cont + 1].transform.position = Vector3.MoveTowards (_cartuchos [_cont + 1].transform.position, _target [1].position, _vel*3);
				_cartuchos [_cont + 2].transform.position = Vector3.MoveTowards (_cartuchos [_cont + 2].transform.position, _target [2].position, _vel*3);
			}
		}
	}
}