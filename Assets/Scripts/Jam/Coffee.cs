using UnityEngine;

public class Coffee : MonoBehaviour {

    public float delayToDrinkCoffee;

    Grabbable grabbable;

    bool collideWithHead, drinkingCoffee;

	void Start () {
        grabbable = GetComponent<Grabbable>();
	}
	
	void Update ()
    {
        if (collideWithHead && !drinkingCoffee)
        {
            if(Mathf.Abs(transform.rotation.eulerAngles.x) > 90 || Mathf.Abs(transform.rotation.eulerAngles.z) > 90)
            {
                DrinkCoffee();
            }
        }
	}

    public void CollideWithHead(bool collide)
    {
        collideWithHead = collide;

        FindObjectOfType<JamTimer>().hasDoneSomething = true;
    }

    void DrinkCoffee()
    {
        Debug.Log("DrinkCoffee");
        drinkingCoffee = true;
        Invoke("CoffeeDrank", delayToDrinkCoffee);
    }

    void CoffeeDrank()
    {
        FindObjectOfType<JamTimer>().EndJam("Tu as gagné la jam !");
    }
}
