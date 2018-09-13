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
	void Update () {
		if (_moving) {
			if (isBumpLeftUp) {
				if (Input.GetButtonDown("W")) {
					_Flipper.AddTorque (speed * Time.deltaTime);
				}
			}
			if (isBumpLeftDown) {
				if (Input.GetButtonDown("S")) {
					_Flipper.AddTorque (speed * Time.deltaTime);
				}
			}
			if (isBumpRightUp) {
				if (Input.GetButtonDown("Up")) {
					_Flipper.AddTorque (speed * Time.deltaTime);
				}
			}
			if (isBumpRightDown) {
				if (Input.GetButtonDown("Down")) {
					_Flipper.AddTorque (speed * Time.deltaTime);
				}
			}
		}
	}
		
	public void ResetPos(){
		transform.rotation = _initialRot;
	}
	public bool Move{
		get{ return _moving; }
		set{ _moving = value; }
	}
}
