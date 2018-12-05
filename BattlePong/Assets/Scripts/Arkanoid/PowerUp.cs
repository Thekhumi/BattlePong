using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {

	[SerializeField] float _speed;
	private GameManager _gamemanager;

	protected enum PowerUpBox{
		Laser = 0,
		Expand = 1,
		Multi = 2
	}
	protected Color _color; 
	protected PowerUpBox _power;
	void Awake(){
		_gamemanager = FindObjectOfType<GameManager> ();
	}
	void Start () {
		GetComponent<Rigidbody2D> ().useFullKinematicContacts = true;
		_power = (PowerUpBox)Random.Range(0,3);
		switch (_power) {
		case PowerUpBox.Laser:
			_color = Color.red;
			break;
		case PowerUpBox.Expand:
			_color = Color.green;
			break;
		case PowerUpBox.Multi:
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
			Bumper bumper = otro.gameObject.GetComponent<Bumper> ();
			switch (_power) {
			case PowerUpBox.Laser:
				bumper.laserActive = true;
				bumper.expandActive = false;
				bumper.GetComponent<LaserPower> ().Reload ();
				break;
			case PowerUpBox.Expand:
				bumper.laserActive = false;
				bumper.expandActive = true;
				bumper.rechargeExpand ();
				break;
			case PowerUpBox.Multi:
				_gamemanager.ActivateMulti ();
				break;
			}
			bumper.updatePowerups ();
			Destroy (gameObject);
		}
		if (otro.gameObject.layer == 9 || otro.gameObject.layer == 10) {
			Destroy(gameObject);
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
