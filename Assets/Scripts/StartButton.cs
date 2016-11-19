using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour {

	public float pressOffset = 0.01f;
	Transform child;
	Vector3 initPos;

	// Use this for initialization
	void Start () {

		child = this.transform.GetChild (0);
		initPos = child.localPosition;

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider o) {
		
		child.localPosition = new Vector3 (initPos.x, initPos.y  -  pressOffset, initPos.z);

		SceneManager.LoadScene ("Smartphone", LoadSceneMode.Single);

	} 

	void OnTriggerExit(Collider o) {

		child.localPosition = initPos;

	}

    void OnTriggerStay(Collider o)
    {
        SteamVR_Controller.Input((int)o.transform.GetComponentInParent<SteamVR_TrackedObject>().index).TriggerHapticPulse((ushort)1000);
    }

}
