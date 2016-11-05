using UnityEngine;
using System.Collections;

public abstract class WorldManager : MonoBehaviour {

	public string sceneName = "undefined";

	protected void RegisterToGameManager () {
		DontDestroyOnLoad (this.gameObject);

		// Register to GameManager
		GameObject gameManagerGO = GameObject.FindWithTag("GameManager");
		if (gameManagerGO != null) {
			GameManager gameManager = gameManagerGO.GetComponent<GameManager> ();
			gameManager.RegisterSceneManager (this);
		} else {
			Debug.LogError ("No game object tagged GameManager found");
		}

	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// Init scene and its content
	public abstract void InitScene();

	// Start end sequence when scene goal is achieved
	public abstract void SetEnd(bool win);

}
