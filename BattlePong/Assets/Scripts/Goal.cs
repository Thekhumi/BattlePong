using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour {
	[SerializeField] bool _isLeft;
	private GameManager _manager;
	void Start () {
		_manager = GameObject.FindGameObjectWithTag ("GAMEMANAGER").GetComponent<GameManager> ();
	}

	void OnTriggerEnter2D(Collider2D otro){
		if (otro.gameObject.tag=="Ball"||otro.gameObject.tag=="PinballBall") {
			switch (_isLeft) {
			case true:
				if (gameObject.tag == "END GAME") {
					_manager.SetResultLeft ();
				}
				_manager.SetWinnerLeft ();
				if (_manager.getGameMode() == GameManager.GameMode.Normal)
				otro.GetComponent<Ball> ().Scored = true;
				break;
			case false:
				if (gameObject.tag == "END GAME") {
					_manager.SetResultRight ();
				}
				_manager.SetWinnerRight ();
				if (_manager.getGameMode() == GameManager.GameMode.Normal)
				otro.GetComponent<Ball> ().Scored = true;
				break;
			}
		}
	}
}
