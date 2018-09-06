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
	[SerializeField] Bumper _bump1;
	[SerializeField] Bumper _bump2;
	[SerializeField] Ball _ball;
	[SerializeField] CameraMov _mainCamera;
	[SerializeField] private float _cameraSpeed;
	[SerializeField] private float _ballReset;
	private int _cameraState;
	private Camera _cam;
	void Start(){
		_cam = Camera.main;
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
		_mainCamera.Move(_cameraScreens[_cameraState].transform,_cameraSpeed);
		if (_cam.transform.position == _cameraScreens [_cameraState].transform.position) {
			_bump1.Move = true;
			_bump2.Move = true;
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
		_ball.Reset ();
	}
	private void BallStart(){
		_ball.Stop ();
		_ball.ResetPosition ();
		Invoke ("BallReset", _ballReset);
	}
	private void CameraMov(){
		_levelElements.transform.position = _levelScreens [_cameraState].transform.position;
		_bump1.Move = false;
		_bump2.Move = false;
		_bump1.ResetPos ();
		_bump2.ResetPos ();
	}
}
