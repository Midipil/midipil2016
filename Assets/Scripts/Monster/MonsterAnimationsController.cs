using UnityEngine;
using System.Collections;

public class MonsterAnimationsController : MonoBehaviour {

	public Animator animator;

	private float timeBeforeRoar = 7.0f;

	private bool countdownStarted = false;
	private float countdownBeforeEnd = 8.0f;
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
		if (!animator.GetCurrentAnimatorStateInfo (0).IsName ("Attack")) {
			animator.SetTrigger ("TriggerAttack");
			countdownStarted = true;
			win = false;
			GameObject.FindWithTag ("Holder").GetComponent<MonsterObjectsHolder> ().sword.GetComponent<Lethal> ().enabled = false;
		}
	}

	public void TriggerDeath(){
		if (!animator.GetCurrentAnimatorStateInfo (0).IsName ("Death")) {
			animator.SetTrigger ("TriggerDeath");
			animator.SetBool ("IsDead", true);
			countdownStarted = true;
			win = true;
		}
	}

}
