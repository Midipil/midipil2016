using UnityEngine;
using System.Collections;

public class Lock : MonoBehaviour 
{
	public Door door;
	public Transform anchor;

	void OnSnap()
	{
		Transform key = transform.Find("Key");
		if(key)
			key.position = anchor.position;
		door.Open();
	}
}
