using UnityEngine;
using System.Collections;

[RequireComponent(typeof (Collider))]
[RequireComponent(typeof (Rigidbody))]
[RequireComponent(typeof (FixedJoint))]
public class Grabber : MonoBehaviour {

	public SteamVR_TrackedController controller = null;

	private Grabbable grabbedObject = null;

	// Use this for initialization
	void Start () {

		// Init rigidbody settings for hands
		gameObject.GetComponent<Rigidbody> ().useGravity = false;
		gameObject.GetComponent<Collider> ().isTrigger = true;

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerStay(Collider other) {

		if (this.enabled
			&& other.gameObject.GetComponent<Grabbable> () != null 
			&& other.gameObject.GetComponent<Grabbable> ().isActiveAndEnabled) {

			// Grab
			if (grabbedObject == null && (controller.triggerPressed || controller.gripped)) {
				if (other.gameObject.GetComponent<Grabbable> ().cantBeGrabbed) {
					SteamVR_Controller.Input ((int)GetComponentInParent<SteamVR_TrackedObject> ().index).TriggerHapticPulse ((ushort)1000);
				} else {
					Grab (other.gameObject);
				}
			} 
			// Drop
			else if (grabbedObject != null && !controller.triggerPressed && !controller.gripped) {
				Drop ();
			}


		}

	}

	public void Grab(GameObject objToGrab){
		Debug.Log ("Grabbed !");
		grabbedObject = objToGrab.GetComponent<Grabbable> ();
		objToGrab.transform.position = this.transform.position;
		objToGrab.transform.rotation = this.transform.rotation;
		objToGrab.GetComponent<Collider> ().isTrigger = true;
		GetComponent<FixedJoint> ().connectedBody = objToGrab.GetComponent<Rigidbody> ();
		grabbedObject.SetGrabbed(true, (int)GetComponentInParent<SteamVR_TrackedObject>().index);

	}

	void Drop(){
		Debug.Log ("Dropped !");

		var rigidbody = grabbedObject.GetComponent<Rigidbody>();
		var trackedObj = GetComponentInParent<SteamVR_TrackedObject>();
		var device = SteamVR_Controller.Input((int)trackedObj.index);

		GetComponent<FixedJoint> ().connectedBody = null;
		grabbedObject.SetGrabbed (false);
		grabbedObject.GetComponent<Collider> ().isTrigger = false;
		grabbedObject = null;

		// We should probably apply the offset between trackedObj.transform.position
		// and device.transform.pos to insert into the physics sim at the correct
		// location, however, we would then want to predict ahead the visual representation
		// by the same amount we are predicting our render poses.

		var origin = trackedObj.origin ? trackedObj.origin : trackedObj.transform.parent;
		if (origin != null)
		{
			rigidbody.velocity = origin.TransformVector(device.velocity);
			rigidbody.angularVelocity = origin.TransformVector(device.angularVelocity);
		}
		else
		{
			rigidbody.velocity = device.velocity;
			rigidbody.angularVelocity = device.angularVelocity;
		}

		rigidbody.maxAngularVelocity = rigidbody.angularVelocity.magnitude;

	}

}
