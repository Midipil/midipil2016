using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Smartphone : MonoBehaviour 
{
	public int controllerIndex { get; private set; }

	public MeshRenderer meshRender;
	public Baby cradle;
	public GameObject tinder;
	public TinderImg[] tinderImages;
	public GameObject fxExplosion;
	public Light screenLight;
	public GameObject screenON;
	public GameObject screenOFF;
	public Image batteryImg;
	public Text batteryLowText;
	public int batteryLifeSeconds;
	public ButtonDoor btnDoor;
	public Grabbable grab;
	public AudioSource audioExplode;
	public AudioSource audioLowBattery;
	public AudioSource audioMatchTinder;

    private Door door;
	private SmartphoneSceneManager sceneManager;
    private bool exploded = false;
    private bool george = false;
	private bool multiTinder = false;

    public int batteryLife
    {
		get 
		{
		    if(batteryImg != null)
		        return (int)(batteryImg.fillAmount * 100);	
		    else
		        return 0;	
		}
		set 
		{
	 	    if(batteryImg != null)
		        batteryImg.fillAmount = value / 100f;	
		} 
    }

    private float startTime;
    private int selectedTinder = 0;
    private bool noBattery;
    private bool drainBattery = true;
    private bool won;
    private SteamVR_TrackedObject right;
    private bool firstPulse = false;

    void Start()
    {
        controllerIndex = -1;
        sceneManager = (SmartphoneSceneManager)FindObjectOfType(typeof(SmartphoneSceneManager));
        GameObject obj = GameObject.Find("Door");
        if (obj)
            door = obj.GetComponent<Door>();
        batteryLife = 100;
        startTime = Time.time;

        if (batteryLowText)
            batteryLowText.gameObject.SetActive(false);

        SetBattery(false);
        
    }

    bool SetRightPulse(int time)
    {
        if(!right)
        {
            GameObject o = GameObject.FindGameObjectWithTag("RightHand");
            if (o)
                right = o.GetComponentInParent<SteamVR_TrackedObject>();
        }

        if (right)
        {
            SteamVR_Controller.Input((int)right.index).TriggerHapticPulse((ushort)time);
            return true;
        }
        else
            return false;
    }
	
	void Update () 
	{
        if (!firstPulse)
            firstPulse = SetRightPulse(1000);

        if (drainBattery)
		{
			float currentTime = Time.time - startTime;
			if(currentTime <= batteryLifeSeconds)
			{
				batteryLife = 100 - (int)((currentTime / batteryLifeSeconds) * 100);
			}
		}

		if(batteryLife <= 25 && !batteryLowText.gameObject.activeSelf)
		{
            audioLowBattery.Stop();
			audioLowBattery.Play();
            SetRightPulse(2000);
            batteryLowText.gameObject.SetActive(true);
		}
		else if(batteryLife > 25 && batteryLowText.gameObject.activeSelf)
		{
			batteryLowText.gameObject.SetActive(false);
		}

		if(batteryLife <= 0 && !noBattery && !exploded)
		{
			SetBattery(false);
			won = false;

            Explode();

        }
		else if(batteryLife > 0 && noBattery)
		{
			SetBattery(true);
		}

		if(controllerIndex != grab.controllerIndex)
			controllerIndex = grab.controllerIndex;
	}

	public void SetEndTime(bool win, float time)
	{
		won = win;
		Invoke("SetEnd", time);
	}

	void SetEnd()
	{
		sceneManager.SetEnd(won);
	}

	void SetBattery(bool empty)
	{
		screenON.SetActive(!empty);
		screenOFF.SetActive(empty);
		screenLight.enabled = !empty;
		noBattery = empty;
	}

	void OnCollisionEnter(Collision collision) 
	{
		if (collision.relativeVelocity.magnitude > 5)
        {
            Explode();
        }
	}

    private void Explode()
    {
        exploded = true;
        audioExplode.Stop();
        audioExplode.Play();
        Instantiate(fxExplosion, transform.position, Quaternion.identity);
        won = false;

        if (door)
        {
            if (Vector3.Distance(transform.position, door.transform.position) < 1.5f)
            {
                won = true;
                door.Destroy();
            }
        }

        meshRender.enabled = false;
        screenON.SetActive(false);

        Invoke("SetEnd", 3f);
    }

	public void ShowTinder(bool show)
	{
		if(show)
		{
			tinder.gameObject.SetActive(true);
			selectedTinder = 0;
			tinderImages[selectedTinder].Fade(true);
		}
		else
		{
			tinderImages[selectedTinder].Fade(false);
			tinder.gameObject.SetActive(false);
		}
	}

	public void NextTinder()
	{
		tinderImages[selectedTinder].Fade(false);

		selectedTinder++;
		if(selectedTinder >= tinderImages.Length)
			selectedTinder = 0;

		tinderImages[selectedTinder].Fade(true);	
	}

    public void SetGeorge()
    {
        george = true;
    }

	public void SetMultiTinder()
	{
		multiTinder = true;
	}

    public void MatchTinder()
	{
		if (george && ((!multiTinder && selectedTinder == 2) || (multiTinder && selectedTinder == 0)))
            cradle.SetGeorgeSound();
		else if (multiTinder && selectedTinder == 1)
			cradle.SetBabyJP ();
		
        tinder.gameObject.SetActive(false);
        audioMatchTinder.Stop();
		audioMatchTinder.Play();
		cradle.gameObject.SetActive(true);
		won = true;
		Invoke("SetEnd", 5f);
	}

	public void SetDrainBattery(bool drain)
	{
		drainBattery = drain;
	}

	public void SetBtnDoor(bool show)
	{
		btnDoor.gameObject.SetActive(show);
	}

	public void SetGeorgeFirst()
	{
		Sprite temp = tinderImages [0].img.sprite;

		tinderImages [0].SetImage (tinderImages [2].img.sprite);

		tinderImages [2].SetImage (temp);
	}

    public void SetTinderImages(Sprite[] new_images)
    {
        for(int i = 0; i < new_images.Length; i++)
        {
            if (i < tinderImages.Length)
                tinderImages[i].SetImage(new_images[i]);
        }
    }

    public void SetOneTinderImage(Sprite img, int index)
    {
        tinderImages[index].SetImage(img);
    }

}
