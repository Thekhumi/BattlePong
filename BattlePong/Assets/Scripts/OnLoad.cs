using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnLoad : MonoBehaviour {

	[SerializeField] MusicManager.Music _music;
	void Start () {
		MusicManager.Instance.music = _music;
	}
}
