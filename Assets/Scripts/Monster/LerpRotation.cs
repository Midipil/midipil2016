﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof (Collider))]
public class LerpRotation : MonoBehaviour {

	public GameObject target;
	public AudioSource sfx;
	public Vector3 beginRotation;
	public Vector3 endRotation;

	public float speed = 1.0f;
	private float startTime;
	private float journeyLength;
	private bool animStarted = false;

	public float delayEnd = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (animStarted) {
			float distCovered = (Time.time - startTime) * speed;
			float fracJourney = distCovered / journeyLength;
			target.transform.localRotation = Quaternion.Euler(Vector3.Lerp (beginRotation, endRotation, fracJourney));
			if (fracJourney >= 1) {
				if (delayEnd < 0) {
					//animStarted = false;
					GameObject.FindWithTag ("SceneManager").GetComponent<MonsterSceneManager> ().SetEnd (true);
				}
				delayEnd -= Time.deltaTime;
			}
		}

	}

	void OnTriggerEnter() {
		if (!animStarted) {
			animStarted = true;
			sfx.Play ();
			startTime = Time.time;
			journeyLength = Vector3.Distance (beginRotation, endRotation);
		}
	}

}
