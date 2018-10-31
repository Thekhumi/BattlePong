using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour {

	[SerializeField] Text _version;
	[SerializeField] SceneChange _scene;
	void Start(){
		_version.text = "V " + Application.version;
	}
	void Update(){
		if (Input.GetButtonDown ("Submit")) {
			_scene.LoadScene (1);
		}
		if (Input.GetButtonDown ("Cancel")) {
			Application.Quit ();
		}
	}
}
