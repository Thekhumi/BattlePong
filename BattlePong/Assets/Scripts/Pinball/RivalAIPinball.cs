using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RivalAIPinball : MonoBehaviour {
	[SerializeField] float speed = 50f;
	Rigidbody2D _Flipper;
	void Start(){
		_Flipper = gameObject.GetComponent<Rigidbody2D> ();
	}
	void OnTriggerStay2D(Collider2D otro){
		if (otro.gameObject.tag == "PinballBall") {
			_Flipper.AddTorque (speed,ForceMode2D.Impulse);
		}
	}
}
