using UnityEngine;
using System.Collections;

public class JamSceneManager : SceneManager {

	// All scene states
	private enum SceneState {
		DO_SOME_CODE,
		DO_SOME_PAINT,
		PUT_THE_VIVE,
		DRINK_COFFEE,
		WATCH_THE_CLOCK
	}

	// State we were last time we played this scene
	private SceneState previousState;
	// Did we win or lose last time we played this scene
	private bool previousStateWin;
	// State we are currently
	private SceneState currentState;

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

		switch (previousState) {
		case SceneState.DO_SOME_CODE:
			break;

		case SceneState.DO_SOME_PAINT:
			break;

		case SceneState.PUT_THE_VIVE:
			break;

		case SceneState.DRINK_COFFEE:
			break;

		case SceneState.WATCH_THE_CLOCK:
			break;

		default:
			break;

		}

	}

	// Start end sequence when scene goal is achieved
	public override void SetEnd(bool win){

		// Save current state as previous one
		previousState = currentState;
		previousStateWin = win;

	}

}
