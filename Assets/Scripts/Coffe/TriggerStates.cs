using UnityEngine;
using System.Collections;

public class TriggerStates : MonoBehaviour {

    public bool isTriggered;

    void OnTriggerEnter(Collider other)
    {
        isTriggered = true;
        if (name == "PorteFour" && other.tag == "Hand")
        {
            transform.parent.GetComponent<Hoven>().ToggleDoor();
        }
        else if (name == "Casserole" && other.tag == "Hand")
        {
            Debug.Log("Je me brule !!!");
        }
    }


    void OnTriggerExit(Collider other)
    {
        isTriggered = false;
    }
}
