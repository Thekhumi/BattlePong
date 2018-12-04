using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {
	public enum Music { intro, selectStage, classic, bricks, flappy,warp}
	private Music _music;
	private AudioSource _audio;
	[SerializeField] AudioClip intro;
	[SerializeField] AudioClip selectStage;
	[SerializeField] AudioClip classic;
	[SerializeField] AudioClip bricks;
	[SerializeField] AudioClip flappy;
	[SerializeField] AudioClip warp;

	void Start() {
		_audio = GetComponent<AudioSource> ();
		_music = Music.intro;
		updateAudio ();
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
		}
	}

	public Music music{
		get{ return _music; }
		set{ _music = value; 
			updateAudio ();}
	}

	
}
