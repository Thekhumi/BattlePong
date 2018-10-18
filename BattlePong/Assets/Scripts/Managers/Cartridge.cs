using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Cartridge : MonoBehaviour {
	[SerializeField] SpriteRenderer[] _cartuchos;
	[SerializeField] Sprite[] _spriteLight;
	[SerializeField] Sprite[] _spriteOff;
	[SerializeField] SpriteRenderer _text;
	[SerializeField] Sprite[] _textSprites;
	[SerializeField] SceneChange _scene;
	private int _cont;

	void Start(){
		_cont = 0;
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
			_scene.LoadScene (_cont+2);
		}
		for (int i = 0; i < _cartuchos.Length; i++) {
			if (i == _cont) {
				_cartuchos [i].sprite = _spriteLight [i];
			} else {
				_cartuchos [i].sprite = _spriteOff [i];
			}
		}

		Debug.Log (_textSprites.Length);
		_text.sprite = _textSprites [_cont];
	}
}