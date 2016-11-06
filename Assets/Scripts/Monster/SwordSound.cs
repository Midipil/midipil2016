using UnityEngine;
using System.Collections;

[RequireComponent(typeof (Collider))]
public class SwordSound : MonoBehaviour {

	public AudioSource[] swordSounds;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag != "LeftHand" && other.gameObject.tag != "RightHand") {
			swordSounds [Random.Range (0, swordSounds.Length)].Play ();
		}
	}

}
