using UnityEngine;
using System.Collections;

public class BaseControl : MonoBehaviour {

	public enum Direction {UP, DOWN, LEFT, RIGHT, PLAYER_FORWARD};
	public Direction controlDirection = Direction.LEFT;
	public float pressOffset = 0.01f;
	Transform child;
	EnemyController ec;
	PlayerController pc;

	Vector3 initPos;

	// Use this for initialization
	void Start () {
		child = this.transform.GetChild (0);
		initPos = child.localPosition;
		if (GameObject.Find ("Enemy")) {
			ec = GameObject.Find ("Enemy").GetComponent<EnemyController> ();
		}
		if (GameObject.Find ("Player")) {
			pc = GameObject.Find ("Player").GetComponent<PlayerController> ();
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider o) {
		if (ec) {
			if (controlDirection == Direction.LEFT) {
				ec.setDirection (-1.0f, 0.0f);
			} else if (controlDirection == Direction.RIGHT) {
				ec.setDirection (1.0f, 0.0f);
			} else if (controlDirection == Direction.UP) {
				ec.setDirection (0.0f, 1.0f);
			} else if (controlDirection == Direction.DOWN) {
				ec.setDirection (0.0f, -1.0f);
			}
		}
		child.localPosition = new Vector3 (initPos.x, initPos.y  -  pressOffset, initPos.z);

		if (controlDirection == Direction.PLAYER_FORWARD) {
			pc.setDirection (0.0f, 1.0f);
		}

	} 
	void OnTriggerExit(Collider o) {
		if (ec) {
			ec.setDirection (0.0f, 0.0f);
		}
		if (pc) {
			pc.setDirection (0.0f, 0.0f);
		}
		child.localPosition = initPos;
	} 
}
