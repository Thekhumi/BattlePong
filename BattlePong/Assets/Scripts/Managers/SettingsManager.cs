using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour {
	[SerializeField] Slider _sfxSlider;
	[SerializeField] Slider _musicSlider;
	[SerializeField] Toggle _isFullscreen;
	[SerializeField] Dropdown _resolutionDropDown;	
	[SerializeField] AudioClip _testSound;
	private Resolution[] _resolutions;
	private bool _testOn;

	void Start(){
		_musicSlider.value = MusicManager.Instance.musicVolume;
		_sfxSlider.value = MusicManager.Instance.sfxVolume;
		_isFullscreen.isOn = Screen.fullScreen;
		_resolutions = Screen.resolutions;
		_resolutionDropDown.ClearOptions ();
		List<string> resOptions = new List<string> ();
		int currentRes = 0;
		for (int i = 0; i < _resolutions.Length; i++) {
			string option = _resolutions[i].width + " x " + _resolutions[i].height;
			resOptions.Add (option);
			if (_resolutions [i].width == Screen.currentResolution.width
			   && _resolutions [i].height == Screen.currentResolution.height) {
				currentRes = i;
			}
		}
		_resolutionDropDown.AddOptions (resOptions);
		_resolutionDropDown.value = currentRes;
		_resolutionDropDown.RefreshShownValue ();
		_testOn = true;
	}

	public void setResolution(int resolution){
		Resolution res = _resolutions [resolution];
		Screen.SetResolution (res.width, res.height,Screen.fullScreen);
	}

	public void setSfxVolume(float volume){
		MusicManager.Instance.sfxVolume = volume;
		MusicManager.Instance.updateVolume ();
		if (_testOn) {
			testSound ();
		}
	}

	public void testSound(){
		MusicManager.Instance.playSound (_testSound);
	}
	public void setMusicVolume(float volume){
		MusicManager.Instance.musicVolume = volume;
		MusicManager.Instance.updateVolume ();
	}

	public void setFullscreen(bool fullscreen){
		Screen.fullScreen = fullscreen;
	}
}
