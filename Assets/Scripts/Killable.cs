using UnityEngine;
using System.Collections;

[RequireComponent(typeof (Collider))]
[RequireComponent(typeof (Rigidbody))]
public class Killable : MonoBehaviour {

	private bool isDead = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision collision){
		if (this.enabled) {
			Debug.Log ("Killable : collide");

			// Collide with a lethal object
			if (collision.gameObject.GetComponent<Lethal> () != null && collision.gameObject.GetComponent<Lethal> ().isActiveAndEnabled) {
				if (collision.relativeVelocity.magnitude >= collision.gameObject.GetComponent<Lethal> ().minVelocityToKill) {
					Kill ();
				}
			}
		}
	}

	void OnTriggerEnter(Collider other) {
		if (this.enabled) {
			Debug.Log ("Killable : trigger");

			// Collide with a lethal object
			if (other.gameObject.GetComponent<Lethal> () != null && other.gameObject.GetComponent<Lethal> ().isActiveAndEnabled) {
				if (other.GetComponent<Rigidbody>().velocity.magnitude + GetComponent<Rigidbody>().velocity.magnitude >= other.gameObject.GetComponent<Lethal> ().minVelocityToKill) {
					Kill ();
				}
			}
		}
	}

	void Kill(){
		Debug.Log ("Killed !");
		isDead = true;
	}

	public bool IsDead(){
		return isDead;
	}

}
