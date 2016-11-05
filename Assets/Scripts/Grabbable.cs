﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof (Collider))]
[RequireComponent(typeof (Rigidbody))]
public class Grabbable : MonoBehaviour {

	private bool isGrabbed = false;
	[HideInInspector]
	public bool dropAsked = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetGrabbed(bool grabbed){
		isGrabbed = grabbed;
	}

	public bool IsGrabbed(){
		return isGrabbed;
	}

	public void AskDrop(){
		dropAsked = true;
	}

}
