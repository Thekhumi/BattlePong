﻿using System.Collections;
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
	[SerializeField] Text _settings;
	[SerializeField] Text _exit;
	[SerializeField] GameObject _quit;
	[SerializeField] Text _yesQuit;
	[SerializeField] Text _noQuit;
	[SerializeField] MusicManager.Music _music;
	[SerializeField] AudioSource _sfxEnter;
	[SerializeField] AudioSource _sfxMove;

	private bool _startPress;
	private int _menuSelect;
	private bool _active;
	private bool _quitSelect;
	private bool _quitActive;
	private bool _selected;
	private float _timer;
	private float _startMenuDelay = 1.5f;

	void Start(){
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.None;
		_version.text = "V " + Application.version;
		_startPress = false;
		_menuSelect = 0;
		_active = false;
		_quitSelect = false;
		_quitActive = false;
		MusicManager.Instance.music = _music;
		_selected = false;
		_sfxEnter.volume = _sfxMove.volume = MusicManager.Instance.musicVolume;

		if (PlayerManager.Instance.skipIntro) {
			_startPress = true;
			_startMenuDelay = 0.0f;
		}
	}
	void Update(){
		
		if (Input.GetButtonDown ("Cancel")) {
			_quitActive = true;
			_quit.SetActive(true);
			_sfxEnter.Play ();
		}

		if (_quitActive) {
			
			switch (_quitSelect) {
			case false:
				_noQuit.color = Color.yellow;
				_yesQuit.color = Color.white;
				if (Input.GetButtonDown("Submit")) {
					_quit.SetActive (false);
					Invoke ("Quit", 0.3f);
					_sfxEnter.Play ();
				}
				break;
			case true:
				_noQuit.color = Color.white;
				_yesQuit.color = Color.yellow;
				if (Input.GetButtonDown("Submit")) {
					_sfxEnter.Play ();
					Application.Quit ();
				}
				break;
			}
			if (Input.GetButtonDown ("Left")||Input.GetButtonDown ("Right")) {
				_sfxMove.Play ();
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
					_sfxEnter.Play ();
					_startPress = true;
					PlayerManager.Instance.skipIntro = true;
				}
			}
			if (_startPress) {
				_pressStart.GetComponent<FlashColor> ().SetColorBlack ();
				Invoke ("StartMenu", _startMenuDelay);
				if (!_selected) {
					if (Input.GetButtonDown ("Up")) {
						_sfxMove.Play ();
						if (_menuSelect == 1 || _menuSelect == 0) {
							_menuSelect = 5;
						} else {
							_menuSelect--;
						}
					}
					if (Input.GetButtonDown ("Down")) {
						_sfxMove.Play ();
						if (_menuSelect == 5) {
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
						_settings.color = Color.white;
						_exit.color = Color.white;
						if (Input.GetButtonDown ("Submit")) {
							_sfxEnter.Play ();
							PlayerManager.Instance.OnePlayer ();
							_selected = true;
							_scene.LoadScene (1);
						}
						break;
					case 2:
						_1p.color = Color.white;
						_2p.color = Color.yellow;
						_credits.color = Color.white;
						_settings.color = Color.white;
						_exit.color = Color.white;
						if (Input.GetButtonDown ("Submit")) {
							_sfxEnter.Play ();
							PlayerManager.Instance.TwoPlayers ();
							_selected = true;
							_scene.LoadScene (1);
						}
						break;
					case 3:
						_1p.color = Color.white;
						_2p.color = Color.white;
						_credits.color = Color.yellow;
						_settings.color = Color.white;
						_exit.color = Color.white;
						if (Input.GetButtonDown ("Submit")) {
							_sfxEnter.Play ();
							_selected = true;
							_scene.LoadScene (8);
						}
						break;
					case 4:
						_1p.color = Color.white;
						_2p.color = Color.white;
						_credits.color = Color.white;
						_settings.color = Color.yellow;
						_exit.color = Color.white;
						if (Input.GetButtonDown ("Submit")) {
							_sfxEnter.Play ();
							_selected = true;
							_scene.LoadScene (9);
						}
						break;
					case 5:
						_1p.color = Color.white;
						_2p.color = Color.white;
						_credits.color = Color.white;
						_settings.color = Color.white;
						_exit.color = Color.yellow;
						if (Input.GetButtonDown ("Submit")) {
							_sfxEnter.Play ();
							_quitActive = true;
							_quit.SetActive (true);
						}
						break;
					}
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
