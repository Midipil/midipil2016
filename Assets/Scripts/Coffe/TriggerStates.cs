using UnityEngine;
using System.Collections;

public class TriggerStates : MonoBehaviour {

    public bool isTriggered;

    void OnTriggerEnter(Collider other)
    {
        isTriggered = true;
		if (name == "PorteFour" && other.tag.Contains("Hand"))
        {
            transform.parent.GetComponent<Hoven>().ToggleDoor(other.GetComponent<Grabber>().controller);
        }
        /*else if (name == "Casserole" && other.tag == "Hand")
        {
			transform.parent.GetComponent<Boiler>().HandBurning(other.GetComponentInParent<SteamVR_TrackedObject>());
        }*/
    }


    void OnTriggerExit(Collider other)
    {
        isTriggered = false;

		if (name == "Casserole" && other.tag.Contains("Hand"))
        {
			//Debug.Log ("END");
            FindObjectOfType<CoffeSceneManager>().SetEnd(false);
        }
	}


	void OnTriggerStay(Collider other)
	{
		isTriggered = true;

		if (name == "Casserole" && other.tag.Contains("Hand"))
		{
			//Debug.Log ("HAPTIC");
			SteamVR_Controller.Input((int)other.GetComponentInParent<SteamVR_TrackedObject>().index).TriggerHapticPulse(5000);

		}
	}
}
