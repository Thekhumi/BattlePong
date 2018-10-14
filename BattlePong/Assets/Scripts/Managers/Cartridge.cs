using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Cartridge : MonoBehaviour 
, IPointerClickHandler
, IPointerEnterHandler
, IPointerExitHandler{

	[SerializeField] Sprite _cartridgeSprite;
	[SerializeField] MainMenuManager _cManager;
	[SerializeField] [Range(1,5)] int _buttonNum;
	Image _button;
	private bool _selected;

	void Awake () {
		_button = GetComponent<Image> ();
		_selected = false;
	}
	void Update(){
		if (_selected) {
			_selected=_cManager.MovingCartridge (_cartridgeSprite, _selected,_buttonNum);
		}
	}
	public void OnPointerClick(PointerEventData eventData)
	{
		_cManager.Clicked ();
	}
	public void OnPointerEnter(PointerEventData eventData)
	{
		_button.color = Color.yellow;
		_selected = true;
	}
	public void OnPointerExit(PointerEventData eventData)
	{
		_button.color = Color.white;
	}
}
