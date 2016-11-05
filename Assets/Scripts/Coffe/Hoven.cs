using UnityEngine;
using System.Collections;

public class Hoven : MonoBehaviour {

    public Transform door;

    bool doorOpened, cakePlaced;

    public Cake cake;

    CoffeSceneManager sceneManager;

    public void Initialize(CoffeSceneManager sceneManager)
    {
        this.sceneManager = sceneManager;
        cake.gameObject.SetActive(true);
    }

	public void ToggleDoor()
    {
        /*if (Input.GetMouseButton(0))
        {*/
            doorOpened = !doorOpened;

        if (doorOpened)
        {
            door.Rotate(Vector3.right, 80f);
        }
        else
        {
            if (cakePlaced)
            {
                Invoke("CookCake", 1f);
            }

            door.Rotate(Vector3.right, -80f);
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
