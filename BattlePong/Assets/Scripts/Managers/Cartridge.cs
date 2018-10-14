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
	[SerializeField] Sprite _cartridgeSprite;
	[SerializeField] GameObject _cartridge;
	[SerializeField] Transform _target;
	[SerializeField] float _speed;
	int _cont;
	Image _button;
	Vector3 _origPos;
	private bool _selected;

	void Awake () {
		_button = GetComponent<Image> ();
	}
	void Start(){
		_origPos = _cartridge.transform.position;
		_selected = false;
		_cont = 0;
	}
	void Update(){
		if (_selected) {
			switch (_cont) {
			case 0:
				_cartridge.transform.position = Vector3.MoveTowards (_cartridge.transform.position, _target.position, _speed * Time.deltaTime);
				if (_cartridge.transform.position==_target.position) {
					if (_cartridgeSprite != null) {
						_cartridge.GetComponent<SpriteRenderer> ().sprite = _cartridgeSprite;
					} else {
						_cartridge.GetComponent<SpriteRenderer> ().sprite = _base;
					}
					_cont++;
				}
				break;
			case 1:
				_cartridge.transform.position = Vector3.MoveTowards (_cartridge.transform.position, _origPos, _speed * Time.deltaTime);
				if (_cartridge.transform.position==_origPos) {
					_selected = false;
				}
				break;
			}
		}
	}
	public void OnPointerClick(PointerEventData eventData)
	{
		
	}
	public void OnPointerEnter(PointerEventData eventData)
	{
		_cont = 0;
		_button.color = Color.yellow;
		_selected = true;
	}
	public void OnPointerExit(PointerEventData eventData)
	{
		_button.color = Color.white;
	}
}
