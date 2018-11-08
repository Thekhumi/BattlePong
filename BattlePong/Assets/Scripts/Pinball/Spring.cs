using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour {
	[SerializeField] private float _delay;
	[SerializeField] Sprite _green;
	[SerializeField] Sprite _red;
	[SerializeField] float _impulse;
	private bool _active;
	private Rigidbody2D _body;

	void Start(){
		_active = true;
	}
	void OnCollisionEnter2D(Collision2D otro){
		if (_active) {
			if (otro.gameObject.tag == "PinballBall") {
				_body=otro.gameObject.GetComponent<Rigidbody2D> ();
				_body.AddForce (transform.up*_impulse,ForceMode2D.Impulse);
				Invoke ("Deactivate",_delay);
			}
		}
	}
	private void Deactivate(){
		_active = false;
		gameObject.GetComponent<SpriteRenderer> ().sprite = _red;
		gameObject.GetComponent<SpriteRenderer> ().color -= new Color (0.0f, 0.0f, 0.0f, 0.7f);
		gameObject.GetComponent<BoxCollider2D> ().enabled = false;
	}
	public void Reactivate(){
		_active = true;
		gameObject.GetComponent<SpriteRenderer> ().sprite = _green;
		gameObject.GetComponent<SpriteRenderer> ().color += new Color (0.0f, 0.0f, 0.0f, 0.7f);
		gameObject.GetComponent<BoxCollider2D> ().enabled = true;
	}
}
