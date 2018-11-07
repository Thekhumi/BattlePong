using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {

	[SerializeField] float _speed;
	protected enum PowerUpBox{
		Laser = 0,
		Expand = 1,
		Charge = 2
	}
	protected Color _color; 
	protected PowerUpBox _power;
	void Start () {
		_power = (PowerUpBox)Random.Range(0,3);
		switch (_power) {
		case PowerUpBox.Laser:
			_color = Color.red;
			break;
		case PowerUpBox.Expand:
			_color = Color.green;
			break;
		case PowerUpBox.Charge:
			_color = Color.yellow;
			break;
		}
		UpdateColor ();
	}

	void Update(){
		transform.Translate(_speed * Time.deltaTime, 0.0f, 0.0f);
	}

	void OnTriggerEnter2D(Collider2D otro){
		if (otro.gameObject.tag == "Bumper") {
			switch (_power) {
			case PowerUpBox.Laser:
				//addLaser
				break;
			case PowerUpBox.Expand:
				//addExpand
				break;
			case PowerUpBox.Charge:
				//addCharge
				break;
			}
			Destroy (gameObject);
		}
	}

	protected void UpdateColor(){
		GetComponent<SpriteRenderer> ().color = _color;
	}

	public float Speed{
		get{ return _speed; }
		set{ _speed = value; }
	}
}
