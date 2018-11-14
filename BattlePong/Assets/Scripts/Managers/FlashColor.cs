using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashColor : MonoBehaviour {
	// Use this for initialization
	[SerializeField] float _flashSpeed;
	[SerializeField] private bool _isSprite;
	private Text _textResult;
	private SpriteRenderer _sprite;
	Color _defaultColor;
	Color _flashColor;
	private int _state;

	void Awake () {
		_flashColor = Color.white;
		_defaultColor = Color.yellow;
		if (_isSprite) {
			_sprite = GetComponent<SpriteRenderer> ();
			_sprite.color = _defaultColor;
		} else {
			_textResult = GetComponent<Text> ();
			_textResult.color = _defaultColor;
		}
		_state = 0;
		StartCoroutine ("Flash");
	}

	IEnumerator Flash(){
		while (true) {
			switch (_state) {
			case 0:
				if (_isSprite) {
					_sprite.color = _defaultColor;
				} else {
					_textResult.color = _defaultColor;
				}
				_state = 1;
				yield return new WaitForSeconds (_flashSpeed);
				break;
			case 1:
				if (_isSprite) {
					_sprite.color = _flashColor;
				} else {
					_textResult.color = _flashColor;
				}
				_state = 0;
				yield return new WaitForSeconds (_flashSpeed);
				break;
			}
		}
	}

	public void SetColorRed(){
		_flashColor = Color.red;
	}
	public void SetColorBlue(){
		_flashColor = Color.blue;
	}
	public void SetColorBlack(){
		_flashColor = Color.black;
		_flashSpeed = 0.1f;
	}
}
