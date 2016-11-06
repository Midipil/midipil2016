using UnityEngine;
using System.Collections;

[RequireComponent(typeof (Collider))]
[RequireComponent(typeof (Rigidbody))]
public class Grabbable : MonoBehaviour {

	public int controllerIndex 
	{ 
		get;
		private set;
	}

	private bool isGrabbed = false;
	[HideInInspector]
	public bool dropAsked = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetGrabbed(bool grabbed, int controller_index = -1)
	{
		isGrabbed = grabbed;
		controllerIndex = controller_index;
	}

	public bool IsGrabbed(){
		return isGrabbed;
	}

	public void AskDrop(){
		dropAsked = true;
	}

}
