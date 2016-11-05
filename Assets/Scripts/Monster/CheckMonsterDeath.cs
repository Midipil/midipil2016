using UnityEngine;
using System.Collections;

public class CheckMonsterDeath : MonoBehaviour {

	public MonsterAnimationsController animationController;
	private Killable[] colliders;

	// Use this for initialization
	void Start () {
		colliders = gameObject.GetComponentsInChildren<Killable> ();
	}
	
	// Update is called once per frame
	void Update () {

		foreach (Killable collider in colliders) {
			if (collider.IsDead ()) {
				animationController.TriggerDeath ();
			}
		}

	}
}
