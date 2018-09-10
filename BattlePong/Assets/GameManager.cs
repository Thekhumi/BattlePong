using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	private bool _winnerLeft = false;
	private bool _winnerRight = false;
	private int _scoreP1;
	private int _scoreP2;
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
	[SerializeField] Text _textScoreP1;
	[SerializeField] Text _textScoreP2;
	[SerializeField] Text _textResult;
	[SerializeField] FlashColor flash;
	private int _cameraState;
	private Camera _cam;
	private string _result;
	void Start(){
		_cam = Camera.main;
		_cameraState = _cameraScreens.Length / 2;
		_scoreP1 = 0;
		_scoreP2 = 0;
		ScoreUpdate();
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
		_scoreP2++;
		ScoreUpdate ();
		if (_cameraState != 0) {
			_cameraState -= 1;
		}
	}
	public void SetWinnerRight(){
		_winnerRight = true;
		_scoreP1++;
		ScoreUpdate ();
		if (_cameraState != (_cameraScreens.Length - 1)) {
			_cameraState += 1;
		}
	}
	public void SetResultLeft(){
		_result = "BLUE";
		_textResult.text = _result + " WINS!";
		flash.SetColorBlue ();
		_textResult.gameObject.SetActive (true);
	}
	public void SetResultRight(){
		_result = "RED";
		_textResult.text = _result + " WINS!";
		flash.SetColorRed ();
		_textResult.gameObject.SetActive (true);
	}
	private void ScoreUpdate(){	
		_textScoreP1.text = _scoreP1.ToString ();
		_textScoreP2.text = _scoreP2.ToString ();
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
