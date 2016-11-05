using UnityEngine;
using System.Collections;

public class CouveuseSceneManager : WorldManager {

	private static bool instantiated = false;

	// All scene states
	private enum SceneState {
		ONE_EGG,
		EASY_LASER,
		MANY_EGGS,
		CAT_LASER,
		BROKEN_WHEEL
	}

	SceneState currentState;
	SceneState previousState;

	void Awake () {

		sceneName = "Couveuse";

		if (!instantiated) {
			RegisterToGameManager ();
			instantiated = true;
		} else {
			Destroy (this.gameObject);
		}

	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// Init scene and its content
	public override void InitScene(){
		Debug.Log ("Init couveuse scene.");

	}

	// Start end sequence when scene goal is achieved
	public override void SetEnd(bool win){

		switch (currentState)
		{
		case SceneState.ONE_EGG:
			break;


		default:
			break;
		}

		previousState = currentState;

		FindObjectOfType<GameManager>().ChangeScene();

	}

}
