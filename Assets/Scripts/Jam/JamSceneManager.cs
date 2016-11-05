using UnityEngine;
using System.Collections;

public class JamSceneManager : WorldManager {

	private static bool instantiated = false;

	// All scene states
	private enum SceneState {
        NOT_SET,
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
	private SceneState currentState = SceneState.DO_SOME_PAINT;

    JamTimer timer;
    CodePeeing codePeeing;
    MouseDrawing mouseDrawing;

	void Awake () {

		sceneName = "Jam";

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
	public override void InitScene()
    {
        timer = FindObjectOfType<JamTimer>();
        codePeeing = FindObjectOfType<CodePeeing>();
        mouseDrawing = FindObjectOfType<MouseDrawing>();

        Debug.Log ("Init jam scene.");

        switch (previousState)
        {
            case SceneState.NOT_SET:
                currentState = SceneState.DO_SOME_CODE;
                codePeeing.Initialize();
                break;

            case SceneState.DO_SOME_CODE:
                currentState = SceneState.DO_SOME_PAINT;
                mouseDrawing.Initialize();
                break;

            case SceneState.DO_SOME_PAINT:
                currentState = SceneState.DO_SOME_CODE;
                codePeeing.Initialize();
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

        FindObjectOfType<TextDisplay>().Initialize(this);
        timer.Initialize(this);
    }

	// Start end sequence when scene goal is achieved
	public override void SetEnd(bool win){

        switch (currentState)
        {
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

        previousState = currentState;
		previousStateWin = win;

        FindObjectOfType<GameManager>().ChangeScene();

    }

}
