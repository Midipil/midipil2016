using UnityEngine;
using System.Collections;

public class Baby : MonoBehaviour 
{
	public GameObject fxSpawn;
	public Transform anchorFx;

	// Use this for initialization
	void Start () 
	{
		Instantiate(fxSpawn, anchorFx.position, Quaternion.identity);
	}
}
