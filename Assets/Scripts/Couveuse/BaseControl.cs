using UnityEngine;
using System.Collections;

public class BaseControl : MonoBehaviour {

	public enum Direction {UP, DOWN, LEFT, RIGHT};
	public Direction controlDirection = Direction.LEFT;
	public float pressOffset = 0.01f;
	Transform child;
	EnemyController ec;

	// Use this for initialization
	void Start () {
		child = this.transform.GetChild (0);
		child.localPosition = this.transform.localPosition;
		ec = GameObject.Find ("Enemy").GetComponent<EnemyController> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider o) {
		if (controlDirection == Direction.LEFT) {
			ec.setDirection (-1.0f, 0.0f);
		} else if (controlDirection == Direction.RIGHT) {
			ec.setDirection (1.0f, 0.0f);
		} else if (controlDirection == Direction.UP) {
			ec.setDirection (0.0f, 1.0f);
		} else if (controlDirection == Direction.DOWN) {
			ec.setDirection (0.0f, -1.0f);
		}
		child.localPosition = new Vector3 (this.transform.localPosition.x, this.transform.localPosition.y  -  pressOffset, this.transform.localPosition.z);

	} 
	void OnTriggerExit(Collider o) {
		ec.setDirection (0.0f, 0.0f);

		child.localPosition = this.transform.localPosition;
	} 
}
