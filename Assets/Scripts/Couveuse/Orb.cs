using UnityEngine;
using System.Collections;

public class Orb : MonoBehaviour {

	float winTime =2.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider o) {
		Invoke ("Win", winTime);
		this.transform.GetComponent<Renderer> ().enabled = false;
		this.transform.GetChild (0).gameObject.SetActive (false);
	}

	void Win(){
		FindObjectOfType<CouveuseSceneManager> ().SetEnd (true);
	}
}
