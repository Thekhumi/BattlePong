using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour {
	[SerializeField]private float _speed;
	[SerializeField]private bool _yAxis;
	void Update () {
		if (!_yAxis) {
			transform.Rotate (0, 0, _speed * Time.deltaTime);
		}
		if(_yAxis){
			transform.Rotate (0,_speed * Time.deltaTime,0 );
		}
	}
}
