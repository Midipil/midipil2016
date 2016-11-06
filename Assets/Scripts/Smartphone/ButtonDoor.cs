using UnityEngine;
using System.Collections;

public class ButtonDoor : MonoBehaviour 
{
	public Smartphone phone;
	private Door door;

	void Start () 
	{
		door = GameObject.Find("Door").GetComponent<Door>();
	}
	
	void OnTriggerEnter(Collider other) 
	{
        bool ok = false;

        if(other.gameObject.tag.Equals("LeftHand") || other.gameObject.tag.Equals("RightHand"))
        {
        	int index = (int)other.gameObject.GetComponentInParent<SteamVR_TrackedObject>().index;

        	if(phone.controllerIndex != index)
			{
				door.Open(true);
				phone.SetEndTime(true, 3f);
	        	gameObject.SetActive(false);
			}
        }
    }
}
