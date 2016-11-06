using UnityEngine;
using System.Collections;

public class Hoven : MonoBehaviour {

    public Transform door;

	bool doorOpened;
	public bool cakePlaced;

    public Cake cake;

    CoffeSceneManager sceneManager;

    float anim = -1;

	SteamVR_TrackedController controller;

    public void Initialize(CoffeSceneManager sceneManager)
    {
        this.sceneManager = sceneManager;
		cake.Initialize (sceneManager);

		FindObjectOfType<TextDisplay>().DisplayText("Mets le gateau au four !", 10f);
    }

	public void ToggleDoor(SteamVR_TrackedController controller)
    {
		this.controller = controller;

        if (controller.triggerPressed && anim < 0f)
        {
            doorOpened = !doorOpened;

            if (doorOpened)
            {
                //door.Rotate(Vector3.right, 80f);
            }
            else
            {
                if (cakePlaced)
                {
                    cake.Invoke("CookCake", 1f);
                }

                //door.Rotate(Vector3.right, -80f);
            }
            anim = 0f;
        }
    }

    void Update()
    {
        if(anim > -1)
        {
            anim += Time.deltaTime;
            if (doorOpened)
                door.rotation = Quaternion.AngleAxis(Mathf.Lerp(0f, 80f, anim), Vector3.right);
            else
                door.rotation = Quaternion.AngleAxis(Mathf.Lerp(80f, 0f, anim), Vector3.right);
            //door.Rotate(Vector3.right, Mathf.Lerp(80f, 0f, anim));

            if (anim >= 1f)
                anim = -1;
        }

		if (GetComponentInChildren<TriggerStates> ().isTriggered && controller.triggerPressed)
			ToggleDoor (controller);
    }
}
