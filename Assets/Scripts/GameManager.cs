using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	int gamesCount = 0;

	string[] scenesNames = new string[5];

	// Use this for initialization
	void Start () {
		if (GameObject.FindGameObjectsWithTag ("GameManager").Length > 1) {
			Destroy (this);
		} else {
			DontDestroyOnLoad(this);

			scenesNames[0] = "Smartphone";
			scenesNames[1] = "Monster";
			scenesNames[2] = "Coffe";
			scenesNames[3] = "Couveuse";
			scenesNames[4] = "Jam";
		}


	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp ("n")) {
			ChangeScene ();
		}
		
	}

	void ChangeScene(){
		SceneManager.LoadScene (scenesNames[gamesCount%scenesNames.Length], LoadSceneMode.Single);
		gamesCount++;
		Debug.Log ("Change scene : " + scenesNames [gamesCount % scenesNames.Length]);
	}
}
