using UnityEngine;
using System.Collections;

[RequireComponent(typeof (Collider))]
public class SnapGrabbedObject : MonoBehaviour {

	public Vector3 positionOffset;

	// Use this for initialization
	void Start () {
		gameObject.GetComponent<Collider> ().isTrigger = true;
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter(Collider other) {

		if (this.enabled
			&& other.gameObject.GetComponent<Grabbable> () != null 
			&& other.gameObject.GetComponent<Grabbable> ().isActiveAndEnabled
			&& other.gameObject.GetComponent<Grabbable> ().IsGrabbed()) {
			other.gameObject.GetComponent<Grabbable> ().AskDrop ();
			other.gameObject.transform.position = transform.position + positionOffset;
			other.gameObject.transform.rotation = transform.rotation;

			if (other.name == "Cake")
				FindObjectOfType<Hoven> ().cakePlaced = true;
		}

	}

}
