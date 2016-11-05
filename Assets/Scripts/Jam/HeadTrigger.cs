using UnityEngine;
using System.Collections;

public class HeadTrigger : MonoBehaviour {

	void OnTriggerEnter(Collider other)
    {
        if (other.name == "Coffee")
            other.GetComponent<Coffee>().CollideWithHead(true);
        else if (other.name == "HTCVive")
            other.GetComponent<Coffee>().CollideWithHead(true);
    }

    void OnTriggerExit(Collider other)
    {
        if (other.name == "Coffee")
            other.GetComponent<Coffee>().CollideWithHead(false);
        else if (other.name == "HTCVive")
            other.GetComponent<Coffee>().CollideWithHead(false);
    }
}
