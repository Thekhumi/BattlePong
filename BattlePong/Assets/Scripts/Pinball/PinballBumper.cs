using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinballBumper : MonoBehaviour {
	[SerializeField]bool isBumpLeftUp;
	[SerializeField]bool isBumpLeftDown;
	[SerializeField]bool isBumpRightUp;
	[SerializeField]bool isBumpRightDown;
	[SerializeField]float speed = 5f;
	private Quaternion _initialRot;
	private bool _moving=true;
	Rigidbody2D _Flipper;

	Camera cam;
	void Start(){
		_initialRot = transform.rotation;
		_Flipper = gameObject.GetComponent<Rigidbody2D> ();
	}
	void FixedUpdate () {
		//if (_moving) {
			if (isBumpLeftUp) {
				if (Input.GetButton("W")) {
					_Flipper.AddTorque (speed);
				}
			}
			if (isBumpLeftDown) {
				if (Input.GetButton("S")) {
					_Flipper.AddTorque (speed);
				}
			}
			if (isBumpRightUp) {
				if (Input.GetButton("Up")) {
					_Flipper.AddTorque (speed);
				}
			}
			if (isBumpRightDown) {
				if (Input.GetButton("Down")) {
					_Flipper.AddTorque (speed);
				}
			}
		//}
	}
		
	public void ResetPos(){
		transform.rotation = _initialRot;
	}
	public bool Move{
		get{ return _moving; }
		set{ _moving = value; }
	}
}
