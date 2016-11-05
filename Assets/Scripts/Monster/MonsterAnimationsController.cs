using UnityEngine;
using System.Collections;

public class MonsterAnimationsController : MonoBehaviour {

	public Animator animator;

	private float timeBeforeRoar = 7.0f;

	// Use this for initialization
	void Start () {
		animator.SetFloat ("TimeBeforeRoar", timeBeforeRoar);
	}
	
	// Update is called once per frame
	void Update () {
		animator.SetFloat ("TimeBeforeRoar", animator.GetFloat ("TimeBeforeRoar") - Time.deltaTime);
		if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Idle breathing")) {
			animator.SetFloat ("TimeBeforeRoar", timeBeforeRoar);
		}
	}

	public void TriggerAttack(){
		animator.SetTrigger ("TriggerAttack");
	}

	public void TriggerDeath(){
		animator.SetTrigger ("TriggerDeath");
	}

}
