using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartFlicker : MonoBehaviour {

	[SerializeField] Sprite _tres;
	[SerializeField] Sprite _dos;
	[SerializeField] Sprite _uno;
	[SerializeField] Sprite _start;
	[SerializeField] private float _delay;
	[SerializeField] GameObject _canvas;
	private Image _img;
	private RectTransform _rect;
	private int _cont;

	void Awake(){
		_img = gameObject.GetComponent<Image> ();	
		_rect = gameObject.GetComponent<RectTransform> ();
	}
	void Start () {
		_cont = 0;
		Invoke ("Change",_delay);
	}
	private void Change(){
		switch (_cont) {
		case 0:
			_img.sprite = _dos;
			_cont++;
			break;
		case 1:
			_img.sprite = _uno;
			_cont++;
			break;
		case 2:
			_rect.sizeDelta= new Vector2(_rect.sizeDelta.x*3,_rect.sizeDelta.y);
			_img.sprite = _start;
			_cont++;
			break;
		case 3:
			_canvas.SetActive(false);
			break;
		}
		Invoke ("Change",_delay);
	}
}
