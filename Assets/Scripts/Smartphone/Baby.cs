using UnityEngine;
using System.Collections;

public class Baby : MonoBehaviour
{
    public GameObject fxSpawn;
    public Transform anchorFx;
    public AudioSource babySound;
    public GameObject baby;
    public GameObject babyJP;
    public AudioClip georgeClip;

    // Use this for initialization
    void Start()
    {
        Instantiate(fxSpawn, anchorFx.position, Quaternion.identity);
        babySound.Play();
    }

    public void SetBabyJP()
    {
        baby.SetActive(false);
        babyJP.SetActive(true);
    }

    public void SetGeorgeSound()
    {
        babySound.clip = georgeClip;
    }
}
