using UnityEngine;
using System.Collections;

public class CouveuseSceneManager : SceneManager {

	// Use this for initialization
	void Start () {
		// Call base class Start method
		base.Start ();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// Init scene and its content
	public override void InitScene(){

	}

	// Get from GameManager the previous scene state (last time we entered this scene)
	protected override void GetPreviousState(){

	}

	// Start end sequence when scene goal is achieved
	public override void SetEnd(bool win){

	}

}
