using UnityEngine;
using System.Collections;

public class CoffeSceneManager : WorldManager {

	private static bool instantiated = false;

    private bool looseHandle = false;

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
	private SceneState previousState = SceneState.NOT_SET;
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

    public bool getLooseHandle()
    {
        return looseHandle;
    }
    public void setLooseHandle(bool b)
    {
        looseHandle = b;
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

        FindObjectOfType<TextDisplay>().Initialize(this);

        switch (previousState)
        {
            case SceneState.NOT_SET:
                currentState = SceneState.MILK_BOILING;
                FindObjectOfType<Boiler>().Initialize(this);
                looseHandle = false;
                break;
            case SceneState.MILK_BOILING:
                currentState = SceneState.CAKE_OK;
                
                FindObjectOfType<Hoven>().Initialize(this);
                FindObjectOfType<Boiler>().gameObject.SetActive(false);
                break;
            case SceneState.CAKE_OK:
                currentState = SceneState.BOILER_HANDLE;
                // poignée détachable
                looseHandle = true;
                FindObjectOfType<Boiler>().Initialize(this);
                break;
            case SceneState.BOILER_HANDLE:
                currentState = SceneState.CAKE_BURNT;
                FindObjectOfType<Hoven>().Initialize(this);
                FindObjectOfType<Boiler>().gameObject.SetActive(false);
                break;
			case SceneState.CAKE_BURNT:
				currentState = SceneState.BOILER_GRABBABLE;
                // poignée normale
                looseHandle = false;
                FindObjectOfType<Boiler>().Initialize(this);
                break;
            case SceneState.BOILER_GRABBABLE:
                break;
            default:
                break;
        }
	}

	// Start end sequence when scene goal is achieved
	public override void SetEnd(bool win)
    {
        switch (currentState)
        {

            default:
                break;

        }

        GameObject.FindWithTag("GameManager").GetComponent<GameManager>().ChangeScene();

        if (win)
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().playWin();
        }
        else {
            GameObject.Find("GameManager").GetComponent<GameManager>().playLoose();
        }

        previousState = currentState;
        previousStateWin = win;
    }

}
