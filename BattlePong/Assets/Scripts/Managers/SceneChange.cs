using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour {

	public void MainMenu(){
		SceneManager.LoadScene ("Menu");
	}
	public void Classic(){
		SceneManager.LoadScene ("Classic");
	}
	public void Arkanoid(){
		SceneManager.LoadScene ("Arkanoid");
	}
	public void Pinball(){
		SceneManager.LoadScene ("Pinball");
	}

	public void Flappy(){
		SceneManager.LoadScene ("Flappy");
	}
}
