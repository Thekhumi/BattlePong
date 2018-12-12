using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {
	private static MusicManager _instance;

	public static MusicManager Instance { get { return _instance; } }


	public enum Music { intro, selectStage, classic, bricks, flappy,warp, pinball,bubble}
	private Music _music;
	[SerializeField] AudioSource _audio;
	[SerializeField] AudioSource _sfx1;
	[SerializeField] AudioSource _sfx2;
	[SerializeField] AudioSource _sfx3;
	[Header("Music clips")]
	[SerializeField] AudioClip intro;
	[SerializeField] AudioClip selectStage;
	[SerializeField] AudioClip classic;
	[SerializeField] AudioClip bricks;
	[SerializeField] AudioClip flappy;
	[SerializeField] AudioClip warp;
	[SerializeField] AudioClip pinball;
	[SerializeField] AudioClip bubble;
	private float _sfxVolume = 1.0f;
	private float _musicVolume = 1.0f;

	private void Awake(){
		if (_instance != null && _instance != this){
			Destroy(this.gameObject);
		} else {
			_instance = this;
			DontDestroyOnLoad(this.gameObject);
		}
		_audio = GetComponent<AudioSource> ();
		updateVolume ();
	}
	public void playSound(AudioClip clip){
		if (!_sfx1.isPlaying) {
			_sfx1.clip = clip;
			_sfx1.Play ();
		} else if (!_sfx2.isPlaying) {
			_sfx2.clip = clip;
			_sfx2.Play ();
		} else if (!_sfx3.isPlaying) {
			_sfx3.clip = clip;
			_sfx3.Play ();
		} else {
			_sfx1.clip = clip;
			_sfx1.Play ();
		}
	}

	void updateAudio(){
		switch (_music) {
		case Music.intro:
			_audio.clip = intro;
			break;
		case Music.selectStage:
			_audio.clip = selectStage;
			break;
		case Music.classic:
			_audio.clip = classic;
			break;
		case Music.bricks:
			_audio.clip = bricks;
			break;
		case Music.flappy:
			_audio.clip = flappy;
			break;
		case Music.warp:
			_audio.clip = warp;
			break;
		case Music.pinball:
			_audio.clip = pinball;
			break;	
		case Music.bubble:
			_audio.clip = bubble;
			break;
		}
	}
	public void updateVolume(){
		_sfx1.volume = _sfxVolume;
		_sfx2.volume = _sfxVolume;
		_sfx3.volume = _sfxVolume;
		_audio.volume = _musicVolume;
	}

	public Music music{
		get{ return _music; }
		set{if(_music != value){
			_music = value;
			updateAudio ();
			_audio.Play ();
			}
		}
	}
	public float sfxVolume{
		get{ return _sfxVolume; }
		set{ _sfxVolume = value;}
	}

	public float musicVolume{
		get{ return _musicVolume; }
		set{ _musicVolume = value;}
	}
}
