using UnityEngine;
using System.Collections;

[RequireComponent(typeof (Rigidbody))]
public class Sword : MonoBehaviour {

	public bool unlockable = true;
	public AudioSource swordUnlockSound;

	public Grabbable grabbable = null;

	void Awake(){
		LockSword ();
	}

	// Use this for initialization
	void Start () {

		if (!unlockable) {
			grabbable.cantBeGrabbed = true;
		}

	}
	
	// Update is called once per frame
	void Update () {

		if (grabbable.IsGrabbed ()) {
			UnlockSword ();
		}

	}

	private void LockSword(){
		gameObject.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeAll;
	}

	private void UnlockSword(){
		gameObject.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.None;
		swordUnlockSound.Play ();
	}

}
