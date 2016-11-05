using UnityEngine;
using System.Collections;

public class CoffeSceneManager : WorldManager {

	private static bool instantiated = false;

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
                currentState = SceneState.CAKE_OK;
                FindObjectOfType<Hoven>().Initialize(this);
                break;
            case SceneState.MILK_BOILING:
                break;
            case SceneState.BOILER_HANDLE:
                break;
            case SceneState.BOILER_GRABBABLE:
                break;
            case SceneState.CAKE_OK:
                break;
            case SceneState.CAKE_BURNT:
                break;
            default:
                break;
        }

        FindObjectOfType<EndScreen>().Initialize(this);
	}

	// Start end sequence when scene goal is achieved
	public override void SetEnd(bool win)
    {
        previousState = currentState;
        previousStateWin = win;
    }

}
