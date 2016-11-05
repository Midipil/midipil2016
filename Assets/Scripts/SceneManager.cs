using UnityEngine;
using System.Collections;

public abstract class SceneManager : MonoBehaviour {

	// Use this for initialization
	protected void Start () {
		DontDestroyOnLoad (this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// Init scene and its content
	public abstract void InitScene();

	// Start end sequence when scene goal is achieved
	public abstract void SetEnd(bool win);

}
