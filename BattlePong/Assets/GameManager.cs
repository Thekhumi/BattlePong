using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	private bool _winnerLeft = false;
	private bool _winnerRight = false;
	[Tooltip("From Left to Right")]
	[SerializeField] GameObject[] _cameraScreens;
	[Tooltip("From Left to Right")]
	[SerializeField] GameObject[] _levelScreens;
	[SerializeField] GameObject _levelElements;
	[SerializeField] GameObject _ball;
	[SerializeField] Camera _mainCamera;
	[SerializeField] private float cameraSpeed;
	private int _cameraState;
	void Start(){
		_cameraState = _cameraScreens.Length / 2;
	}
	void Update(){
		if (_winnerLeft) {
			CameraMov ();
			BallStart ();
			_winnerRight = false; _winnerLeft = false;
		}
		if (_winnerRight) {
			CameraMov ();
			BallStart ();
			_winnerRight = false; _winnerLeft = false;
		}
	}
	public void SetWinnerLeft(){
		_winnerLeft = true;
		if (_cameraState != 0) {
			_cameraState -= 1;
		}
	}
	public void SetWinnerRight(){
		_winnerRight = true;
		if (_cameraState != (_cameraScreens.Length - 1)) {
			_cameraState += 1;
		}
	}
	private void BallReset(){
		_ball.GetComponent<Ball> ().Reset ();
	}
	private void BallStart(){
		_ball.GetComponent<Ball> ().Stop ();
		_ball.GetComponent<Ball> ().ResetPosition ();
		Invoke ("BallReset", 0.5f);
	}
	private void CameraMov(){
		_mainCamera.transform.position = _cameraScreens [_cameraState].transform.position;
		_levelElements.transform.position = _levelScreens [_cameraState].transform.position;
	}
}
