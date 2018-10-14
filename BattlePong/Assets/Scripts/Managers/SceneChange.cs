using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour {
	[SerializeField] private GameObject _loadingScreen;

	public void LoadScene(int sceneNum){
		StartCoroutine (LoadAsynchronously (sceneNum));
	}

	IEnumerator LoadAsynchronously (int sceneNum){
		AsyncOperation operation = SceneManager.LoadSceneAsync (sceneNum);
		if (_loadingScreen != null) {
			_loadingScreen.SetActive (true);
		}
		while (!operation.isDone) {
			yield return null;
		}
	}
}
