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
	[SerializeField] Text _exit;
	[SerializeField] GameObject _quit;
	[SerializeField] Text _yesQuit;
	[SerializeField] Text _noQuit;
	[SerializeField] MusicManager.Music _music;

	private bool _startPress;
	private int _menuSelect;
	private bool _active;
	private bool _quitSelect;
	private bool _quitActive;

	void Start(){
		Cursor.visible = false;
		_version.text = "V " + Application.version;
		_startPress = false;
		_menuSelect = 0;
		_active = false;
		_quitSelect = false;
		_quitActive = false;
		MusicManager.Instance.music = _music;
	}
	void Update(){
		
		if (Input.GetButtonDown ("Cancel")) {
			_quitActive = true;
			_quit.SetActive(true);
		}

		if (_quitActive) {
			
			switch (_quitSelect) {
			case false:
				_noQuit.color = Color.yellow;
				_yesQuit.color = Color.white;
				if (Input.GetButtonDown("Submit")) {
					_quit.SetActive (false);
					Invoke ("Quit", 0.3f);
				}
				break;
			case true:
				_noQuit.color = Color.white;
				_yesQuit.color = Color.yellow;
				if (Input.GetButtonDown("Submit")) {
					Application.Quit ();
				}
				break;
			}
			if (Input.GetButtonDown ("Left")||Input.GetButtonDown ("Right")) {
				if (_quitSelect) {
					_quitSelect = false;
				} else {
					_quitSelect = true;
				}
			}
		}
		if (!_quitActive) {
			if (Input.GetButtonDown ("Submit")) {
				if (!_startPress) {
					_startPress = true;
				}
			}
			if (_startPress) {
				_pressStart.GetComponent<FlashColor> ().SetColorBlack ();
				Invoke ("StartMenu", 1.5f);
				if (Input.GetButtonDown ("Up")) {
					if (_menuSelect == 1 || _menuSelect == 0) {
						_menuSelect = 4;
					} else {
						_menuSelect--;
					}
				}
				if (Input.GetButtonDown ("Down")) {
					if (_menuSelect == 4) {
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
					_exit.color = Color.white;
					if (Input.GetButtonDown ("Submit")) {
						_scene.LoadScene (8);
					}
					break;
				case 2:
					_1p.color = Color.white;
					_2p.color = Color.yellow;
					_credits.color = Color.white;
					_exit.color = Color.white;
					if (Input.GetButtonDown ("Submit")) {
						_scene.LoadScene (1);
					}
					break;
				case 3:
					_1p.color = Color.white;
					_2p.color = Color.white;
					_credits.color = Color.yellow;
					_exit.color = Color.white;
					if (Input.GetButtonDown ("Submit")) {
						_scene.LoadScene (7);
					}
					break;
				case 4:
					_1p.color = Color.white;
					_2p.color = Color.white;
					_credits.color = Color.white;
					_exit.color = Color.yellow;
					if (Input.GetButtonDown ("Submit")) {
						_quitActive = true;
						_quit.SetActive(true);
					}
					break;
				}
			}
		}
	}
	private void StartMenu(){
		if (!_active) {
			_pressStart.SetActive (false);
			_menuButtons.SetActive (true);
			_menuSelect = 1;
			_active = true;
		}
	}
	private void Quit(){
		_quitActive = false;
	}
}
