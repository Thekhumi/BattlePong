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
				updatePermanentBoost (otro);
				break;
			case false:
				if (gameObject.tag == "END GAME") {
					_manager.SetResultRight ();
				}
				_manager.SetWinnerRight ();
				updatePermanentBoost (otro);
				break;
			}
		}
	}

	void updatePermanentBoost(Collider2D otro){
		if (_manager.getGameMode () == GameManager.GameMode.Normal) {
			Ball ball = otro.GetComponent<Ball> ();
			ball .Scored = true;
			ball.permanentBoost += ball.permanentBoostSum;
			if (ball.permanentBoost > ball.permanentBoostMax) {
				ball.permanentBoost = ball.permanentBoostMax;
			}
		}
	}
}
