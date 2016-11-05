using UnityEngine;
using System.Collections;

public class Snapable : MonoBehaviour 
{
	public GameObject target;
	public Vector3 rotationError;
	public Vector3 positionError;

	public bool autoSetSnap; // Auto set position and rotation when ok
	public bool autoSetChild; // Auto set gameObject as a child of target

	private bool rotOK, posOK;
	private bool isSnaped;

	void Start () 
	{
		isSnaped = false;
	}
	
	void Update () 
	{
		if(target)
		{
			rotOK = Vector3.Angle(transform.right, target.transform.right) <= rotationError.x &&
			Vector3.Angle(transform.up, target.transform.up) <= rotationError.y &&
			Vector3.Angle(transform.forward, target.transform.forward) <= rotationError.z;

			Vector3 distDiff = target.transform.position - transform.position;

			posOK = Mathf.Abs(distDiff.x) <= positionError.x &&
				Mathf.Abs(distDiff.y) <= positionError.y &&
				Mathf.Abs(distDiff.z) <= positionError.z;

			if(!isSnaped && posOK && rotOK)
			{
				if(autoSetChild)
				{
					gameObject.transform.parent = target.transform;
				}

				if(autoSetSnap)
				{
					transform.rotation = target.transform.rotation;
					transform.position = target.transform.position;
				}

				target.SendMessage("OnSnap");
				gameObject.SendMessage("OnSnap");

				isSnaped = true;
			}
		}
	}
}
