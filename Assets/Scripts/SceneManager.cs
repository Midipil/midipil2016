using UnityEngine;
using System.Collections;

public abstract class SceneManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// Init scene and its content
	protected abstract void InitScene();

	// Get from GameManager the previous scene state (last time we entered this scene)
	protected abstract void GetPreviousState();

	// Start end sequence when scene goal is achieved
	public abstract void SetEnd(bool win);

}
