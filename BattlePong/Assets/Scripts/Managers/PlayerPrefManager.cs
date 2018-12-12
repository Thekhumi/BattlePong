using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefManager : MonoBehaviour {
	private static PlayerPrefManager _instance;
	public static PlayerPrefManager Instance {get { return _instance;}}

	private bool _started = false;
	private void Awake(){
		if (_instance != null && _instance != this) {
			Destroy (this.gameObject);
		} else {
			_instance = this;
			DontDestroyOnLoad (this.gameObject);
		}
	}
	private void Start(){
		if(!_started){
			if (PlayerPrefs.HasKey ("pSfxVolume")) {
				MusicManager.Instance.sfxVolume = PlayerPrefs.GetFloat ("pSfxVolume");
			}
			if (PlayerPrefs.HasKey ("pMusicVolume")) {
				MusicManager.Instance.musicVolume = PlayerPrefs.GetFloat ("pMusicVolume");
			}
			if (PlayerPrefs.HasKey ("pFullscreen")) {
				Screen.fullScreen = getBool ("pFullscreen");
			}
			_started = true;
		}
		MusicManager.Instance.updateVolume ();
	}
	public static void setBool(string key,bool state){
		PlayerPrefs.SetInt (key, state ? 1 : 0);
	}

	public static bool getBool(string key){
		int value = PlayerPrefs.GetInt (key);
		if (value == 1) {
			return true;
		} else {
			return false;
		}
	}
}
