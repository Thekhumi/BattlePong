using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumper : MonoBehaviour {
	private delegate void UpdateDelegate();
	private UpdateDelegate currentUpdate;
	[SerializeField] bool isBump1;
	[SerializeField] float speed = 5f;
	[SerializeField] GameObject _particleObject;
	[SerializeField] ParticleSystem _spark;
	Rigidbody2D _rb;
	float _thisY;
	float _thisZ;
	bool _moving;
	GameObject Wall;
	SpriteRenderer sprite;
	SpriteRenderer WallSprite;
	Camera cam;
	AudioSource audioSrc;

	//ARKANOID STATS
	[SerializeField] int _expandMaxLives = 6;
	[SerializeField] float _stunTime = 2f;
	int _expandLives = 0;
	float _stunTimer = 0.0f;
	//POWERUPS
	private bool _laserActive;
	private bool _expandActive;
	private bool _chargeActive;

	[SerializeField]bool isBumpLeftUp;
	[SerializeField]bool isBumpLeftDown;
	[SerializeField]bool isBumpRightUp;
	[SerializeField]bool isBumpRightDown;
	Rigidbody2D _Flipper;
	Vector3 _originalRot;

	void Awake(){
		_rb = GetComponent<Rigidbody2D> ();
		cam = Camera.main;
		sprite = GetComponent<SpriteRenderer> ();
		_thisY = transform.position.y;
		_thisZ = transform.position.z;
		_originalRot = transform.eulerAngles;
		_moving = false;
		audioSrc = GetComponent<AudioSource> ();
	}

	void Start(){
		switch (GameManager.instance.getGameMode()) {
		case GameManager.GameMode.Normal:
			currentUpdate = UpdateNormal;
			break;
		case GameManager.GameMode.Arkanoid:
			currentUpdate = UpdateNormal;
			break;
		case GameManager.GameMode.Warp:
			currentUpdate = UpdateNormal;
			break;
		case GameManager.GameMode.Flappy:
			currentUpdate = UpdateFlappy;
			break;
		case GameManager.GameMode.Pinball:
			_Flipper = gameObject.GetComponent<Rigidbody2D> ();
			currentUpdate = UpdatePinball;
			break;
		case GameManager.GameMode.Bubble:
			currentUpdate = UpdateNormal;
			break;
		}
		Wall = GameObject.FindGameObjectsWithTag ("BoundWall")[0];
		WallSprite = Wall.GetComponent<SpriteRenderer> ();

	}
	void OnTriggerEnter2D(Collider2D otro){
		if (otro.gameObject.tag == "Ball" || otro.gameObject.tag == "MultiBall") {
			if (_expandActive) {
				_expandLives--;
				if (_expandLives <= 0) {
					_expandActive = false;
					updatePowerups ();
				}
			}
			if (!audioSrc.isPlaying) {
				audioSrc.Play ();
			}
			if (_particleObject && _particleObject.GetComponent<ParticleSystem> ()) {
				Debug.Log (otro.transform.position);
				_particleObject.transform.position = otro.transform.position;
				Debug.Log (_particleObject.transform.position);
				_particleObject.GetComponent<ParticleSystem> ().Play();
			}
			int multiplier = isLeft ? 1 : -1; 
			otro.transform.position += new Vector3(0.2f * multiplier,0,0);
		}
	}
		
	void Update(){
		if (currentUpdate != null) {
			currentUpdate ();
		}
	}
	//NORMAL GAMEPLAY
	void UpdateNormal () {
		if (_moving && ! (_stunTimer >= 0)) {
			if (isBump1) {
				transform.Translate (0.0f, Input.GetAxisRaw ("Vertical") * speed * Time.deltaTime, 0.0f);
				if (_spark != null) {
					_spark.Stop ();
				}
			} else {
				transform.Translate (0.0f, Input.GetAxisRaw ("Vertical2") * speed * Time.deltaTime, 0.0f);
				if (_spark != null) {
					_spark.Stop ();
				}
			}
		}
		_stunTimer -= Time.deltaTime;
		BoundsCheck();
	}
	//FLAPPY GAMEPLAY
	void UpdateFlappy(){
		if (_moving) {
			if (isBump1) {
				if (Input.GetButtonDown ("Vertical")) {
					_rb.velocity = Vector2.zero;
					_rb.AddForce (new Vector2 (0f, speed),ForceMode2D.Impulse);
				}
			} else {
				if (Input.GetButtonDown ("Vertical2")) {
					_rb.velocity = Vector2.zero;
					_rb.AddForce (new Vector2 (0f, speed),ForceMode2D.Impulse);
				}
			}
		}
		BoundsCheck();
	}

	protected void BoundsCheck(){
		if(sprite.bounds.max.y > cam.ViewportToWorldPoint(Vector3.one).y - WallSprite.bounds.extents.y*2){
			transform.position = new Vector3(transform.position.x,
				cam.ViewportToWorldPoint(Vector3.one).y - sprite.bounds.extents.y - WallSprite.bounds.extents.y*2,
				transform.position.z);

		}
		else if(sprite.bounds.min.y < cam.ViewportToWorldPoint(Vector3.zero).y + WallSprite.bounds.extents.y*2){
			transform.position = new Vector3(transform.position.x,
				cam.ViewportToWorldPoint (Vector3.zero).y + sprite.bounds.extents.y + WallSprite.bounds.extents.y*2,
				transform.position.z);
		}
	}
	//PINBALL GAMEPLAY
	void UpdatePinball(){
		if (_moving) {
			if (isBumpLeftUp) {
				if (Input.GetButton("W")) {
					_Flipper.AddTorque (speed,ForceMode2D.Impulse);
				}
			}
			if (isBumpLeftDown) {
				if (Input.GetButton("S")) {
					_Flipper.AddTorque (speed,ForceMode2D.Impulse);
				}
			}
			if (isBumpRightUp) {
				if (Input.GetButton("Up")) {
					_Flipper.AddTorque (speed,ForceMode2D.Impulse);
				}
			}
			if (isBumpRightDown) {
				if (Input.GetButton("Down")) {
					_Flipper.AddTorque (speed,ForceMode2D.Impulse);
				}
			}
		}
	}
	public void rechargeExpand(){
		_expandLives = _expandMaxLives;
	}
	public void ResetPos(){
		transform.position = new Vector3 (transform.position.x, _thisY, _thisZ);
	}
	public void ResetAngle(){
		transform.eulerAngles = _originalRot;
	}

	public void stun(){
		_spark.Play ();
		_stunTimer = _stunTime;
	}
	public void updatePowerups(){
		GetComponent<LaserPower> ().Active = _laserActive;
		GetComponent<Expand> ().Active = _expandActive;
		GetComponent<Expand> ().ExpandBumper ();
	}
	public bool Move{
		get{ return _moving; }
		set{ _moving = value; }
	}
	public bool isLeft{
		get{ return isBump1; }
		set{ isBump1 = value; }
	}

	public bool laserActive{
		get{ return _laserActive; }
		set{ _laserActive = value; }
	}
	public bool expandActive{
		get{ return _expandActive; }
		set{ _expandActive = value; }
	}
	public bool chargeActive{
		get{ return _chargeActive; }
		set{ _chargeActive = value; }
	}
		
}
