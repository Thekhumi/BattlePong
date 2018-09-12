using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionColor : MonoBehaviour {

	[SerializeField] private Sprite _normal;
	[SerializeField] private Sprite _light;
	[SerializeField] private float _lightDelay=0.1f;
	private SpriteRenderer _renderer;
	void Start () {
		_renderer = gameObject.GetComponent<SpriteRenderer>();
	}
	void OnCollisionEnter2D (Collision2D otro){
		_renderer.sprite = _light;
		Invoke ("ChangeBack", _lightDelay);
	}
	//void OnCollisionExit2D (Collision2D otro){
	//	_renderer.sprite = _normal;
	//}
	private void ChangeBack(){
		_renderer.sprite = _normal;
	}
}
