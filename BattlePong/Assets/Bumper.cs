using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumper : MonoBehaviour {

	[SerializeField]bool isBump1;
	[SerializeField]float speed = 5f;
	private float _thisY;
	private float _thisZ;
	private bool _moving;

	void Start(){
		_thisY = transform.position.y;
		_thisZ = transform.position.z;
		_moving = false;
	}
	void Update () {
		if (_moving) {
			if (isBump1) {
				transform.Translate (0.0f, Input.GetAxis ("Vertical") * speed * Time.deltaTime, 0.0f);
			} else {
				transform.Translate (0.0f, Input.GetAxis ("Vertical2") * speed * Time.deltaTime, 0.0f);
			}
		}
	}
	public void ResetPos(){
		transform.position = new Vector3 (transform.position.x, _thisY, _thisZ);
	}
	public bool Move{
		get{ return _moving; }
		set{ _moving = value; }
	}
}
