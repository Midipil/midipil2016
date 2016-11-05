using UnityEngine;
using System.Collections;

public class TriggerStates : MonoBehaviour {

    public bool isTriggered;

    void OnTriggerEnter(Collider other)
    {
        isTriggered = true;
        if (name == "PorteFour" && other.tag == "Hand")
        {
            transform.parent.GetComponent<Hoven>().ToggleDoor(other.GetComponent<Grabber>().controller);
        }
        else if (name == "Casserole" && other.tag == "Hand")
        {
            transform.parent.GetComponent<Boiler>().HandBurning(other.GetComponent<Grabber>().controller);
        }
    }


    void OnTriggerExit(Collider other)
    {
        isTriggered = false;

        if (name == "Casserole" && other.tag == "Hand")
        {
            transform.parent.GetComponent<Boiler>().HandBurning(null);
        }
    }
}
