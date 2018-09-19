using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinballBall : MonoBehaviour {
	[SerializeField] float _magnitude = 5f;
	[SerializeField] GameManager _manager;
	private bool _scored = false;

	Rigidbody2D body;

	void Start () {
		body = GetComponent<Rigidbody2D>();
		float sx = Random.Range (0, 2) == 0 ? -1 : 1;
		body.AddForce ((Vector2.right * sx) * _magnitude);
	}
	void OnCollisionEnter2D(Collision2D otro){
		if (otro.gameObject.tag == "END GAME") {
			switch (otro.gameObject.layer) {
			case 9:
				_manager.SetResultRight ();
				break;
			case 10:
				_manager.SetResultLeft ();
				break;
			}
		}
		if (otro.gameObject.tag == "BreakableWall") {
			otro.gameObject.SetActive (false);
		}
		else{
			switch (otro.gameObject.layer) {
			case 9:
				_manager.SetWinnerRight ();
				_scored = true;
				break;
			case 10:
				_manager.SetWinnerLeft ();
				_scored = true;
				break;
			}
		}
	}
	public void Stop(){
		body.velocity = Vector2.zero;
	}
	public void Reset(){
		float sx = Random.Range (0, 2) == 0 ? -1 : 1;
		body.AddForce ((transform.forward * sx) * _magnitude);
		_scored = false;
	}
	public void ResetPosition(){
		transform.position = transform.parent.position+new Vector3(0.0f,1.0f,0.0f);
	}
	public bool Scored{
		get{return _scored;}
		set{_scored=value;}
	}
}
