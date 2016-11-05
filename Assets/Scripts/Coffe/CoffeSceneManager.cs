using UnityEngine;
using System.Collections;

public class CoffeSceneManager : WorldManager {

	private static bool instantiated = false;

    public bool handleTaken;

    // All scene states
    public enum SceneState
    {
        NOT_SET,
        MILK_BOILING,
        BOILER_HANDLE,
        BOILER_GRABBABLE,
        CAKE_OK,
        CAKE_BURNT
    }

    // State we were last time we played this scene
    private SceneState previousState;
    // Did we win or lose last time we played this scene
    private bool previousStateWin;
    // State we are currently
    public SceneState currentState;

    void Awake () {

		sceneName = "Coffe";

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
		Debug.Log ("Init coffe scene.");

        switch (previousState)
        {
            case SceneState.NOT_SET:
                currentState = SceneState.MILK_BOILING;
                FindObjectOfType<Boiler>().Initialize(this);
                break;
            case SceneState.MILK_BOILING:
                currentState = SceneState.CAKE_OK;
                FindObjectOfType<Hoven>().Initialize(this);
                break;
            case SceneState.CAKE_OK:
                currentState = SceneState.BOILER_HANDLE;
                FindObjectOfType<Boiler>().Initialize(this);
                break;
            case SceneState.BOILER_HANDLE:
                currentState = SceneState.CAKE_BURNT;
                FindObjectOfType<Boiler>().Initialize(this);
                break;
            case SceneState.CAKE_BURNT:
                currentState = SceneState.BOILER_GRABBABLE;
                FindObjectOfType<Boiler>().Initialize(this);
                break;
            case SceneState.BOILER_GRABBABLE:
                break;
            default:
                break;
        }

        FindObjectOfType<TextDisplay>().Initialize(this);
	}

	// Start end sequence when scene goal is achieved
	public override void SetEnd(bool win)
    {
        previousState = currentState;
        previousStateWin = win;
    }

}
