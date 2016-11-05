using UnityEngine;
using System.Collections;

public class Boiler : MonoBehaviour {

    CoffeSceneManager sceneManager;

    public GameObject milk;
    public Grabbable handle;
    public GameObject boilingParticles;

    bool isHeating = true;

    public void Initialize(CoffeSceneManager sceneManager)
    {
        this.sceneManager = sceneManager;
        gameObject.SetActive(true);

        if (sceneManager.handleTaken)
            Destroy(handle);
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

        if (handle.IsGrabbed())
        {
            Invoke("StartBoiling", 1f);
            sceneManager.handleTaken = true;
        }
    }

    void StartBoiling()
    {
        boilingParticles.SetActive(true);
        Invoke("BoilingFail", 1f);
    }

    void BoilingFail()
    {
        FindObjectOfType<TextDisplay>().ShowEndScreen("Tu as laissé déborder le lait !\nJean-Pierre Coffe n'est pas content du tout !", false);
    }
}
