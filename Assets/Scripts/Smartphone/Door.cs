using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour 
{
	public AudioSource audioDoor;
	public float timeMoveDoor; // Time to open the door
    public float timeMoveHandle;
    public GameObject handle;

	private bool opening = false, isOpen = false;
	private float startTimeMove;
	private Smartphone phone;

	void Start () 
	{
		opening = false;
		isOpen = false;	
		phone = GameObject.Find("Smartphone").GetComponent<Smartphone>();
	}

	void Update()
	{
		if(!isOpen && opening)
		{
            float tHandle = (Time.time - startTimeMove) / timeMoveHandle;

            if(tHandle <= 1f)
            {
                if(tHandle <= 0.5f)
                {
                    handle.transform.localRotation = Quaternion.Euler(-(tHandle * 2f * 45f), 0f, 0f);
                }
                else
                {
                    handle.transform.localRotation = Quaternion.Euler((1f - ((tHandle - 0.5f) * 2f)) * -45f, 0f, 0f);
                }
            }
            else
            {
                float t = (Time.time - (startTimeMove + timeMoveHandle + 0.2f)) / timeMoveDoor;

                if(t >= 0f)
                {
                    if (t <= 1f)
                    {
                        transform.rotation = Quaternion.Euler(0f, t * 90f, 0f);
                    }
                    else
                    {
                        isOpen = true;
                        opening = false;
                    }
                }
            }
		}
	}

	public void Open(bool openNow = false)
	{
		if(!isOpen)
		{
            phone.SetDrainBattery(false);
			audioDoor.Stop();
			audioDoor.Play();
			if(openNow)
				MoveDoor();
			else
				StartCoroutine("OpenDoor");
		}
	}

	public void Destroy()
	{
		Destroy(gameObject);
	}

	IEnumerator OpenDoor()
	{
		yield return new WaitForSeconds(2f);
		MoveDoor();
	}

	void MoveDoor()
	{
        startTimeMove = Time.time;
		opening = true;
        if(phone)
		    phone.SetEndTime(true, timeMoveDoor + timeMoveHandle);
	}
}
