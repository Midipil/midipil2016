using UnityEngine;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour {

    public Text feedback;
    public float feedbackDelay;

    bool gameWon;

    WorldManager sceneManager;

    public void Initialize(WorldManager sceneManager)
    {
        this.sceneManager = sceneManager;
    }

    public void ShowEndScreen(string message, bool win)
    {
        feedback.text = message;
        gameObject.SetActive(true);
        gameWon = win;
        Invoke("EndScene", feedbackDelay);
    }

    void EndScene()
    {
        sceneManager.SetEnd(gameWon);
    }
}
