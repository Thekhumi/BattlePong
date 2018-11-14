using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Expand : MonoBehaviour {

	[SerializeField] float _scaleX = 1f;
	[SerializeField] float _scaleY = 1.5f;
	[SerializeField] float _scaleZ = 1f;
	[SerializeField] bool _active;
	float _originalScaleX;
	float _originalScaleY;
	float _originalScaleZ;
	void Start () {
		_originalScaleX = transform.localScale.x;
		_originalScaleY = transform.localScale.y;
		_originalScaleZ = transform.localScale.z;
		ExpandBumper ();
	}

	public void ExpandBumper(){
		// MULTIPLICA, NO NUMERO FIJO
		if (_active) {
			transform.localScale = new Vector3 (_originalScaleX * _scaleX,
											_originalScaleY * _scaleY,
											_originalScaleZ * _scaleZ);
		} 
		else {
			transform.localScale = new Vector3 (_originalScaleX, _originalScaleY, _originalScaleZ);
		}
	}

	public bool Active{
		get{ return _active; }
		set{ _active = value; }
	}
}
