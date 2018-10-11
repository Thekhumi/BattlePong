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
	[SerializeField] Sprite _thisCartridge;
	[SerializeField] GameObject _cartridge;
	Image _button;
	Vector3 _origPos;

	void Awake () {
		_button = GetComponent<Image> ();
	}
	void Start(){
		_origPos = _cartridge.transform.position;
	}
	public void OnPointerClick(PointerEventData eventData)
	{
		
	}
	void Update(){
		
	}
	public bool OnPointerEnter(PointerEventData eventData)
	{
		_button.color = Color.yellow;
		return true;
	}
	public void OnPointerExit(PointerEventData eventData)
	{
		_button.color = Color.white;
	}
}
