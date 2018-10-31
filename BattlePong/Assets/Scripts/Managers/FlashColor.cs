using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashColor : MonoBehaviour {
	// Use this for initialization
	[SerializeField] float _flashSpeed;
	private Text _textResult;
	Color _defaultColor;
	Color _flashColor;
	private int _state;

	void Awake () {
		_textResult = GetComponent<Text> ();
		_flashColor = Color.white;
		_defaultColor = Color.yellow;
		_textResult.color = _defaultColor;
		_state = 0;
		StartCoroutine ("Flash");
	}

	IEnumerator Flash(){
		while (true) {
			switch (_state) {
			case 0:
				_textResult.color = _defaultColor;
				_state = 1;
				yield return new WaitForSeconds (_flashSpeed);
				break;
			case 1:
				_textResult.color = _flashColor;
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
}
