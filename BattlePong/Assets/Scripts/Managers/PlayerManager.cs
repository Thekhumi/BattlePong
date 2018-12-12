using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

	private static PlayerManager _instance;

	public static PlayerManager Instance {get { return _instance;}}

	public enum Player { ONEPLAYER, TWOPLAYERS}
	public enum Diff {EASY,NORMAL,HARD}
	[SerializeField] private Player _player;
	[SerializeField] private Diff _difficulty;
	[SerializeField] private bool _skipIntro = false;

	void Awake(){
		if (_instance != null && _instance != this) {
			Destroy (this.gameObject);
		} else {
			_instance = this;
			DontDestroyOnLoad (this.gameObject);
		}
	}

	public void OnePlayer(){
		_player = Player.ONEPLAYER;
	}
	public void TwoPlayers(){
		_player = Player.TWOPLAYERS;
	}

	public void setDifficulty(Diff difficulty){
		_difficulty = difficulty;
	}
	public Player Players{
		get{return _player;}
	}

	public bool skipIntro{
		get{return _skipIntro;}
		set{_skipIntro = value;}
	}
	public Diff Difficulty{
		get{return _difficulty;}
		set{_difficulty = value; }
	}
}
