using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour {

	[SerializeField] Sprite _base;
	[SerializeField] GameObject _cartridge;
	[SerializeField] Transform _target;
	[SerializeField] float _speed;
	int _cont;
	int _selected;
	int _lastSelected;
	Image _button;
	Vector3 _origPos;
	Vector3 _inserted;

	void Awake () {
		_button = GetComponent<Image> ();
	}
	void Start(){
		_origPos = _cartridge.transform.position;
		_cont = 0;
		_selected = _lastSelected = 0;
		_inserted = _cartridge.transform.position - (new Vector3 (0.0f, 0.9f, 0.0f));
	}

	public bool MovingCartridge(Sprite cartSprite, bool selected, int select){
		_selected = select;
		if(_lastSelected!=_selected)
		switch (_cont) {
			case 0:
				_lastSelected = select;
				_cartridge.transform.position = Vector3.MoveTowards (_cartridge.transform.position, _target.position, _speed * Time.deltaTime);
				if (_cartridge.transform.position == _target.position) {
					if (cartSprite != null) {
						_cartridge.GetComponent<SpriteRenderer> ().sprite = cartSprite;
					} else {
						_cartridge.GetComponent<SpriteRenderer> ().sprite = _base;
					}
					_cont++;
				}
				return true;
			break;
		case 1:
			_cartridge.transform.position = Vector3.MoveTowards (_cartridge.transform.position, _origPos, _speed * Time.deltaTime);
			if (_cartridge.transform.position==_origPos) {
				_cont = 0;
				return false;
			}
			break;
		}
		return true;
	}
	public void Clicked(){
		_cartridge.transform.position = Vector3.MoveTowards (_cartridge.transform.position, _inserted , _speed * Time.deltaTime);
	}
}
