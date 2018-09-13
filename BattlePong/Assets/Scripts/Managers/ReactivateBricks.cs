using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactivateBricks : MonoBehaviour {

	private Ball _ball;
	private GameObject[] _bricks;

	void Start(){
		_ball = GameObject.FindGameObjectWithTag ("Ball").GetComponent<Ball>();
		_bricks = GameObject.FindGameObjectsWithTag ("BreakableWall");
	}
	void Update(){
		if (_ball.Scored) {
			for (int i = 0; i < _bricks.Length; i++) {
				_bricks [i].SetActive (true);
			}
		}
	}
}
