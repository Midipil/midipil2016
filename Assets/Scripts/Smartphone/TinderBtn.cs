using UnityEngine;
using System.Collections;

public class TinderBtn : MonoBehaviour 
{
	public Smartphone phone;
	public bool cancelBtn;

	void OnTriggerEnter(Collider other) 
	{
		if(other)
		{
			if(other.gameObject.tag.Equals("Finger")) //  || other.gameObject.tag.Equals("RightHand")
            {
	        	/*int index = (int)other.gameObject.GetComponentInParent<SteamVR_TrackedObject>().index;

	        	if(phone.controllerIndex != index)
				{*/
					if(cancelBtn)
			        	phone.NextTinder();
			       	else
			       		phone.MatchTinder();
				//}
			}
		}
    }
}
