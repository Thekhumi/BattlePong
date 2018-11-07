using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour {

	[SerializeField] Text _version;
	[SerializeField] SceneChange _scene;
	[SerializeField] GameObject _menuButtons;
	[SerializeField] GameObject _pressStart;
	[SerializeField] Text _1p;
	[SerializeField] Text _2p;
	[SerializeField] Text _credits;
	private bool _startPress;
	private int _menuSelect;

	void Start(){
		Cursor.visible = false;
		_version.text = "V " + Application.version;
		_startPress = false;
		_menuSelect = 0;
	}
	void Update(){
		if (Input.GetButtonDown ("Submit")) {
			if (!_startPress) {
				_startPress = true;
			}
		}
		if (Input.GetButtonDown ("Cancel")) {
			Application.Quit ();
		}
		if (_startPress) {
			_pressStart.SetActive (false);
			_menuButtons.SetActive (true);
			if (Input.GetButtonDown ("Up")) {
				if (_menuSelect == 1||_menuSelect == 0) {
					_menuSelect = 3;
				} else{
					_menuSelect--;
				}
			}
			if (Input.GetButtonDown ("Down")) {
				if (_menuSelect == 3) {
					_menuSelect = 1;
				} else {
					_menuSelect++;
				}
			}

			switch (_menuSelect) {
			case 1:
				_1p.color = Color.yellow;
				_2p.color = Color.white;
				_credits.color = Color.white;
				if (Input.GetButtonDown ("Submit")) {
					_scene.LoadScene(8);
				}
				break;
			case 2:
				_1p.color = Color.white;
				_2p.color = Color.yellow;
				_credits.color = Color.white;
				if (Input.GetButtonDown ("Submit")) {
					_scene.LoadScene (1);
				}
				break;
			case 3:
				_1p.color = Color.white;
				_2p.color = Color.white;
				_credits.color = Color.yellow;
				if (Input.GetButtonDown ("Submit")) {
					_scene.LoadScene (7);
				}
				break;
			}
		}
	}
}
