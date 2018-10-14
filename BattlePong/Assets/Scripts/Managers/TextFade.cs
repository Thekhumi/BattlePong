using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextFade : MonoBehaviour {

	[SerializeField]private bool _activeFade;
	private Text _text;
	private Color _fade;

	void Awake(){
		_text = gameObject.GetComponent<Text> ();
	}
	void Start(){
		_activeFade = false;
		_fade = new Color (0.0f, 0.0f, 0.0f, 0.1f);
	}
	void Update(){
		if (_activeFade) {
			FadeIn ();
			if (_text.color.a >= 1.0f) {
				Invoke ("Deactivate", 1.0f);
			} 
		}else {
			FadeOut ();
		}
	}
	void FadeIn(){
		if (_text.color.a <= 1) {
			_text.color += _fade;
		}
	}
	void FadeOut(){
		if (_text.color.a >= 0.3f) {
			_text.color -= _fade;
		}
	}
	void Deactivate(){
		_activeFade = false;
	}
	public bool ActivateFade{
		set{_activeFade = value;}
	}
}
