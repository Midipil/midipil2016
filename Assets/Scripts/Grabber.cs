using UnityEngine;
using System.Collections;

[RequireComponent(typeof (Collider))]
[RequireComponent(typeof (Rigidbody))]
[RequireComponent(typeof (FixedJoint))]
public class Grabber : MonoBehaviour {

	public SteamVR_TrackedController controller = null;

	private Grabbable grabbedObject = null;

	private float dropCooldown = 0;
	private float grabCooldown = 0;
	private float cooldownAmount = 0.2f;


	// Use this for initialization
	void Start () {

		// Init rigidbody settings for hands
		gameObject.GetComponent<Rigidbody> ().useGravity = false;
		gameObject.GetComponent<Collider> ().isTrigger = true;

		//controller = transform.parent.GetComponent<SteamVR_TrackedController> ();
		//controller.TriggerClicked += OnTriggerClicked;


	}
	
	// Update is called once per frame
	void Update () {
		
		grabCooldown -= Time.deltaTime;
		dropCooldown -= Time.deltaTime;

		// If an object is grabbed
		if (grabbedObject != null 
			&& (controller.triggerPressed || controller.gripped)
			&& dropCooldown <= 0) {
			Drop ();
		}

		if (grabbedObject != null && grabbedObject.dropAsked)
			Drop ();
	}

	void OnTriggerEnter(Collider other) {

		if (this.enabled
			&& grabbedObject == null
			&& other.gameObject.GetComponent<Grabbable> () != null 
			&& other.gameObject.GetComponent<Grabbable> ().isActiveAndEnabled
			&& (controller.triggerPressed || controller.gripped)
			&& grabCooldown <= 0) {

			if (other.gameObject.GetComponent<Grabbable> ().cantBeGrabbed) {
				SteamVR_Controller.Input((int)GetComponentInParent<SteamVR_TrackedObject>().index).TriggerHapticPulse((ushort)1000);
			} else {
				Grab (other.gameObject);
			}
		}

	}

	void OnTriggerStay(Collider other) {

		if (this.enabled
			&& grabbedObject == null
			&& other.gameObject.GetComponent<Grabbable> () != null 
			&& other.gameObject.GetComponent<Grabbable> ().isActiveAndEnabled
			&& (controller.triggerPressed || controller.gripped)
			&& grabCooldown <= 0) {

			if (other.gameObject.GetComponent<Grabbable> ().cantBeGrabbed) {
				SteamVR_Controller.Input((int)GetComponentInParent<SteamVR_TrackedObject>().index).TriggerHapticPulse((ushort)1000);
			} else {
				Grab (other.gameObject);
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

		dropCooldown = cooldownAmount;
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

		grabCooldown = cooldownAmount;
	}

	void OnTriggerClicked(object o, ClickedEventArgs e){
		//Debug.Log ("click");
	}
	/*
	void Update () {
		if (controller!=null) {
			HandleControllerInput (controller);
		}
	}

	void HandleControllerInput(SteamVR_Controller.Device c){
		// Create sculpture on trigger press

	}
*/
}
