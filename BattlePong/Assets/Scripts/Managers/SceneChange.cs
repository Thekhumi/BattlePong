using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChange : MonoBehaviour {
	[SerializeField] private GameObject _loadingScreen;
	[SerializeField] private float _delayText;
	[SerializeField] private Text _loadingText;
	[SerializeField] private float _startDelay;
	[SerializeField] private bool _justBack;
	private int _change;
	private int _sceneNum;

	void Start(){
		_change = 0;
	}
	void Update(){
		if (Input.GetButtonDown ("Cancel")&& SceneManager.GetActiveScene ().buildIndex!=0) {
			LoadScene (0);
		}
		if (_justBack) {
			if (Input.GetButtonDown ("Submit")) {
				LoadScene (0);
			}
		}
	}
	public void LoadScene(int sceneNum){
		_sceneNum = sceneNum;
		Invoke ("StartCR", _startDelay);
	}
	public void RestartScene(){
		_sceneNum = SceneManager.GetActiveScene ().buildIndex;
		Invoke ("StartCR", _startDelay);
	}

	private void StartCR(){
		StartCoroutine (LoadAsynchronously (_sceneNum));
	}
	IEnumerator LoadAsynchronously (int sceneNum){
		AsyncOperation operation = SceneManager.LoadSceneAsync (sceneNum);
		if (_loadingScreen != null) {
			_loadingScreen.SetActive (true);
			Invoke ("TextC", _delayText);
		}
		while (!operation.isDone) {
			yield return null;
		}
	}
	private void TextC(){
		switch (_change) {
		case 0:
			_loadingText.text = "Loading";
			_change++;
			break;
		case 1:
			_loadingText.text = "Loading.";
			_change++;
			break;
		case 2:
			_loadingText.text = "Loading..";
			_change++;
			break;
		case 3:
			_loadingText.text = "Loading...";
			_change = 0;
			break;
		}
		Invoke ("TextC", _delayText);
	}
}
