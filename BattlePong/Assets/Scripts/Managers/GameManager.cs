using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	public enum GameMode { Normal, Flappy, Pinball, Warp, Arkanoid, Bubble}
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
	[SerializeField] MusicManager.Music _music;
	[SerializeField] GameObject _1pCanvas;
	[SerializeField] GameObject _2pCanvas;
	FlashColor _flash;

	private Ball _ball;
	private PinballBall _pinballBall;
	[SerializeField]private Ball _multiBall1;
	[SerializeField]private Ball _multiBall2;

	[SerializeField]GameObject _secondPlayer1;
	[SerializeField]GameObject _secondPlayer2;

	[SerializeField]Ghost _ghost;

	private GameObject[] _bump;
	private Spring[] _springs;
	private Warp[] _warps;
	private GameObject[] _bricks;


	private int _ballCount;
	private SceneChange _sceneManager;
	private int _cameraState;
	private Camera _cam;
	private string _result;
	private bool _init;
	private float _timer;
	private GameObject [] _powerUps;

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
		_timer = 0;

	}
		
	void Start(){
		MusicManager.Instance.music = _music;
		if (_gameMode==GameMode.Pinball) {
			_pinballBall = GameObject.FindGameObjectWithTag ("PinballBall").GetComponent<PinballBall> ();
			Physics2D.gravity = new Vector2 (0f, 0f);
		}
		else{
			_ball = GameObject.FindGameObjectWithTag ("Ball").GetComponent<Ball> ();
			Physics2D.gravity = new Vector2 (0f, -9.8f);
		}
		if (_gameMode == GameMode.Arkanoid) {
			_bricks = GameObject.FindGameObjectsWithTag ("BreakableWall");
			ResetMultiBalls ();
		}
		_ballCount = 0;
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

		switch (_gameMode) {

		case GameMode.Normal:
			switch (PlayerManager.Instance.Players) {
			case PlayerManager.Player.ONEPLAYER:
				_secondPlayer1.GetComponent<Bumper> ().enabled = false;
				_secondPlayer1.GetComponent<RivalIA> ().enabled = true;
				_1pCanvas.SetActive (true);
				_2pCanvas.SetActive (false);
				break;
			case PlayerManager.Player.TWOPLAYERS:
				_secondPlayer1.GetComponent<Bumper> ().enabled = true;
				_secondPlayer1.GetComponent<RivalIA> ().enabled = false;
				_1pCanvas.SetActive (false);
				_2pCanvas.SetActive (true);
				break;
			}
			break;

		case GameMode.Warp:
			switch (PlayerManager.Instance.Players) {
			case PlayerManager.Player.ONEPLAYER:
				_secondPlayer1.GetComponent<Bumper> ().enabled = false;
				_secondPlayer1.GetComponent<RivalIA> ().enabled = true;
				_1pCanvas.SetActive (true);
				_2pCanvas.SetActive (false);
				break;
			case PlayerManager.Player.TWOPLAYERS:
				_secondPlayer1.GetComponent<Bumper> ().enabled = true;
				_secondPlayer1.GetComponent<RivalIA> ().enabled = false;
				_1pCanvas.SetActive (false);
				_2pCanvas.SetActive (true);
				break;
			}
			break;

		case GameMode.Bubble:
			switch (PlayerManager.Instance.Players) {
		case PlayerManager.Player.ONEPLAYER:
			_secondPlayer1.GetComponent<Bumper> ().enabled = false;
			_secondPlayer1.GetComponent<RivalIA> ().enabled = true;
			_1pCanvas.SetActive (true);
			_2pCanvas.SetActive (false);
			break;
		case PlayerManager.Player.TWOPLAYERS:
			_secondPlayer1.GetComponent<Bumper> ().enabled = true;
			_secondPlayer1.GetComponent<RivalIA> ().enabled = false;
			_1pCanvas.SetActive (false);
			_2pCanvas.SetActive (true);
			break;
		}
		break;

		case GameMode.Arkanoid:
			switch (PlayerManager.Instance.Players) {
			case PlayerManager.Player.ONEPLAYER:
				_secondPlayer1.GetComponent<Bumper> ().enabled = false;
				_secondPlayer1.GetComponent<RivalAIArkanoid> ().enabled = true;
				_1pCanvas.SetActive (true);
				_2pCanvas.SetActive (false);
				break;
			case PlayerManager.Player.TWOPLAYERS:
				_secondPlayer1.GetComponent<Bumper> ().enabled = true;
				_secondPlayer1.GetComponent<RivalAIArkanoid> ().enabled = false;
				_1pCanvas.SetActive (false);
				_2pCanvas.SetActive (true);
				break;
			}
			break;

		case GameMode.Flappy:
			switch (PlayerManager.Instance.Players) {
			case PlayerManager.Player.ONEPLAYER:
				_secondPlayer1.GetComponent<Bumper> ().enabled = false;
				_secondPlayer1.GetComponent<RivalAIFlappy> ().enabled = true;
				_1pCanvas.SetActive (true);
				_2pCanvas.SetActive (false);
				break;
			case PlayerManager.Player.TWOPLAYERS:
				_secondPlayer1.GetComponent<Bumper> ().enabled = true;
				_secondPlayer1.GetComponent<RivalAIFlappy> ().enabled = false;
				_1pCanvas.SetActive (false);
				_2pCanvas.SetActive (true);
				break;
			}
			break;

		case GameMode.Pinball:
			switch (PlayerManager.Instance.Players) {
			case PlayerManager.Player.ONEPLAYER:
				_secondPlayer1.GetComponent<Bumper> ().enabled = false;
				_secondPlayer1.GetComponent<RivalAIPinball> ().enabled = true;
				_secondPlayer2.GetComponent<Bumper> ().enabled = false;
				_secondPlayer2.GetComponent<RivalAIPinball> ().enabled = true;
				_1pCanvas.SetActive (true);
				_2pCanvas.SetActive (false);
				break;
			case PlayerManager.Player.TWOPLAYERS:
				_secondPlayer1.GetComponent<Bumper> ().enabled = true;
				_secondPlayer1.GetComponent<RivalAIPinball> ().enabled = false;
				_secondPlayer1.GetComponent<BoxCollider2D> ().enabled = false;
				_secondPlayer2.GetComponent<Bumper> ().enabled = true;
				_secondPlayer2.GetComponent<RivalAIPinball> ().enabled = false;
				_secondPlayer2.GetComponent<BoxCollider2D> ().enabled = false;
				_1pCanvas.SetActive (false);
				_2pCanvas.SetActive (true);
				break;
			}
			break;
		}
	}

	void Update(){
		if (_gameMode == GameMode.Bubble) {
			_timer += Time.deltaTime;
			if (_timer >= 20) {
				if (!_ghost.Active) {
					_ghost.Active= true;
				}
			}
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
		if (_gameMode == GameMode.Arkanoid) {
			ResetMultiBalls ();
			_ballCount = 0;
			ResetBricks ();
		}
		if (_gameMode == GameMode.Bubble) {
			_ghost.Reset ();
			_timer = -5;
		}
		ScoreUpdate ();
	}

	public void SetWinnerRight(){
		_winnerRight = true;
		_scoreP1++;
		if (_cameraState != (_cameraScreens.Length - 1)) {
			_cameraState += 1;
		}
		if (_gameMode == GameMode.Arkanoid) {
			ResetMultiBalls ();
			_ballCount = 0;
			ResetBricks ();
		}
		if (_gameMode == GameMode.Bubble) {
			_ghost.Reset ();
			_timer = -5;
		}
		ScoreUpdate ();
	}

	public void SetResultLeft(){
		_winScreen.SetActive (true);
		_result = "BLUE";
		_textResult.text = _result + " WINS!";
		BallEnd ();
		if (_gameMode == GameMode.Arkanoid) {
			ResetMultiBalls ();
			_ballCount = 0;
			ResetBricks ();
		}
		if (_gameMode == GameMode.Bubble) {
			_ghost.Reset ();
			_timer = -1000;
		}
		_flash.SetColorBlue ();
	}

	public void SetResultRight(){
		_winScreen.SetActive (true);
		_result = "RED";
		_textResult.text = _result + " WINS!";
		BallEnd ();
		if (_gameMode == GameMode.Arkanoid) {
			ResetMultiBalls ();
			_ballCount = 0;
			ResetBricks ();
		}
		if (_gameMode == GameMode.Bubble) {
			_ghost.Reset ();
			_timer = -1000;
		}
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

	private void ResetMultiBalls(){
		_multiBall1.MultiStop ();
		_multiBall2.MultiStop ();
	}
	public void ActivateMulti(){
		switch (_ballCount) {
		case 0:
			_multiBall1.MultiActive (_ball.gameObject);
			_ballCount++;
			break;
		case 1:
			_multiBall2.MultiActive (_ball.gameObject);
			_ballCount++;
			break;
		case 2:
			break;
		}
	}
	private void ResetBricks(){
		foreach (var brick in _bricks) {
			brick.gameObject.SetActive (true);
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
		if (_gameMode == GameMode.Warp) {
			foreach (var warp in _warps) {
				warp.DeactivateDelay ();
			}
		} else if (_gameMode == GameMode.Arkanoid) {
		
			_powerUps = GameObject.FindGameObjectsWithTag ("PowerUp");
			if (_powerUps != null ) {
				foreach (var powerUp in _powerUps) {
					Destroy (powerUp.gameObject);
				}
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
