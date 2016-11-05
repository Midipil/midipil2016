using UnityEngine;
using System.Collections;

public class TinderBtn : MonoBehaviour 
{
	public Smartphone phone;
	public bool cancelBtn;

	private bool isCollisionEnabled = false;

	void OnTriggerEnter(Collider other) 
	{
		if(isCollisionEnabled)
		{
			// TODO : Check tag hand
	        if(cancelBtn)
	        	phone.NextTinder();
	       	else
	       		phone.MatchTinder();
		}
    }

    public void SetCollision(bool enabled)
    {
    	isCollisionEnabled = enabled;
    }
}
