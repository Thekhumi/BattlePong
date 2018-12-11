using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RivalAIPinball : MonoBehaviour {
	[SerializeField] float speed = 50f;
	Rigidbody2D _Flipper;
	private AudioSource _audio;
	bool _flipped;
	void Awake(){
		_Flipper = gameObject.GetComponent<Rigidbody2D> ();
		_flipped = false;
		_audio = GetComponent<AudioSource> ();
	}
	void OnTriggerStay2D(Collider2D otro){
		if (otro.gameObject.tag == "PinballBall") {
			_Flipper.AddTorque (speed,ForceMode2D.Impulse);
			if (!_flipped && !_audio.isPlaying) {
				_audio.volume = MusicManager.Instance.sfxVolume;
				_audio.Play ();
			}
		}
	}
}
