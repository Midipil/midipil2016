using UnityEngine;
using System.Collections;

[RequireComponent(typeof (Collider))]
[RequireComponent(typeof (Rigidbody))]
public class Grabber : MonoBehaviour {

	SteamVR_Controller.Device controller = null;

	private Grabbable grabbedObject = null;

	private float dropCooldown = 0;
	private float dropCooldownAmount = 0.1f;


	// Use this for initialization
	void Start () {

		// Init rigidbody settings for hands
		gameObject.GetComponent<Rigidbody> ().useGravity = false;
		gameObject.GetComponent<Collider> ().isTrigger = true;

	}
	
	// Update is called once per frame
	void Update () {

		dropCooldown -= Time.deltaTime;

		// If an object is grabbed
		if (grabbedObject != null 
			&& Input.GetKeyUp ("g")
			&& dropCooldown <= 0) {
			Drop ();
		}

	}

	void OnTriggerEnter(Collider other) {

		if (this.enabled
			&& grabbedObject == null
			&& other.gameObject.GetComponent<Grabbable> () != null 
			&& other.gameObject.GetComponent<Grabbable> ().isActiveAndEnabled
			&& Input.GetKeyUp ("g")) {
				
			Grab (other.gameObject);
		}

	}

	void OnTriggerStay(Collider other) {

		if (this.enabled
			&& grabbedObject == null
			&& other.gameObject.GetComponent<Grabbable> () != null 
			&& other.gameObject.GetComponent<Grabbable> ().isActiveAndEnabled
			&& Input.GetKeyUp ("g")) {
					
			Grab (other.gameObject);
		}

	}

	void Grab(GameObject objToGrab){
		Debug.Log ("Grabbed !");

		grabbedObject = objToGrab.GetComponent<Grabbable> ();
		grabbedObject.transform.parent = this.transform;
		grabbedObject.transform.localPosition = Vector3.zero;
		grabbedObject.transform.localRotation = Quaternion.identity;
		grabbedObject.GetComponent<Rigidbody> ().useGravity = false;
		grabbedObject.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeAll;
		grabbedObject.SetGrabbed(true);

		dropCooldown = dropCooldownAmount;
	}

	void Drop(){
		Debug.Log ("Dropped !");

		grabbedObject.transform.parent = null;
		grabbedObject.GetComponent<Rigidbody> ().useGravity = true;
		grabbedObject.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.None;
		grabbedObject.SetGrabbed (false);
		grabbedObject = null;
	}

}
