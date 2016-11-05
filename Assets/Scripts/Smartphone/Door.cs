using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour 
{
	public float timeMoveDoor; // Time to open the door

	private bool opening, isOpen;
	private float startTimeMove;

	void Start () 
	{
		opening = false;
		isOpen = false;	
	}

	void Update()
	{
		if(!isOpen && opening)
		{
			float t = (Time.time - startTimeMove) / timeMoveDoor;

			if(t <= 1f)
			{
				transform.rotation = Quaternion.Euler(0f, t * 90f, 0f);
			}
			else
			{
				isOpen = true;
				opening = false;
			}
		}
	}

	public void Open()
	{
		if(!isOpen)
			StartCoroutine("OpenDoor");
	}

	IEnumerator OpenDoor()
	{
		yield return new WaitForSeconds(2f);
		startTimeMove = Time.time;
		opening = true;
	}
}
