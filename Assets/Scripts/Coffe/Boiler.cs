using UnityEngine;
using System.Collections;

public class Boiler : MonoBehaviour {

    CoffeSceneManager sceneManager;

    public GameObject milk;
    public Grabbable handle;
    public GameObject heatingParticles, boilingParticles;
    public AudioSource backNoise;
    public GameObject casserole;

    bool needToLook, handleGrabbed;
    public bool isHeating = true;
    bool isBoiling = false;

	SteamVR_TrackedObject burningHand;

    public void Initialize(CoffeSceneManager sceneManager)
    {
        this.sceneManager = sceneManager;
        gameObject.SetActive(true);

        FindObjectOfType<TextDisplay>().DisplayText("Ne laisse pas le lait déborder !", 8f);
        
		if (sceneManager.getLooseHandle()) {
            //Destroy(handle);
            //gameObject.AddComponent<Grabbable>();
            Destroy(GetComponent<Grabbable>());
            handle.enabled = true;
		} else
        {
            handle.enabled = false;
        }
        
		Invoke("NeedToLook", 5f);
        Invoke("MakeNoise", 4f);

        GetComponent<AudioSource>().Play();

        //FindObjectOfType<Grabber>().Grab(gameObject);
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
            if (!GetComponent<AudioSource>().isPlaying)
            {
                GetComponent<AudioSource>().Play();
            }
            if (isBoiling)
            {
                heatingParticles.SetActive(false);
                boilingParticles.SetActive(true);
            } else
            {
                heatingParticles.SetActive(true);
                boilingParticles.SetActive(false);
            }
        }
        else
        {
            GetComponent<AudioSource>().Stop();
            heatingParticles.SetActive(false);
            boilingParticles.SetActive(false);
        }

        // la casserole chauffe
        if (isHeating
            && needToLook
            && Vector3.Angle(transform.position - Camera.main.transform.position, Camera.main.transform.transform.forward) > Camera.main.fieldOfView)
        {
            StartBoiling();
            GetComponent<AudioSource>().volume = 1f;
        }

        if (!handleGrabbed && handle.IsGrabbed() && sceneManager.getLooseHandle()==true)
        {
            //Invoke("StartBoiling", 1f);

            Destroy(GetComponent<Rigidbody>());
			handleGrabbed = true;
			handle.GetComponent<Rigidbody>().useGravity = true;
			handle.GetComponent<Rigidbody>().isKinematic = false;
			handle.GetComponent<Collider>().isTrigger = false;
            handle.transform.parent = null;
            GetComponent<Collider>().isTrigger = true;
            casserole.GetComponent<Collider>().isTrigger = false;
            casserole.GetComponent<Rigidbody>().isKinematic = false;
            casserole.GetComponent<Rigidbody>().useGravity = true;
            CancelInvoke("MakeNoise");
        }

        /*if(GetComponent<Grabbable>() != null && GetComponent<Grabbable>().IsGrabbed() && isHeating && sceneManager.getLooseHandle() == false)
        {
            heatingParticles.SetActive(false);
            boilingParticles.SetActive(false);
            GetComponent<AudioSource>().Stop();
            CancelInvoke("MakeNoise");
            isHeating = false;
            Debug.Log("case 1");
        }*/

        if(transform.up.y < -0.8)
        {
            milk.GetComponent<Rigidbody>().useGravity = true;
            milk.GetComponent<Rigidbody>().isKinematic = false;
            heatingParticles.SetActive(false);
            boilingParticles.SetActive(false);
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
        isBoiling = true;
        boilingParticles.SetActive(true);
        Invoke("BoilingFail", 1f);
    }

    void BoilingFail()
    {
        FindObjectOfType<TextDisplay>().ShowEndMessage("Tu as laissé déborder le lait !\nJean-Pierre Coffe n'est pas content du tout !", false);
    }
}
