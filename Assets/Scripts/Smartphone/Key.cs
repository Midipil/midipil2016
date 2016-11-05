using UnityEngine;
using System.Collections;

public class Key : MonoBehaviour {

	public Rigidbody rb;
	public float rotationTime;

	private float timeStartRotate;
	private bool rotation;

	void Start()
	{
		rotation = false;
	}
	
	void OnSnap()
	{
		rb.useGravity = false;
		rb.isKinematic = true;
		timeStartRotate = Time.time;
		rotation = true;
	}

	void Update()
	{
		if(rotation)
		{
			float t = (Time.time - timeStartRotate) / rotationTime;

			if(t <= 1f)
			{
				transform.rotation = Quaternion.Euler(0, 0, t * 360f);
			}
			else
			{
				rotation = false;
			}
		}
	}
}
