using UnityEngine;
using System.Collections;

public class CheckMonsterDeath : MonoBehaviour {

	public MonsterAnimationsController animationController;
	private Killable[] colliders;

	private bool countdownStarted = false;
	private float countdownBeforeEnd = 3.0f;

	// Use this for initialization
	void Start () {
		colliders = gameObject.GetComponentsInChildren<Killable> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (countdownBeforeEnd <= 0) {
			GameObject.FindWithTag ("SceneManager").GetComponent<MonsterSceneManager>().SetEnd(true);
		}

		if (countdownStarted) {
			countdownBeforeEnd -= Time.deltaTime;
		}

		foreach (Killable collider in colliders) {
			if (collider.IsDead ()) {
				animationController.TriggerDeath ();
				countdownStarted = true;
			}
		}

	}
}
