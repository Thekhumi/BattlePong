using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumper : MonoBehaviour {

	[SerializeField]bool isBump1;
	[SerializeField]float speed = 5f;
	private float _thisY;
	private float _thisZ;
	private bool _moving;
	GameObject Wall;
	SpriteRenderer sprite;
	SpriteRenderer WallSprite;
	Camera cam;
	void Start(){
		cam = Camera.main;
		sprite = GetComponent<SpriteRenderer> ();
		Wall = GameObject.FindGameObjectsWithTag ("BoundWall")[0];
		WallSprite = Wall.GetComponent<SpriteRenderer> ();
		_thisY = transform.position.y;
		_thisZ = transform.position.z;
		_moving = false;
	}
	void Update () {
		if (_moving) {
			if (isBump1) {
				transform.Translate (0.0f, Input.GetAxis ("Vertical") * speed * Time.deltaTime, 0.0f);
			} else {
				transform.Translate (0.0f, Input.GetAxis ("Vertical2") * speed * Time.deltaTime, 0.0f);
			}
		}
		BoundsCheck();
	}

	void BoundsCheck(){
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
	public void ResetPos(){
		transform.position = new Vector3 (transform.position.x, _thisY, _thisZ);
	}
	public bool Move{
		get{ return _moving; }
		set{ _moving = value; }
	}
}
