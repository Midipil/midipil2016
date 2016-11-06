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
			bool ok = false;

			if(phone.controllerIndex == -1)
				ok = other.gameObject.tag.Equals("LeftHand") || other.gameObject.tag.Equals("RightHand");
			else if(phone.controllerIndex == 0)
				ok = other.gameObject.tag.Equals("RightHand");
			else if(phone.controllerIndex == 1)
				ok = other.gameObject.tag.Equals("LeftHand");

			if(ok)
			{
				if(cancelBtn)
		        	phone.NextTinder();
		       	else
		       		phone.MatchTinder();
			}
		}
    }

    public void SetCollision(bool enabled)
    {
    	isCollisionEnabled = enabled;
    }
}
