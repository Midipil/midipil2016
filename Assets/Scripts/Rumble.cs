using UnityEngine;
using System.Collections;

public class Rumble : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void StartRumble(float length, float strength)
    {
        StartCoroutine(LongVibration(length, strength));
    }

    public void StartRumble(float length)
    {
        StartCoroutine(LongVibration(length, 1.0f));
    }

    public void StartRumble()
    {
        StartCoroutine(LongVibration(0.5f, 1.0f));
    }

    IEnumerator LongVibration(float length, float strength)
    {
        for (float i = 0; i < length; i += Time.deltaTime)
        {
            SteamVR_Controller.Input((int)transform.GetComponent<SteamVR_TrackedObject>().index).TriggerHapticPulse((ushort)Mathf.Lerp(0, 3999, strength));
            yield return null;
        }
    }
}
