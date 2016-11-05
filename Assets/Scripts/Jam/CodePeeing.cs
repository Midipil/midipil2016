﻿using UnityEngine;
using UnityEngine.UI;

public class CodePeeing : MonoBehaviour {

    bool initialized;

    public GameObject codingScreen;
    public Text codeText;
    public int codePieceLength;

    string randomCode;

    int currentTextLength = 0;

    public void Initialize()
    {
        codingScreen.SetActive(true);
        randomCode = codeText.text;
        codeText.text = "";
        initialized = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (initialized && other.tag == "Player")
        {
            if (currentTextLength + codePieceLength <= randomCode.Length)
                DisplayMoreCode();
        }
    }

    void DisplayMoreCode()
    {
        codeText.text += randomCode.Substring(currentTextLength, codePieceLength);
        currentTextLength += codePieceLength;
    }
}
