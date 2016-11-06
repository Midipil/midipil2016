using UnityEngine;
using System.Collections;

public class MonsterSceneManager : WorldManager {

	private static bool instantiated = false;

	// All scene states
	private enum SceneState {
		NOT_SET,
		KILL_THE_MONSTER,
		KILLED_BY_MONSTER,
		OPEN_CHEST
	}

	// State we were last time we played this scene
	private SceneState previousState;
	// Did we win or lose last time we played this scene
	private bool previousStateWin;
	// State we are currently
	private SceneState currentState;

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
			GameObject.FindWithTag ("Holder").GetComponent<MonsterObjectsHolder> ().animations.TriggerAttack();
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

		previousState = currentState;
		previousStateWin = win;

	}

}
