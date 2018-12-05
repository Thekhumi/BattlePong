using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

	private static PlayerManager _instance;

	public static PlayerManager Instance {get { return _instance;}}

	public enum Player { ONEPLAYER, TWOPLAYERS}
	[SerializeField] private Player _player;

	void Awake(){
		if (_instance != null && _instance != this) {
			Destroy (this.gameObject);
		} else {
			_instance = this;
			DontDestroyOnLoad (this.gameObject);
		}
		_player = Player.ONEPLAYER;
	}

	public void OnePlayer(){
		_player = Player.ONEPLAYER;
	}
	public void TwoPlayers(){
		_player = Player.TWOPLAYERS;
	}
	public Player Players{
		get{return _player;}
	}
}
