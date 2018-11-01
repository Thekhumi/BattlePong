using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinScreen : MonoBehaviour {

	[SerializeField] Text _yesText;
	[SerializeField] Text _selectText;
	SceneChange _scene;
	private int _selection;
	private Color _defaultColor;
	private Color _selectedColor;

	void Awake(){
		_scene = FindObjectOfType<SceneChange> ();
	}
	void Start(){
		_selection = 0;
		_defaultColor = Color.white;
		_selectedColor = Color.yellow;
	}

	void Update(){
		if (Input.GetButtonDown ("Left")) {
			_selection = 0;
			_yesText.color = _selectedColor;
			_selectText.color = _defaultColor;
		}
		if (Input.GetButtonDown ("Right")) {
			_selection = 1;
			_yesText.color = _defaultColor;
			_selectText.color = _selectedColor;
		}
		if (Input.GetButtonDown ("Submit")) {
			switch (_selection) {
			case 0:
				_scene.RestartScene ();
				break;
			case 1:
				_scene.LoadScene (1);
				break;
			}
		}
	}
}
