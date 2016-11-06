using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Smartphone : MonoBehaviour 
{
	public int controllerIndex { get; private set; }

	public GameObject cradle;
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

	private Door door;
	private SmartphoneSceneManager sceneManager;

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

	void Start () 
	{
		controllerIndex = -1;
		sceneManager = (SmartphoneSceneManager) FindObjectOfType(typeof(SmartphoneSceneManager));
		door = GameObject.Find("Door").GetComponent<Door>();
		batteryLife = 100;
		startTime = Time.time;

		if(batteryLowText)
			batteryLowText.gameObject.SetActive(false);

		SetBattery(false);
	}
	
	void Update () 
	{
		if(drainBattery)
		{
			float currentTime = Time.time - startTime;
			if(currentTime <= batteryLifeSeconds)
			{
				batteryLife = 100 - (int)((currentTime / batteryLifeSeconds) * 100);
			}
		}

		if(batteryLife <= 25 && !batteryLowText.gameObject.activeSelf)
		{
			batteryLowText.gameObject.SetActive(true);
		}
		else if(batteryLife > 25 && batteryLowText.gameObject.activeSelf)
		{
			batteryLowText.gameObject.SetActive(false);
		}

		if(batteryLife <= 0 && !noBattery)
		{
			SetBattery(false);
			won = false;
			Invoke("SetEnd", 2f);
		}
		else if(batteryLife > 0 && noBattery)
		{
			SetBattery(true);
		}

		if(controllerIndex != grab.controllerIndex)
			controllerIndex = grab.controllerIndex;
	}

	public void SetEnd(bool win, float time)
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
        	Instantiate(fxExplosion, transform.position, Quaternion.identity);

        	if(door)
        	{
        		if(Vector3.Distance(transform.position, door.transform.position) < 1.5f)
        			door.Destroy();
        	}

        	Destroy(gameObject);

        	won = true;
			Invoke("SetEnd", 3f);
        }
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

	public void MatchTinder()
	{
		cradle.gameObject.SetActive(false);
		won = true;
		Invoke("SetEnd", 4f);
	}

	public void SetDrainBattery(bool drain)
	{
		drainBattery = drain;
	}

	public void SetBtnDoor(bool show)
	{
		btnDoor.gameObject.SetActive(show);
	}

}
