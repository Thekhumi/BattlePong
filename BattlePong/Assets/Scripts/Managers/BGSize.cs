using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGSize : MonoBehaviour {
	[SerializeField] private float _size;
	private Vector2 _lastSize;
	private RectTransform _rect;
	void Awake(){
		_rect = gameObject.GetComponent <RectTransform> ();
	}
	void Update(){
		_rect.sizeDelta = _rect.sizeDelta* (new Vector2(_size*Time.deltaTime,_size*Time.deltaTime));
	}
}
