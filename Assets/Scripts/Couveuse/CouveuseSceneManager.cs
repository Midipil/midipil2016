using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CouveuseSceneManager : WorldManager {

	private static bool instantiated = false;

	// All scene states
	public enum SceneState {
		ONE_EGG,
		EASY_LASER,
		FATE,
		TOO_SLOW
	}

	public SceneState currentState;

	private GameObject playerCamera, enemyCamera, playerBot, enemyBot, oneEgg, player, enemy, baseControls, laserControls;

	void Awake () {

		sceneName = "Couveuse";

		if (!instantiated) {
			DontDestroyOnLoad (this);
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
		if(Input.GetKeyUp("p")){
			currentState++;
			SceneManager.LoadScene (sceneName, LoadSceneMode.Single);
			InitScene ();
		}
		if(Input.GetKeyUp("e")){
			SetEnd (false);
		}

	}

	// Init scene and its content
	public override void InitScene(){

		playerBot = GameObject.Find ("Bearbot").gameObject;
		enemyBot = GameObject.Find ("Evil").gameObject;
		oneEgg = GameObject.Find ("OneEgg").gameObject;
		player = GameObject.Find ("Player").gameObject;
		enemy = GameObject.Find ("Enemy").gameObject;
		baseControls = GameObject.Find ("BaseControls").gameObject;
		laserControls = GameObject.Find ("LaserControls").gameObject;
		playerCamera = player.transform.Find ("Anchor/[CameraRigCustom]").gameObject;
		enemyCamera = enemy.transform.Find ("[CameraRigCustom]").gameObject;

		Debug.Log ("Init couveuse scene with state : "+ currentState);
		switch (currentState)
		{
		case SceneState.ONE_EGG:
			playerBot.SetActive (false);
			enemyCamera.SetActive (false);
			enemy.SetActive (false);
			break;

        case SceneState.EASY_LASER:
            playerCamera.SetActive(false);
            enemyBot.SetActive(false);
            oneEgg.SetActive(false);
            baseControls.SetActive(false);
            enemy.GetComponent<EnemyController>().activateLaser(false);
            enemy.transform.Rotate(new Vector3(90, 0, 0));
            break;

        case SceneState.FATE:
            playerBot.SetActive(false);
            enemyCamera.SetActive(false);
            oneEgg.SetActive(false);
            enemy.GetComponent<EnemyController>().sweep=true;

            break;

        case SceneState.TOO_SLOW:
            playerCamera.SetActive(false);
            enemyBot.SetActive(false);
            laserControls.SetActive(false);
		    baseControls.SetActive(false);
            enemy.GetComponent<EnemyController>().activateLaser(true);
            player.GetComponent<PlayerController>().sweep = true;
            enemy.transform.Rotate(new Vector3(100, 0, 0));
            break;

        default:
		break;
		}

	}

	// Start end sequence when scene goal is achieved
	public override void SetEnd(bool win){
		switch (currentState)
		{
		case SceneState.ONE_EGG:
                currentState = SceneState.EASY_LASER;
			break;
        case SceneState.EASY_LASER:
            currentState = SceneState.FATE;
            break;
        case SceneState.FATE:
		    currentState = SceneState.TOO_SLOW;
            break;
        case SceneState.TOO_SLOW:
            currentState = SceneState.ONE_EGG;
            break;

            default:
			break;
		}

		if (win) {
			GameObject.Find ("GameManager").GetComponent<GameManager> ().playWin ();
		} else {
			GameObject.Find ("GameManager").GetComponent<GameManager> ().playLoose ();
		}

		FindObjectOfType<GameManager>().ChangeScene();

	}

}
