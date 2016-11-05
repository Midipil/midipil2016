using UnityEngine;
using System.Collections;

public class MonsterAnimationsController : MonoBehaviour {

	public Animator animator;

	private float timeBeforeRoar = 7.0f;

	private bool countdownStarted = false;
	private float countdownBeforeEnd = 2.0f;
	private bool win = false;

	// Use this for initialization
	void Start () {
		if (!animator.isInitialized) {
			animator.Rebind ();
		}

		animator.SetBool ("IsDead", false);
		animator.SetFloat ("TimeBeforeRoar", timeBeforeRoar);
	}
	
	// Update is called once per frame
	void Update () {
		animator.SetFloat ("TimeBeforeRoar", animator.GetFloat ("TimeBeforeRoar") - Time.deltaTime);
		if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Idle breathing")) {
			animator.SetFloat ("TimeBeforeRoar", timeBeforeRoar);
		}

		if (countdownStarted) {
			countdownBeforeEnd -= Time.deltaTime;
		}

		if (countdownBeforeEnd <= 0) {
			GameObject.FindWithTag ("SceneManager").GetComponent<MonsterSceneManager>().SetEnd(win);
		}

	}

	public void TriggerAttack(){
		animator.SetTrigger ("TriggerAttack");
		countdownStarted = true;
		win = false;
	}

	public void TriggerDeath(){
		animator.SetBool ("IsDead", true);
		animator.SetTrigger ("TriggerDeath");
		countdownStarted = true;
		win = true;
	}

}
