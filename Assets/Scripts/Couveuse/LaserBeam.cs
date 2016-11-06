using UnityEngine;
using System.Collections;

public class LaserBeam : MonoBehaviour
{
    LineRenderer line;

    public float rayLength = 100;
    public float minRadiusRayCast = 0;
    public float laserDamages = 1;
    public GameObject impactFX;
    public int timeToKill = 3;
    public float currentTimeHit;
	float initSpotAngle;

	float deathTime = 5.0f;


    void Start()
    {
        line = gameObject.GetComponent<LineRenderer>();
		currentTimeHit = timeToKill;
		initSpotAngle = this.transform.Find ("Spotlight").GetComponent<Light> ().spotAngle;
    }

    void Update()
    {

        if (currentTimeHit <= 0){
			//GameObject.FindWithTag("GameManager").GetComponent<GameManager>().enemyWin = true;
			//Debug.Log("Enemy win");
		}

        Ray ray = new Ray(transform.position + minRadiusRayCast * transform.forward, transform.forward);
        RaycastHit hit;

        //line.SetPosition(0, transform.position);

        if (Physics.Raycast(ray, out hit, rayLength))
        {
            //line.SetPosition(1, hit.point);
            if (hit.transform.parent.tag == "Player")
            {
                //hit.transform.GetComponent<Player>().life -= laserDamages;
                //TimerKill();
				currentTimeHit -= Time.deltaTime;
				AudioSource source;
				source = hit.transform.gameObject.GetComponent<AudioSource>();
				if (!source.isPlaying){
					hit.transform.gameObject.GetComponent<AudioSource>().Play();
				}
				this.transform.Find ("Spotlight").GetComponent<Light> ().spotAngle = 8;

               // impactFX.transform.position = hit.point;
                impactFX.SetActive(true);

				Invoke ("End", deathTime);
            }
        }
        else
        {
			this.transform.Find ("Spotlight").GetComponent<Light> ().spotAngle = initSpotAngle;
            line.SetPosition(1, ray.GetPoint(rayLength));
            //impactFX.SetActive(false);
			currentTimeHit = timeToKill;
        }
    }

	void End(){
       if( FindObjectOfType<CouveuseSceneManager>().currentState == CouveuseSceneManager.SceneState.EASY_LASER)
        {
            FindObjectOfType<CouveuseSceneManager>().SetEnd(true);
        } else if (FindObjectOfType<CouveuseSceneManager>().currentState == CouveuseSceneManager.SceneState.FATE)
        {
            FindObjectOfType<CouveuseSceneManager>().SetEnd(false);
        }
        
	}
}