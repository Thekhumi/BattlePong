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
	//[SerializeField] Bumper _bump1;
	//[SerializeField] Bumper _bump2;
	[SerializeField] bool _pinballMode;
	[SerializeField] CameraMov _mainCamera;
	[SerializeField] private float _cameraSpeed;
	[SerializeField] private float _ballReset;
	[SerializeField] Text _textScoreP1;
	[SerializeField] Text _textScoreP2;
	[SerializeField] Text _textResult;
	[SerializeField] FlashColor flash;
	[SerializeField] float _menuDelay;
	private Ball _ball;
	private GameObject[] _bump;
	private PinballBall _pinballBall;
	private SceneChange _sceneManager;
	private int _cameraState;
	private Camera _cam;
	private string _result;
	void Start(){
		switch (_pinballMode) {
		case true:
			_pinballBall = GameObject.FindGameObjectWithTag ("PinballBall").GetComponent<PinballBall> ();
			Physics2D.gravity = new Vector2 (0f, 0f);
			break;
		case false:
			_ball = GameObject.FindGameObjectWithTag ("Ball").GetComponent<Ball> ();
			break;
		}
		_cam = Camera.main;
		_cameraState = _cameraScreens.Length / 2;
		_scoreP1 = 0;
		_scoreP2 = 0;
		_sceneManager = gameObject.GetComponent<SceneChange> ();
		_bump = GameObject.FindGameObjectsWithTag ("Bumper");
		ScoreUpdate();
	}
	void Update(){
		if (Input.GetButtonDown ("Cancel")) {
			MainMenu ();
		}
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
			_bump[0].GetComponent<Bumper>().Move = true;
			_bump[1].GetComponent<Bumper>().Move = true;
			if (_pinballMode) {
				_bump[2].GetComponent<Bumper>().Move = true;
				_bump[3].GetComponent<Bumper>().Move = true;
			}
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
		Invoke ("MainMenu", _menuDelay);
	}
	public void SetResultRight(){
		_result = "RED";
		_textResult.text = _result + " WINS!";
		flash.SetColorRed ();
		_textResult.gameObject.SetActive (true);
		Invoke ("MainMenu", _menuDelay);
	}
	private void ScoreUpdate(){	
		_textScoreP1.text = _scoreP1.ToString ();
		_textScoreP2.text = _scoreP2.ToString ();
	}
	private void BallReset(){
		switch (_pinballMode) {
		case true:
			_pinballBall.Reset ();
			break;
		case false:
			_ball.Reset ();
			break;
		}
	}
	private void BallStart(){
		switch (_pinballMode) {
		case true:
			_pinballBall.ResetPosition ();
			_pinballBall.Stop ();
			Invoke ("BallReset", _ballReset);
			break;
		case false:
			_ball.Stop ();
			_ball.ResetPosition ();
			Invoke ("BallReset", _ballReset);
			break;
		}
	}
	private void CameraMov(){
		_levelElements.transform.position = _levelScreens [_cameraState].transform.position;
		_bump[0].GetComponent<Bumper>().Move = false;
		_bump[1].GetComponent<Bumper>().Move = false;
		if (_pinballMode) {
			_bump[2].GetComponent<Bumper>().Move = false;
			_bump[3].GetComponent<Bumper>().Move = false;
		}
		if (!_pinballMode) {
			_bump [0].GetComponent<Bumper> ().ResetPos ();
			_bump [1].GetComponent<Bumper> ().ResetPos ();
		}
	}
	private void MainMenu(){
		_sceneManager.MainMenu ();
	}
}
