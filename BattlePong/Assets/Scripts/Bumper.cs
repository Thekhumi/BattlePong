using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumper : MonoBehaviour {
	private delegate void UpdateDelegate();
	private UpdateDelegate currentUpdate;
	[SerializeField] bool isBump1;
	[SerializeField] float speed = 5f;
	Rigidbody2D _rb;
	float _thisY;
	float _thisZ;
	bool _moving;
	GameObject Wall;
	SpriteRenderer sprite;
	SpriteRenderer WallSprite;
	Camera cam;
	AudioSource audioSrc;

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
		}
		Wall = GameObject.FindGameObjectsWithTag ("BoundWall")[0];
		WallSprite = Wall.GetComponent<SpriteRenderer> ();

	}
	void OnTriggerEnter2D(Collider2D otro){
		if (otro.gameObject.tag == "Ball" && otro.GetComponent<Ball>().inmuneTimer > 0) {
			audioSrc.Play ();
		}
	}
		
	void Update(){
		if (currentUpdate != null) {
			currentUpdate ();
		}
	}
	//NORMAL GAMEPLAY
	void UpdateNormal () {
		if (_moving) {
			if (isBump1) {
				transform.Translate (0.0f, Input.GetAxis ("Vertical") * speed * Time.deltaTime, 0.0f);
			} else {
				transform.Translate (0.0f, Input.GetAxis ("Vertical2") * speed * Time.deltaTime, 0.0f);
			}
		}
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
	public void ResetPos(){
		transform.position = new Vector3 (transform.position.x, _thisY, _thisZ);
	}
	public void ResetAngle(){
		transform.eulerAngles = _originalRot;
	}
	public bool Move{
		get{ return _moving; }
		set{ _moving = value; }
	}
		
}
