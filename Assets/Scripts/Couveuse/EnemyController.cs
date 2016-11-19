using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	float horizontal = 0.0f;
	float vertical = 0.0f;

	float speed = 0.015f;

    bool laserActivated = true;

    public bool sweep = false;

    void Start()
    {
		//activateLaser (false);
    }

    void Update()
    {
        
    }

	// Update is called once per frame
	void FixedUpdate() {

		// Old method
		//transform.Rotate(new Vector3(Input.GetAxis("Enemy_Vertical"), Input.GetAxis("Enemy_Horizontal"), 0));
	
		this.GetComponent<Rigidbody>().AddTorque(transform.up * horizontal * speed);
		this.GetComponent<Rigidbody>().AddTorque(- transform.right * vertical * speed);

        if (sweep)
        {
            this.GetComponent<Rigidbody>().AddTorque(transform.right * 5 * speed);
        }

	}

	public void setDirection(float h, float v){
		horizontal = h;
		vertical = v;
	}

    public void activateLaser(bool b)
    {
		Debug.Log ("LASSER" + b);
        transform.GetComponent<LineRenderer>().enabled=b;
        transform.GetComponent<LaserBeam>().enabled=b;
        transform.Find("Spotlight").gameObject.SetActive(b);
        transform.Find("Spotlight_bis").gameObject.SetActive(b);
    }

}
