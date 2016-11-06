using UnityEngine;
using System.Collections;

public class MonsterAnimationsController : MonoBehaviour {

	public Animator animator;

	private float timeBeforeRoar = 7.0f;

	private bool countdownStarted = false;
	private float countdownBeforeEnd = 4.0f;
	private bool win = false;
	private bool deathHasBeenPlayed = false;

	public AudioSource idleSound;
	public AudioSource roarSound;
	public AudioSource attackSound;
	public AudioSource whooshSound;
	public AudioSource deathSound;

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


		// Play sounds
		if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle breathing") && !idleSound.isPlaying) {
			idleSound.Play ();
			roarSound.Stop ();
			attackSound.Stop ();
			whooshSound.Stop ();
			deathSound.Stop ();
		}
		else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Roaring") && !roarSound.isPlaying) {
			idleSound.Stop ();
			roarSound.Play ();
			attackSound.Stop ();
			whooshSound.Stop ();
			deathSound.Stop ();
		}
		else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack") && !attackSound.isPlaying) {
			idleSound.Stop ();
			roarSound.Stop ();
			attackSound.Play ();
			whooshSound.PlayDelayed (1.5f);
			deathSound.Stop ();
		}
		else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Death") && !deathSound.isPlaying && !deathHasBeenPlayed) {
			idleSound.Stop ();
			roarSound.Stop ();
			attackSound.Stop ();
			whooshSound.Stop ();
			deathSound.Play ();
			deathHasBeenPlayed = true;
		}


	}

	public void TriggerAttack(){
		countdownStarted = true;
		win = false;
		GameObject.FindWithTag ("Holder").GetComponent<MonsterObjectsHolder> ().sword.GetComponent<Lethal> ().enabled = false;
		
		if (!animator.GetCurrentAnimatorStateInfo (0).IsName ("Attack")) {
			animator.SetTrigger ("TriggerAttack");
		}
	}

	public void TriggerDeath(){
		if (!animator.GetCurrentAnimatorStateInfo (0).IsName ("Death")) {
			animator.SetTrigger ("TriggerDeath");
			animator.SetBool ("IsDead", true);
			countdownBeforeEnd = 6.0f;
			countdownStarted = true;
			win = true;
		}
	}

}
