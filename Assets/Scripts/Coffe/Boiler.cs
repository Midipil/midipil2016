﻿using UnityEngine;
using System.Collections;

public class Boiler : MonoBehaviour {

    CoffeSceneManager sceneManager;

    public GameObject milk;
    public Grabbable handle;
    public GameObject heatingParticles, boilingParticles;
    public AudioSource backNoise;

    bool needToLook, isHeating = true, handleGrabbed;

	SteamVR_TrackedObject burningHand;

    public void Initialize(CoffeSceneManager sceneManager)
    {
        this.sceneManager = sceneManager;
        gameObject.SetActive(true);

        FindObjectOfType<TextDisplay>().DisplayText("Fait attention au lait !", 8f);

		if (sceneManager.handleTaken) {
			Destroy(handle);
			gameObject.AddComponent<Grabbable>();
		}

		Invoke("NeedToLook", 5f);
        Invoke("MakeNoise", 10f);

        GetComponent<AudioSource>().Play();

        FindObjectOfType<Grabber>().Grab(gameObject);
    }

    /*public void StartHeating()
    {
        isHeating = true;
        Debug.Log("StartHeating");
    }*/

	void NeedToLook()
	{
		needToLook = true;
	}

    void MakeNoise()
    {
        backNoise.Play();
    }

    void Update()
    {
        if (isHeating)
        {
			if (needToLook && isHeating && !handleGrabbed && Vector3.Angle(transform.position - Camera.main.transform.position, Camera.main.transform.transform.forward) > Camera.main.fieldOfView)
            {
                StartBoiling();
                GetComponent<AudioSource>().volume = 1f;
            }
        }

        if (!handleGrabbed && handle.IsGrabbed())
        {
            //Invoke("StartBoiling", 1f);
            sceneManager.handleTaken = true;
			handleGrabbed = true;
			handle.GetComponent<Rigidbody>().useGravity = true;
			handle.GetComponent<Rigidbody>().isKinematic = false;
			handle.GetComponent<Collider>().isTrigger = false;
            CancelInvoke("MakeNoise");
        }

        if(GetComponent<Grabbable>() != null && GetComponent<Grabbable>().IsGrabbed() && isHeating)
        {
            heatingParticles.SetActive(false);
            boilingParticles.SetActive(false);
            GetComponent<AudioSource>().Stop();
            CancelInvoke("MakeNoise");
            isHeating = false;
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

    /*public void HandBurning(SteamVR_TrackedObject controller)
	{
		Debug.Log("HandBurning");
        CancelInvoke("MakeNoise");

		if (controller == null && burningHand != null) {
			sceneManager.SetEnd (false);
			CancelInvoke ("BurnHaptic");
		}
		else if (controller != null && burningHand == null) {
			burningHand = controller;
			InvokeRepeating("BurnHaptic", 0f, 0.1f);
		}
    }

    void BurnHaptic()
    {
		SteamVR_Controller.Input((int)burningHand.index).TriggerHapticPulse(5000);
    }*/

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
