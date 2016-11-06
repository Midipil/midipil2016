﻿using UnityEngine;
using System.Collections;

public class CouveuseSceneManager : WorldManager {

	private static bool instantiated = false;

	// All scene states
	private enum SceneState {
		ONE_EGG,
		EASY_LASER,
		FATE,
		CAT_LASER,
		BROKEN_WHEEL
	}

	SceneState currentState = SceneState.FATE;
	SceneState previousState;

	public GameObject playerCamera, enemyCamera, playerBot, enemyBot, oneEgg, enemy, baseControls, laserControls;

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
			playerCamera.SetActive (true);
			playerBot.SetActive (false);
			enemyCamera.SetActive (false);
			enemyBot.SetActive (true);
			oneEgg.SetActive (true);
			enemy.SetActive (false);
			break;

        case SceneState.EASY_LASER:
            playerCamera.SetActive(false);
            playerBot.SetActive(true);
            enemyCamera.SetActive(true);
            enemyBot.SetActive(false);
            oneEgg.SetActive(false);
            enemy.SetActive(true);
            baseControls.SetActive(false);
            laserControls.SetActive(true);
            enemy.GetComponent<EnemyController>().activateLaser(false);
            enemy.transform.Rotate(new Vector3(90, 0, 0));
            break;

        case SceneState.FATE:
            playerCamera.SetActive(true);
            playerBot.SetActive(false);
            enemyCamera.SetActive(false);
            enemyBot.SetActive(true);
            oneEgg.SetActive(false);
            enemy.SetActive(true);
            enemy.GetComponent<EnemyController>().sweep=true;

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
