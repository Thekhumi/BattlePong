using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuBack : MonoBehaviour, IPointerEnterHandler {

	[SerializeField] Image _back; 
	[SerializeField] Sprite _classic;
	[SerializeField] Sprite _arkanoid;
	[SerializeField] Sprite _pinball;
	[SerializeField] Sprite _flappy;
	[SerializeField] Sprite _warp;
	[SerializeField] private char _Button; 
	private Vector2 _originalSize;
	private RectTransform _rect;

	void Awake(){
		_rect = _back.gameObject.GetComponent <RectTransform> ();
		_originalSize = _rect.sizeDelta;
	}
	public void OnPointerEnter(PointerEventData eventData){
		switch (_Button) {
		case 'C':
			_back.sprite = _classic;
			_rect.sizeDelta = _originalSize;
			break;
		case 'A':
			_back.sprite = _arkanoid;
			_rect.sizeDelta = _originalSize;
			break;
		case 'P':
			_back.sprite = _pinball;
			_rect.sizeDelta = _originalSize;
			break;
		case 'F':
			_back.sprite = _flappy;
			_rect.sizeDelta = _originalSize;
			break;
		case 'W':
			_back.sprite = _warp;
			_rect.sizeDelta = _originalSize;
			break;
		}
	}
}
