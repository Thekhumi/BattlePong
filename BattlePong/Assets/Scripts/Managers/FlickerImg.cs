using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlickerImg : MonoBehaviour {

	[SerializeField] Sprite _normal;
	[SerializeField] Sprite _pressed;
	[SerializeField] private float _delay;
	[SerializeField] private bool _firstFlicker;
	private bool _press;
	private Image _img;
	void Awake(){
		_img = gameObject.GetComponent<Image> ();
	}
	void Start () {
		switch (_firstFlicker) {
		case true:
			Invoke ("Flicker", _delay*2);
		break;
		case false:
			Invoke ("Flicker", _delay);
			break;
		}
	}

	private void Flicker(){
		switch (_press) {
		case true:
			_img.sprite = _normal;
			_press = false;
			break;
		case false:
			_img.sprite = _pressed;
			_press = true;
			break;
		}
		Invoke ("Flicker", _delay);
	}
}
