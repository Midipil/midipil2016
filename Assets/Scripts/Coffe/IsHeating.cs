using UnityEngine;
using System.Collections;

public class IsHeating : MonoBehaviour {

    Boiler boiler;

	// Use this for initialization
	void Start () {
        boiler = FindObjectOfType<Boiler>();
    }
	
	// Update is called once per frame
	void Update () {

    }

    void OnTriggerStay(Collider other)
    {
        if (this.enabled && other.name == "Casserole" && boiler != null)
        {
            boiler.isHeating = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (this.enabled && other.name == "Casserole" && boiler != null)
        {
            boiler.isHeating = false;
        }
    }

}
