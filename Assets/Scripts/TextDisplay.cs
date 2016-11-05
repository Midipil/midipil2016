﻿using UnityEngine;
using UnityEngine.UI;

public class TextDisplay : MonoBehaviour {

    public Text feedback;
    public float feedbackDelay;

    bool gameWon;

    WorldManager sceneManager;

    public void Initialize(WorldManager sceneManager)
    {
        this.sceneManager = sceneManager;
        feedback.text = "";
    }

    public void DisplayText(string text, float duration)
    {
        CancelInvoke("HideText");
        feedback.text = text;
        Invoke("HideText", duration);
    }

    public void HideText()
    {
        feedback.text = "";
    }

    public void ShowEndScreen(string message, bool win)
    {
        feedback.text = message;
        gameWon = win;
        Invoke("EndScene", feedbackDelay);
    }

    void EndScene()
    {
        sceneManager.SetEnd(gameWon);
    }
}
