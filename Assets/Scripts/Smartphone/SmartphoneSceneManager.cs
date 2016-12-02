using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SmartphoneSceneManager : WorldManager
{
    public Sprite[] imagesJP;

	private static bool instantiated = false;

	// All scene states
	private enum SceneState {
        NOT_SET,
        OPEN_DOOR,
        OPEN_DOOR_BTN,
		MATCH_TINDER,
        MATCH_TINDER_JP
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
    private Baby cradle;
	private GameObject cameraRig;
	private Transform spawnInsideHouse;

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
        cradle = holder.cradle;
		cameraRig = holder.cameraRig;
		spawnInsideHouse = holder.spawnInsideHouse;

        previousState = SceneState.MATCH_TINDER_JP;

        switch (previousState)
        {
        	case SceneState.NOT_SET:
				currentState = SceneState.OPEN_DOOR;
				phone.SetBtnDoor(false);
				key.gameObject.SetActive(true);
                break;
            case SceneState.OPEN_DOOR:
            	currentState = SceneState.OPEN_DOOR_BTN;
            	key.gameObject.SetActive(false);
           	    phone.ShowTinder(false);
           		phone.SetBtnDoor(true);
                break;
			case SceneState.OPEN_DOOR_BTN:
				currentState = SceneState.MATCH_TINDER;
				cameraRig.transform.position = spawnInsideHouse.position;
				cameraRig.transform.rotation = spawnInsideHouse.rotation;
            	key.gameObject.SetActive(false);
            	phone.SetBtnDoor(false);
            	phone.ShowTinder(true);
            	door.gameObject.SetActive(false);
                phone.SetGeorge();
                break;

            case SceneState.MATCH_TINDER:
                currentState = SceneState.MATCH_TINDER_JP;
				cameraRig.transform.position = spawnInsideHouse.position;
				cameraRig.transform.rotation = spawnInsideHouse.rotation;
                key.gameObject.SetActive(false);
            	phone.SetBtnDoor(false);
            	phone.ShowTinder(true);
                phone.SetTinderImages(imagesJP);
                door.gameObject.SetActive(false);
                cradle.SetBabyJP();
                break;
			case SceneState.MATCH_TINDER_JP:
				currentState = SceneState.MATCH_TINDER_JP;
				cameraRig.transform.position = spawnInsideHouse.position;
				cameraRig.transform.rotation = spawnInsideHouse.rotation;
				key.gameObject.SetActive (false);
				phone.SetBtnDoor (false);
				phone.ShowTinder (true);
				phone.SetOneTinderImage (imagesJP [2], 1);
				phone.SetMultiTinder ();
				phone.SetGeorgeFirst ();
				phone.SetGeorge ();
                door.gameObject.SetActive(false);
                break;
            default:
                break;
        }
			
		phone.transform.SetParent(null);

		Debug.Log ("Init smartphone scene.");
	}

	// Start end sequence when scene goal is achieved
	public override void SetEnd(bool win)
	{
		switch (currentState)
        {
            case SceneState.OPEN_DOOR:
                break;

            case SceneState.OPEN_DOOR_BTN:
                break;

            case SceneState.MATCH_TINDER:
                break;

            default:
                break;
        }

		if (win) {
			GameObject.Find ("GameManager").GetComponent<GameManager> ().playWin ();
		} else {
			GameObject.Find ("GameManager").GetComponent<GameManager> ().playLoose ();
		}

        if(win)
            previousState = currentState;
		previousStateWin = win;

		manager.ChangeScene();
	}

}
