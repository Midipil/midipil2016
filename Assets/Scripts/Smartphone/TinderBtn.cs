using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TinderBtn : MonoBehaviour 
{
	public Smartphone phone;
	public bool cancelBtn;
    public Image img;

    private bool triggerEnabled = true;

    public void SetEnabled(bool enable)
    {
        triggerEnabled = enable;
        Color c = img.color;
        c.a = enable ? 1f : 0.2f;
        img.color = c;
    }

	void OnTriggerEnter(Collider other) 
	{
		if(other && triggerEnabled)
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
