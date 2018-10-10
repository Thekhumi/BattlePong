using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Cartridge : MonoBehaviour 
, IPointerClickHandler
, IPointerEnterHandler
, IPointerExitHandler{

	[SerializeField] Sprite _base;
	[SerializeField] Sprite _classic;
	[SerializeField] Sprite _arkanoid;
	[SerializeField] Sprite _pinball;
	[SerializeField] Sprite _flappy;
	[SerializeField] Sprite _warp;
	Image _button;
	[SerializeField] [Range(1,5)] int _buttonNum;
	//[SerializeField] RectTransform _cartridge;
	Vector3 _origPos;
	//[SerializeField] RectTransform _target;

	void Awake () {
		_button = GetComponent<Image> ();
	}
	void Start(){
		//_origPos = _cartridge.position;
	}
	public void OnPointerClick(PointerEventData eventData)
	{
		
	}
	void Update(){
		
	}
	public void OnPointerEnter(PointerEventData eventData)
	{
		_button.color = Color.yellow;
		/*switch (_buttonNum) {
		case 1:
			break;
		case 2:
			break;
		case 3:
			break;
		case 4:
			break;
		case 5:
			break;
		}*/
	}
	public void OnPointerExit(PointerEventData eventData)
	{
		_button.color = Color.white;
	}
}
