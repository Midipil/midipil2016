using UnityEngine;
using System.Collections;

public class Cake : MonoBehaviour
{
    public GameObject cakeRaw, cakeOk, cakeBurnt;

	CoffeSceneManager sceneManager;

	public void Initialize(CoffeSceneManager sceneManager)
	{
		gameObject.SetActive(true);
		this.sceneManager = sceneManager;

        cakeRaw.SetActive(true);
        cakeOk.SetActive(false);
        cakeBurnt.SetActive(false);

		FindObjectOfType<Grabber>().Grab(gameObject);
    }

	public void CookCake()
	{
		GetComponent<AudioSource>().Play();

		if (sceneManager.currentState == CoffeSceneManager.SceneState.CAKE_OK)
		{
			cakeRaw.SetActive(false);
			cakeOk.SetActive(true);

			Invoke("CakeBakedOk", 3f);
		}
		else if (sceneManager.currentState == CoffeSceneManager.SceneState.CAKE_BURNT)
		{
			cakeRaw.SetActive(false);
			cakeBurnt.SetActive(true);

			Invoke("CakeBakedOk", 3f);
		}
	}
}