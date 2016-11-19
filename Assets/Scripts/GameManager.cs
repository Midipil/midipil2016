using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	private static bool instantiated = false;

	public AudioClip winSound;
	public AudioClip looseSound;

	int gamesCount = 1;
	int maxBeforeCredits = 25;

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

		if (Input.GetKeyUp ("c")) {
			LoadCredits ();
		}
		
	}

	public void ChangeScene(){
		if(gamesCount > maxBeforeCredits){
			LoadCredits();
		} else {

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
		
	}

	private void LoadCredits(){
		SceneManager.LoadScene ("Credits", LoadSceneMode.Single);
	}

	void OnSceneWasLoaded(Scene scene, LoadSceneMode loadSceneMode){
		SteamVR_Fade.View(Color.clear, 2);
		// For safety, double check
		foreach (WorldManager world in sceneManagers.Values) {
			world.gameObject.SetActive(false);
		}
		// Enable right scene manager
		sceneManagers [SceneManager.GetActiveScene().name].gameObject.SetActive(true);
		// Init loaded scene
		sceneManagers [SceneManager.GetActiveScene().name].InitScene ();
	}

	public void RegisterSceneManager(WorldManager world) {
		Debug.Log ("Registered");
		sceneManagers.Add (world.sceneName, world);
	}

	public void playWin(){
		GetComponent<AudioSource> ().clip = winSound;
		GetComponent<AudioSource> ().Play ();
	}

	public void playLoose(){
		GetComponent<AudioSource> ().clip = looseSound;
		GetComponent<AudioSource> ().Play ();
	}
}
