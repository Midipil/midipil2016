using UnityEngine;
using System.Collections;

public class SmartphoneSceneManager : WorldManager {

	private static bool instantiated = false;

	// All scene states
	private enum SceneState {
        NOT_SET,
        OPEN_DOOR,
		MATCH_TINDER
	}

	// State we were last time we played this scene
	private SceneState previousState;
	// Did we win or lose last time we played this scene
	private bool previousStateWin;
	// State we are currently
	private SceneState currentState;

	private GameManager manager;

	private Smartphone phone;
	private Key key;
	private Door door;

	void Awake () {

		sceneName = "Smartphone";

		if (!instantiated) 
		{
			RegisterToGameManager ();
			GameObject obj = GameObject.FindWithTag("GameManager");
			if(obj)
				manager = obj.GetComponent<GameManager>();
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
		SmartphoneHolder holder = GameObject.FindWithTag("Holder").GetComponent<SmartphoneHolder>();
		phone = holder.phone;
		key = holder.key;
		door = holder.door;

		switch (previousState)
        {
        	case SceneState.NOT_SET:
				currentState = SceneState.OPEN_DOOR;
				phone.SetBtnDoor(false);
				key.gameObject.SetActive(true);
                break;
            case SceneState.OPEN_DOOR:
            	if(!previousStateWin)
            	{
            		currentState = SceneState.OPEN_DOOR;
					phone.SetBtnDoor(false);
					key.gameObject.SetActive(true);
            	}
            	else
            	{
            		currentState = SceneState.MATCH_TINDER;
            		key.gameObject.SetActive(false);
            		phone.SetBtnDoor(true);
            	}
                break;
            case SceneState.MATCH_TINDER:
            	key.gameObject.SetActive(false);
            	phone.SetBtnDoor(true);
                break;

            default:
                break;
        }

		Debug.Log ("Init smartphone scene.");
	}

	// Start end sequence when scene goal is achieved
	public override void SetEnd(bool win)
	{
		switch (currentState)
        {
            case SceneState.OPEN_DOOR:
                break;

            case SceneState.MATCH_TINDER:
                break;

            default:
                break;
        }

        previousState = currentState;
		previousStateWin = win;

		manager.ChangeScene();
	}

}
