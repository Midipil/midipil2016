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
			if(transform.up.y < -0.2f)
            {
                DrinkCoffee();
            }
        }
	}

    public void CollideWithHead(bool collide)
    {
        collideWithHead = collide;

        FindObjectOfType<JamTimer>().hasDoneSomething = false;
    }

    void DrinkCoffee()
    {
        GetComponent<AudioSource>().Play();
        drinkingCoffee = true;
        Invoke("CoffeeDrank", delayToDrinkCoffee);
    }

    void CoffeeDrank()
    {
        FindObjectOfType<TextDisplay>().ShowEndMessage("Tu as gagné la jam !", true);
    }
}
