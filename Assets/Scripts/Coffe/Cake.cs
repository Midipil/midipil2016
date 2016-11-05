using UnityEngine;
using System.Collections;

public class Cake : MonoBehaviour
{
    public GameObject cakeRaw, cakeOk, cakeBurnt;

    void Start()
    {
        cakeRaw.SetActive(true);
        cakeOk.SetActive(false);
        cakeBurnt.SetActive(false);
    }
}