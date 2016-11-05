using UnityEngine;
using System.Collections;

public class TriggerStates : MonoBehaviour {

    public bool isTriggered;

    void OnTriggerEnter(Collider other)
    {
        isTriggered = true;
        if (name == "PorteFour" && other.tag == "Player")
        {
            transform.parent.GetComponent<Hoven>().ToggleDoor();
        }
    }


    void OnTriggerExit(Collider other)
    {
        isTriggered = false;
        if (other.gameObject.name == "Pan")
        {
            
        }
    }
}
