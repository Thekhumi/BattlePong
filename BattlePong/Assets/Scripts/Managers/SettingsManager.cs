using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour {
	[SerializeField] Slider _sfxSlider;
	[SerializeField] Slider _musicSlider;
	[SerializeField] Toggle _isFullscreen;
	[SerializeField] Dropdown _resolutionDropDown;	
	[SerializeField] AudioClip _testSound;
	private Resolution[] _resolutions;
	private bool _turnOnSliders;

	void Awake(){
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		_musicSlider.value = MusicManager.Instance.musicVolume;
		_sfxSlider.value = MusicManager.Instance.sfxVolume;
		_isFullscreen.isOn = Screen.fullScreen;
		updateResolutions ();
		_turnOnSliders = true;
	}
	public void updateResolutions(){
		_resolutions = Screen.resolutions.Select (resolution => new Resolution {
			width = resolution.width,
			height = resolution.height
		}).Distinct ().ToArray ();
		_resolutionDropDown.ClearOptions ();
		List<string> resOptions = new List<string> ();
		int currentRes = 0;
		for (int i = 0; i < _resolutions.Length; i++) {
			string option = _resolutions[i].width + " x " + _resolutions[i].height;
			if (_resolutions [i].width == Screen.width
			    && _resolutions [i].height == Screen.height) {
				currentRes = i;
			}
			resOptions.Add (option);
		}

		_resolutionDropDown.AddOptions (resOptions);
		_resolutionDropDown.value = currentRes;
		Debug.Log (Screen.currentResolution.width + " " + Screen.currentResolution.height);
		_resolutionDropDown.RefreshShownValue ();
	}

	public void setResolution(int resolution){
		if (_turnOnSliders) {
			Resolution res = _resolutions [resolution];
			Screen.SetResolution (res.width, res.height, Screen.fullScreen);
		}
	}

	public void setSfxVolume(float volume){
		MusicManager.Instance.sfxVolume = volume;
		MusicManager.Instance.updateVolume ();
		PlayerPrefs.SetFloat ("pSfxVolume", volume);
		if (_turnOnSliders) {
			testSound ();
		}
	}

	public void testSound(){
		MusicManager.Instance.playSound (_testSound);
	}
	public void setMusicVolume(float volume){
		MusicManager.Instance.musicVolume = volume;
		MusicManager.Instance.updateVolume ();
		PlayerPrefs.SetFloat ("pMusicVolume", volume);
	}

	public void setFullscreen(bool fullscreen){
		Screen.fullScreen = fullscreen;
		PlayerPrefManager.setBool ("pFullscreen", fullscreen);
	}
}
