using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMov : MonoBehaviour {

	public void Move(Transform target, float speed){
		transform.position = Vector3.MoveTowards (transform.position, target.position, speed * Time.deltaTime);
	}
}
