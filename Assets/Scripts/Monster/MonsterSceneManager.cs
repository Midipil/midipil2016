using UnityEngine;
using System.Collections;

public class MonsterSceneManager : WorldManager {

	private static bool instantiated = false;

	// All scene states
	private enum SceneState {
		NOT_SET,
		KILL_THE_MONSTER,
		KILLED_BY_MONSTER,
		OPEN_CHEST,
		JP_CRAZY_MODE
	}

	// State we were last time we played this scene
	private SceneState previousState = SceneState.NOT_SET;
	// Did we win or lose last time we played this scene
	private bool previousStateWin;
	// State we are currently
	private SceneState currentState = SceneState.NOT_SET;

	private float countdown = 0.0f;

	void Awake () {

		sceneName = "Monster";

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

		countdown -= Time.deltaTime;

		if (countdown <= 0) {
			// Launch monster anim
			if (GameObject.FindWithTag ("Holder").GetComponent<MonsterObjectsHolder> ().animations.gameObject.activeInHierarchy) {
				GameObject.FindWithTag ("Holder").GetComponent<MonsterObjectsHolder> ().animations.TriggerAttack ();
			} else if (GameObject.FindWithTag ("Holder").GetComponent<MonsterObjectsHolder> ().jpAnimations.gameObject.activeInHierarchy) {
				GameObject.FindWithTag ("Holder").GetComponent<MonsterObjectsHolder> ().jpAnimations.TriggerAttack();
			}
		}

	}

	// Init scene and its content
	public override void InitScene(){
		Debug.Log ("Init monster scene.");

		switch (previousState){

		case SceneState.NOT_SET:
			currentState = SceneState.KILL_THE_MONSTER;
			GameObject.FindWithTag ("Holder").GetComponent<MonsterObjectsHolder> ().chest.SetActive (false);
			GameObject.FindWithTag ("Holder").GetComponent<MonsterObjectsHolder> ().sword.GetComponent<Sword> ().unlockable = true;
			countdown = 30; // in seconds
			break;

		case SceneState.KILL_THE_MONSTER:
			currentState = SceneState.KILLED_BY_MONSTER;
			GameObject.FindWithTag ("Holder").GetComponent<MonsterObjectsHolder> ().chest.SetActive (false);
			GameObject.FindWithTag ("Holder").GetComponent<MonsterObjectsHolder> ().sword.GetComponent<Sword> ().unlockable = false;
			countdown = 10;
			break;

		case SceneState.KILLED_BY_MONSTER:
			currentState = SceneState.OPEN_CHEST;
			GameObject.FindWithTag ("Holder").GetComponent<MonsterObjectsHolder> ().sword.SetActive (false);
			GameObject.FindWithTag ("Holder").GetComponent<MonsterObjectsHolder> ().chest.SetActive (true);
			countdown = 30;
			break;

		case SceneState.OPEN_CHEST:
		case SceneState.JP_CRAZY_MODE:
			currentState = SceneState.JP_CRAZY_MODE;
			GameObject.FindWithTag ("Holder").GetComponent<MonsterObjectsHolder> ().chest.SetActive (true);
			GameObject.FindWithTag ("Holder").GetComponent<MonsterObjectsHolder> ().sword.GetComponent<Sword> ().unlockable = true;
			GameObject.FindWithTag ("Holder").GetComponent<MonsterObjectsHolder> ().animations.gameObject.SetActive (false);
			GameObject.FindWithTag ("Holder").GetComponent<MonsterObjectsHolder> ().jpAnimations.gameObject.SetActive (true);
			GameObject.FindWithTag ("Holder").GetComponent<MonsterObjectsHolder> ().chest.GetComponent<Chest> ().coins.gameObject.SetActive (false);
			GameObject.FindWithTag ("Holder").GetComponent<MonsterObjectsHolder> ().chest.GetComponent<Chest> ().JP.gameObject.SetActive (true);
			GameObject.FindWithTag ("Holder").GetComponent<MonsterObjectsHolder> ().chest.gameObject.GetComponentInChildren<LerpRotation> ().delayEnd = 6;
			countdown = 30; // in seconds
			break;

		default:
			GameObject.FindWithTag ("Holder").GetComponent<MonsterObjectsHolder> ().chest.SetActive (true);
			GameObject.FindWithTag ("Holder").GetComponent<MonsterObjectsHolder> ().sword.GetComponent<Sword> ().unlockable = true;
			countdown = 30;
			break;

		}

	}

	// Start end sequence when scene goal is achieved
	public override void SetEnd(bool win){

		switch (currentState){

		default:
			break;
		
		}

		GameObject.FindWithTag ("GameManager").GetComponent<GameManager> ().ChangeScene ();

		if (win) {
			GameObject.Find ("GameManager").GetComponent<GameManager> ().playWin ();
		} else {
			GameObject.Find ("GameManager").GetComponent<GameManager> ().playLoose ();
		}

		previousState = currentState;
		previousStateWin = win;

	}

}
