using UnityEngine;
using System.Collections;

public class MouseDrawing : MonoBehaviour {

    bool initialized;

    public GameObject paintScreen;
    public Transform mouse;
    public LineRenderer line;
    public Rect mousePad;

    Vector3 previousPosition, lastRegisteredPosition;

    Transform hand;

    int pointCount = 0;

    public void Initialize()
    {
        paintScreen.SetActive(true);
        initialized = true;
    }
	
	void FixedUpdate ()
    {
        if (initialized)
        {
            if (hand != null)
                MoveMouse();
        }
    }

    void MoveMouse()
    {
        Vector3 newPosition = new Vector3(hand.position.x, 0f, hand.position.z);

        if (newPosition.x < mousePad.x)
            newPosition = new Vector3(mousePad.x, 0f, newPosition.z);
        else if(newPosition.x > mousePad.width)
            newPosition = new Vector3(mousePad.width, 0f, newPosition.z);

        if (newPosition.z < mousePad.y)
            newPosition = new Vector3(newPosition.x, 0f, mousePad.y);
        else if (newPosition.z > mousePad.height)
            newPosition = new Vector3(newPosition.x, 0f, mousePad.height);

        /*if (mousePad.Contains(new Vector2(newPosition.x, newPosition.z)))
        {*/
            mouse.Translate(newPosition - previousPosition);
            previousPosition = mouse.localPosition;

            if (Vector3.Distance(lastRegisteredPosition, previousPosition) > 0.005f)
            {
                AddPoint(previousPosition);
            }
        /*}
        else
            Debug.Log(newPosition.x + " " + newPosition.z);*/
    }

    void AddPoint(Vector3 position)
    {
        line.SetVertexCount(pointCount+1);
        line.SetPosition(pointCount, position);

        if(pointCount == 1)
        {
            line.SetPosition(pointCount - 1, position);
            FindObjectOfType<JamTimer>().hasDoneSomething = true;
        }

        pointCount++;
        lastRegisteredPosition = position;
    }

    void OnTriggerEnter(Collider other)
    {
        if (initialized && other.tag == "Hand")
        {
            hand = other.transform;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (initialized && other.tag == "Hand")
        {
            hand = null;
        }
    }
}
