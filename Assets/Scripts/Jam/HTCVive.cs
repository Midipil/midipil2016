using UnityEngine;
using System.Collections;

public class HTCVive : MonoBehaviour
{
    Grabbable grabbable;

    bool collideWithHead;

    void Start()
    {
        grabbable = GetComponent<Grabbable>();
    }

    void Update()
    {

    }

    public void CollideWithHead(bool collide)
    {
        collideWithHead = collide;
        Debug.Log("collide " + collideWithHead);

        if(collide)
            PutVive();
    }

    public void PutVive()
    {
        FindObjectOfType<TextDisplay>().ShowEndScreen("Tu as gagné la jam !", true);
    }
}
