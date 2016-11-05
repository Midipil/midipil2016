﻿using UnityEngine;
using System.Collections;

public class Boiler : MonoBehaviour {

    CoffeSceneManager sceneManager;

    public GameObject milk;
    public Grabbable handle;
    public GameObject heatingParticles, boilingParticles;

    bool isHeating = true, handleGrabbed;

    SteamVR_TrackedController burningHand;

    public void Initialize(CoffeSceneManager sceneManager)
    {
        this.sceneManager = sceneManager;
        gameObject.SetActive(true);

        FindObjectOfType<TextDisplay>().DisplayText("Fait attention au lait !", 5f);

        if (sceneManager.handleTaken)
            Destroy(handle);

        Invoke("MakeNoise", 8f);

        //FindObjectOfType<Grabber>().Grab(gameObject);
    }

    /*public void StartHeating()
    {
        isHeating = true;
        Debug.Log("StartHeating");
    }*/

    void MakeNoise()
    {
        Debug.Log("MakeNoise");
    }

    void Update()
    {
        if (isHeating)
        {
            if (isHeating && !handleGrabbed && Vector3.Angle(transform.position - Camera.main.transform.position, Camera.main.transform.transform.forward) > Camera.main.fieldOfView)
            {
                StartBoiling();
            }
        }

        if (!handleGrabbed && handle.IsGrabbed())
        {
            Invoke("StartBoiling", 1f);
            sceneManager.handleTaken = true;
            handleGrabbed = true;
            CancelInvoke("MakeNoise");
        }

        if(GetComponent<Grabbable>() != null && GetComponent<Grabbable>().IsGrabbed() && isHeating)
        {
            heatingParticles.SetActive(false);
            boilingParticles.SetActive(false);
            CancelInvoke("MakeNoise");
        }

        if(transform.up.y < -0.8)
        {
            milk.GetComponent<Rigidbody>().useGravity = true;
            milk.GetComponent<Rigidbody>().isKinematic = false;
            Invoke("EnableMilkCollider", 0.2f);
            FindObjectOfType<TextDisplay>().ShowEndMessage("Oh mon dieu !\nIl a fait tomber le lait !!!", false);
            milk.AddComponent<Grabbable>();
        }
    }

    void EnableMilkCollider()
    {
        milk.GetComponent<Collider>().enabled = true;
    }

    public void HandBurning(SteamVR_TrackedController controller)
    {
        CancelInvoke("MakeNoise");

        if (controller == null && burningHand != null)
            sceneManager.SetEnd(false);

        burningHand = controller;

        InvokeRepeating("BurnHaptic", 0f, 0.1f);
    }

    void BurnHaptic()
    {
        //SteamVR_Controller.Input((int)burningHand.controllerIndex).TriggerHapticPulse(100);
    }

    void StartBoiling()
    {
        boilingParticles.SetActive(true);
        Invoke("BoilingFail", 1f);
    }

    void BoilingFail()
    {
        FindObjectOfType<TextDisplay>().ShowEndMessage("Tu as laissé déborder le lait !\nJean-Pierre Coffe n'est pas content du tout !", false);
    }
}
