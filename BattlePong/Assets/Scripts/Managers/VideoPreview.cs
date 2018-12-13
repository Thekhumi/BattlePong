using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoPreview : MonoBehaviour {

	[SerializeField] GameObject _video1;
	[SerializeField] GameObject _video2;
	[SerializeField] GameObject _video3;
	[SerializeField] GameObject _video4;
	[SerializeField] GameObject _video5;
	[SerializeField] GameObject _video6;
	[SerializeField] bool _is1P;
	Cartridge _cart2p;
	Cartridge1P _cart1p;
	int _cont;

	void Awake(){
		if (_is1P) {
			_cart1p = FindObjectOfType<Cartridge1P> ();
		} else {
			_cart2p = FindObjectOfType<Cartridge> ();
		}
	}
	void Update () {
		switch (_is1P) {
		case true:
			_cont = _cart1p.Cont;
			break;
		case false:
			_cont = _cart2p.Cont;
			break;
		}
		switch (_cont) {
		case 0:
			_video1.SetActive(true);
			_video2.SetActive(false);
			_video3.SetActive(false);
			_video4.SetActive(false);
			_video5.SetActive(false);
			_video6.SetActive(false);
			break;
		case 1:
			_video1.SetActive(false);
			_video2.SetActive(true);
			_video3.SetActive(false);
			_video4.SetActive(false);
			_video5.SetActive(false);
			_video6.SetActive(false);
			break;
		case 2:
			_video1.SetActive(false);
			_video2.SetActive(false);
			_video3.SetActive(true);
			_video4.SetActive(false);
			_video5.SetActive(false);
			_video6.SetActive(false);
			break;
		case 3:
			_video1.SetActive(false);
			_video2.SetActive(false);
			_video3.SetActive(false);
			_video4.SetActive(true);
			_video5.SetActive(false);
			_video6.SetActive(false);
			break;
		case 4:
			_video1.SetActive(false);
			_video2.SetActive(false);
			_video3.SetActive(false);
			_video4.SetActive(false);
			_video5.SetActive(true);
			_video6.SetActive(false);
			break;
		case 5:
			_video1.SetActive(false);
			_video2.SetActive(false);
			_video3.SetActive(false);
			_video4.SetActive(false);
			_video5.SetActive(false);
			_video6.SetActive(true);
			break;
		}
	}
}
