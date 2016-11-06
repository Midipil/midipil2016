﻿using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour 
{
	public AudioSource audio;
	public float timeMoveDoor; // Time to open the door

	private bool opening, isOpen;
	private float startTimeMove;
	private Smartphone phone;

	void Start () 
	{
		opening = false;
		isOpen = false;	
		phone = GameObject.Find("Smartphone").GetComponent<Smartphone>();
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

	public void Open(bool openNow = false)
	{
		if(!isOpen)
		{
			audio.Stop();
			audio.Play();
			if(openNow)
				MoveDoor();
			else
				StartCoroutine("OpenDoor");
		}
	}

	public void Destroy()
	{
		Destroy(gameObject);
	}

	IEnumerator OpenDoor()
	{
		yield return new WaitForSeconds(2f);
		MoveDoor();
	}

	void MoveDoor()
	{
		startTimeMove = Time.time;
		opening = true;
		phone.SetEndTime(true, timeMoveDoor);
	}
}
