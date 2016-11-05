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
        // TODO : Check tag hand
        door.Open(true);    
        phone.ShowTinder(true);
        gameObject.SetActive(false);
    }
}
