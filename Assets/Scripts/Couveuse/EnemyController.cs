using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	float horizontal = 0.0f;
	float vertical = 0.0f;

	float speed = 0.02f; 

	// Update is called once per frame
	void FixedUpdate() {

		// Old method
		//transform.Rotate(new Vector3(Input.GetAxis("Enemy_Vertical"), Input.GetAxis("Enemy_Horizontal"), 0));
	
		this.GetComponent<Rigidbody>().AddTorque(transform.up * horizontal * speed);
		this.GetComponent<Rigidbody>().AddTorque(- transform.right * vertical * speed);

	}

	public void setDirection(float h, float v){
		horizontal = h;
		vertical = v;
	}
}
