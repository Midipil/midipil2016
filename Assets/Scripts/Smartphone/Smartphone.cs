using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Smartphone : MonoBehaviour 
{
	public Image batteryImg;
	public int batteryLifeSeconds;

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

	void Start () 
	{
		batteryLife = 100;
		startTime = Time.time;
	}
	
	void Update () 
	{
		float currentTime = Time.time - startTime;
		if(currentTime <= batteryLifeSeconds)
		{
			batteryLife = 100 - (int)((currentTime / batteryLifeSeconds) * 100);
		}

		if(batteryLife <= 0)
		{

		}
	}
}
