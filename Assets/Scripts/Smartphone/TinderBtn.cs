using UnityEngine;
using System.Collections;

public class TinderBtn : MonoBehaviour 
{
	public Smartphone phone;
	public bool cancelBtn;

	private bool isCollisionEnabled = false;

	void OnTriggerEnter(Collider other) 
	{
		if(isCollisionEnabled && other)
		{
			if(other.gameObject.tag.Equals("LeftHand") || other.gameObject.tag.Equals("RightHand"))
        	{
	        	int index = (int)other.gameObject.GetComponentInParent<SteamVR_TrackedObject>().index;

	        	if(phone.controllerIndex != index)
				{
					if(cancelBtn)
			        	phone.NextTinder();
			       	else
			       		phone.MatchTinder();
				}
			}
		}
    }

    public void SetCollision(bool enabled)
    {
    	isCollisionEnabled = enabled;
    }
}
