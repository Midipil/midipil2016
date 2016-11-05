using UnityEngine;
using System.Collections;

public class Boiler : MonoBehaviour {

    CoffeSceneManager sceneManager;

    public GameObject milk;

    bool isHeating = true;

    public void Initialize(CoffeSceneManager sceneManager)
    {
        this.sceneManager = sceneManager;
    }

    public void StartHeating()
    {
        isHeating = true;
        Debug.Log("StartHeating");
    }

    void Update()
    {
        if (isHeating)
        {
            if (Vector3.Angle(transform.position - Camera.main.transform.position, Camera.main.transform.transform.forward) > Camera.main.fieldOfView)
            {
                StartBoiling();
            }
        }
    }

    void StartBoiling()
    {
        // son
        // particleEffect
        milk.GetComponent<Renderer>().material.color = Color.red;
    }
}
