using UnityEngine;
using System.Collections;

public class Hoven : MonoBehaviour {

    public Transform door;

    bool doorOpened, cakePlaced;

    public Cake cake;

    CoffeSceneManager sceneManager;

    float anim = -1;

    public void Initialize(CoffeSceneManager sceneManager)
    {
        this.sceneManager = sceneManager;
        cake.gameObject.SetActive(true);
    }

	public void ToggleDoor(/*SteamVR_TrackedController controller*/)
    {
        if (/*controller.triggerPressed && */anim < 0f)
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
                    Invoke("CookCake", 1f);
                }

                //door.Rotate(Vector3.right, -80f);
            }
            anim = 0f;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Cake")
        {
            other.transform.parent = transform;
            other.transform.localPosition = Vector3.zero;
            other.transform.localRotation = Quaternion.identity;
            cakePlaced = true;
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
    }

    void CookCake()
    {
        GetComponent<AudioSource>().Play();

        if (sceneManager.currentState == CoffeSceneManager.SceneState.CAKE_OK)
        {
            cake.cakeRaw.SetActive(false);
            cake.cakeOk.SetActive(true);

            Invoke("CakeBakedOk", 1f);
        }
        else if (sceneManager.currentState == CoffeSceneManager.SceneState.CAKE_BURNT)
        {
            cake.cakeRaw.SetActive(false);
            cake.cakeBurnt.SetActive(true);

            Invoke("CakeBakedOk", 1f);
        }
    }

    void CakeBakedOk()
    {
        FindObjectOfType<TextDisplay>().ShowEndScreen("Tu as réussis à cuire le gateau !\nJean-Pierre Coffe est fière de toi !", true);
    }

    void CakeBurnt()
    {
        FindObjectOfType<TextDisplay>().ShowEndScreen("Tu as cramé le gateau !\nJean-Pierre Coffe n'est pas content du tout !", false);
    }
}
