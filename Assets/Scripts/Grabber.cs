using UnityEngine;
using System.Collections;

[RequireComponent(typeof (Collider))]
[RequireComponent(typeof (Rigidbody))]
public class Grabber : MonoBehaviour {

	// Use this for initialization
	void Start () {

		// Init rigidbody settings for hands
		gameObject.GetComponent<Rigidbody> ().useGravity = false;
		gameObject.GetComponent<Collider> ().isTrigger = true;

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {

		if (this.enabled) {
			Debug.Log ("Grabber : Collision !");

			if (other.gameObject.GetComponent<Grabbable> () != null && other.gameObject.GetComponent<Grabbable> ().isActiveAndEnabled) {
				Grab (other.gameObject);
			}
		}

	}

	void Grab(GameObject objToGrab){
		Debug.Log ("Grabbed !");
		objToGrab.transform.parent = this.transform;
		objToGrab.transform.localPosition = Vector3.zero;
		objToGrab.transform.localRotation = Quaternion.identity;
		objToGrab.GetComponent<Rigidbody> ().useGravity = false;
		objToGrab.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeAll;
	}

	void Drop(){

	}

}
