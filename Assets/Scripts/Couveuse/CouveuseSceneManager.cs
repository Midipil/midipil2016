using UnityEngine;
using System.Collections;

public class CouveuseSceneManager : WorldManager {

	private static bool instantiated = false;

	// All scene states
	private enum SceneState {
		ONE_EGG,
		EASY_LASER,
		MANY_EGGS,
		CAT_LASER,
		BROKEN_WHEEL
	}

	SceneState currentState = SceneState.ONE_EGG;
	SceneState previousState = SceneState.ONE_EGG;

	public GameObject playerCamera, enemyCamera, playerBot, enemyBot, oneEgg, manyEggs, enemy;

	void Awake () {

		sceneName = "Couveuse";

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
		Debug.Log ("Init couveuse scene.");
		switch (currentState)
		{
		case SceneState.ONE_EGG:
			Debug.Log ("smlkfvnmé");
			playerCamera.SetActive (true);
			playerBot.SetActive (false);
			enemyCamera.SetActive (false);
			enemyBot.SetActive (true);
			oneEgg.SetActive (true);
			manyEggs.SetActive (false);
			enemy.SetActive (false);
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
			break;


		default:
			break;
		}

		previousState = currentState;

		FindObjectOfType<GameManager>().ChangeScene();

	}

}
