using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class IsTheCakeInside : MonoBehaviour {

    Hoven hoven = null;

	// Use this for initialization
	void Start () {
        gameObject.GetComponent<Collider>().isTrigger = true;
        hoven = FindObjectOfType<Hoven>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerStay(Collider other)
    {
        if (this.enabled && other.name == "Cake" && hoven != null)
        {
            hoven.cakePlaced = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (this.enabled && other.name == "Cake" && hoven != null)
        {
            hoven.cakePlaced = false;
        }
    }

}
