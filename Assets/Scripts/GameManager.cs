using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	private static bool instantiated = false;

	int gamesCount = 0;

	string[] scenesNames = new string[5];

	Dictionary<string, WorldManager> sceneManagers = new Dictionary<string, WorldManager>();

	void Awake () {
		if (!instantiated) {
			DontDestroyOnLoad (this);

			scenesNames [0] = "Smartphone";
			scenesNames [1] = "Monster";
			scenesNames [2] = "Coffe";
			scenesNames [3] = "Couveuse";
			scenesNames [4] = "Jam";

			SceneManager.sceneLoaded += OnSceneWasLoaded;

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
		if (Input.GetKeyUp ("n")) {
			ChangeScene ();
		}
		
	}

	public void ChangeScene(){
		// Disable Scene managers
		SteamVR_Fade.View(Color.black, 2);
		foreach (WorldManager world in sceneManagers.Values) {
			world.gameObject.SetActive(false);
		}

		string sceneToLoad = scenesNames [gamesCount % scenesNames.Length];
		// Load scene
		SceneManager.LoadScene (sceneToLoad, LoadSceneMode.Single);
		// Increment counter
		gamesCount++;
		Debug.Log ("Change scene : " + sceneToLoad + " ; Count : " + gamesCount);

	}

	void OnSceneWasLoaded(Scene scene, LoadSceneMode loadSceneMode){
		SteamVR_Fade.View(Color.clear, 2);
		// Enable right scene manager
		sceneManagers [SceneManager.GetActiveScene().name].gameObject.SetActive(true);
		// Init loaded scene
		sceneManagers [SceneManager.GetActiveScene().name].InitScene ();
	}

	public void RegisterSceneManager(WorldManager world) {
		Debug.Log ("Registered");
		sceneManagers.Add (world.sceneName, world);
	}
}
