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

		if(phone.controllerIndex == -1)
			ok = other.gameObject.tag.Equals("LeftHand") || other.gameObject.tag.Equals("RightHand");
		else if(phone.controllerIndex == 0)
			ok = other.gameObject.tag.Equals("RightHand");
		else if(phone.controllerIndex == 1)
			ok = other.gameObject.tag.Equals("LeftHand");

		if(ok)
		{
			door.Open(true);
			phone.SetEnd(true, 3f);
        	gameObject.SetActive(false);
		}
        
    }
}
