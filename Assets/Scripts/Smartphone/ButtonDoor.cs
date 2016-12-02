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
		if (other != null) 
		{
			if(other.gameObject.tag.Equals("Finger"))
			{
				//int index = (int)other.gameObject.GetComponentInParent<SteamVR_TrackedObject>().index;

				//if(phone.controllerIndex != index)
				//{
					door.Open(true);
					gameObject.SetActive(false);
				//}
			}
		} 
    }
}
