using UnityEngine;
using System.Collections;

public class Lock : MonoBehaviour 
{
	public Door door;

	void OnSnap()
	{
		door.Open();
	}
}
