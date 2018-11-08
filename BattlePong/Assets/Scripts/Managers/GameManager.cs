using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	public enum GameMode { Normal, Flappy, Pinball, Warp,}
	[SerializeField] private GameManager.GameMode _gameMode = GameManager.GameMode.Normal;
	public static GameManager instance = null;
	private bool _winnerLeft = false;
	private bool _winnerRight = false;
	private int _scoreP1;
	private int _scoreP2;
	[Tooltip("From Left to Right")]
	[SerializeField] GameObject[] _cameraScreens;
	[Tooltip("From Left to Right")]
	[SerializeField] GameObject[] _levelScreens;
	[SerializeField] GameObject _levelElements;
	[SerializeField] CameraMov _mainCamera;
	[SerializeField] private float _cameraSpeed;
	[SerializeField] private float _ballReset;
	[SerializeField] Text _textScore;
	[SerializeField] Text _textResult;
	[SerializeField] float _menuDelay;
	[SerializeField] GameObject _winScreen;
	[SerializeField] private bool _3ScreensGame;
	FlashColor _flash;
	private Ball _ball;
	private GameObject[] _bump;
	private Spring[] _springs;
	private Warp[] _warps;
	private PinballBall _pinballBall;
	private SceneChange _sceneManager;
	private int _cameraState;
	private Camera _cam;
	private string _result;
	private bool _init;
	void Awake(){
		if (instance == null){
			instance = this;
		}
			else if(instance != this){
			Destroy (gameObject);
		}
		_scoreP1 = 0;
		_scoreP2 = 0;
		ScoreUpdate();
		if (_gameMode == GameMode.Warp) {
			_warps = FindObjectsOfType<Warp> ();
		}
		if (_gameMode == GameMode.Pinball) {
			_springs = FindObjectsOfType<Spring> ();
		}
		_flash = _textResult.GetComponent<FlashColor> ();
	}
		
	void Start(){
		if (_gameMode==GameMode.Pinball) {
			_pinballBall = GameObject.FindGameObjectWithTag ("PinballBall").GetComponent<PinballBall> ();
			Physics2D.gravity = new Vector2 (0f, 0f);
		}
		else{
			_ball = GameObject.FindGameObjectWithTag ("Ball").GetComponent<Ball> ();
			Physics2D.gravity = new Vector2 (0f, -9.8f);
		}
		_init = true;
		BallStart ();
		_cam = Camera.main;
		_cameraState = _cameraScreens.Length / 2;
		_bump = GameObject.FindGameObjectsWithTag ("Bumper");
		_sceneManager = gameObject.GetComponent<SceneChange> ();
		if (!_3ScreensGame) {
			_textScore.text = "- - O - -";
		} else {
			_textScore.text = "-  O  -";
		}
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
			foreach (GameObject bumper in _bump) {
				bumper.GetComponent<Bumper>().Move = true;
			}
		}
	}

	public void SetWinnerLeft(){
		_winnerLeft = true;
		_scoreP2++;
		if (_cameraState != 0) {
			_cameraState -= 1;
		}
		ScoreUpdate ();
	}

	public void SetWinnerRight(){
		_winnerRight = true;
		_scoreP1++;
		if (_cameraState != (_cameraScreens.Length - 1)) {
			_cameraState += 1;
		}
		ScoreUpdate ();
	}

	public void SetResultLeft(){
		_winScreen.SetActive (true);
		_result = "BLUE";
		_textResult.text = _result + " WINS!";
		BallEnd ();
		_flash.SetColorBlue ();
	}

	public void SetResultRight(){
		_winScreen.SetActive (true);
		_result = "RED";
		_textResult.text = _result + " WINS!";
		BallEnd ();
		_flash.SetColorRed ();
	}

	private void ScoreUpdate(){	
		_textScore.GetComponent<TextFade> ().ActivateFade=true;
		switch (_cameraState) {
		case 0:
			if (!_3ScreensGame) {
				_textScore.text = "O  -  -  -  -";
			}else{
				_textScore.text = "O    -    -";
			}
			break;
		case 1:
			if (!_3ScreensGame) {
				_textScore.text = "-  O  -  -  -";
			}else{
				_textScore.text = "-    O    -";
			}
			break;
		case 2:
			if (!_3ScreensGame) {
				_textScore.text = "-  -  O  -  -";
			}else{
				_textScore.text = "-    -    O";
			}
			break;
		case 3:
			_textScore.text = "-  -  -  O  -";
			break;
		case 4:
			_textScore.text = "-  -  -  -  O";
			break;
		}
	}

	private void BallReset(){
		if (_gameMode == GameMode.Pinball) {
			_pinballBall.Reset ();
		} else {
			_ball.Reset ();
		}
	}
	private void BallEnd(){
		if (_gameMode == GameMode.Pinball) {
			_pinballBall.ResetPosition ();
			_pinballBall.Stop ();
		}
		else{
			_ball.Stop ();
			_ball.ResetPosition ();
		}
	}

	private void BallStart(){
			if (_gameMode == GameMode.Pinball) {
				_pinballBall.ResetPosition ();
				_pinballBall.Stop ();
			} else {
				_ball.Stop ();
				_ball.ResetPosition ();
			}
			if (_init) {
				Invoke ("BallReset", _ballReset * 2);
				_init = false;
			} else {
				Invoke ("BallReset", _ballReset);
			}
	}

	private void CameraMov(){
		_levelElements.transform.position = _levelScreens [_cameraState].transform.position;
		foreach (GameObject bumper in _bump) {
			bumper.GetComponent<Bumper>().Move = false;
		}
		if(_gameMode == GameMode.Pinball) {
			foreach (GameObject bumper in _bump) {
				bumper.GetComponent<Bumper> ().ResetAngle ();
			}
			foreach (var spring in _springs) {
				spring.Reactivate ();
			}
		}else{
			foreach (GameObject bumper in _bump) {
				bumper.GetComponent<Bumper> ().ResetPos ();
			}
		}
		if (_gameMode==GameMode.Warp) {
			foreach (var warp in _warps) {
				warp.DeactivateDelay();
			}
		}
	}

	private void MainMenu(){
		_sceneManager.LoadScene(0);
	}
	public GameMode getGameMode(){
		return _gameMode;
	}
	public void setGameMode(GameMode gameMode){
		_gameMode = gameMode;
	}
}
