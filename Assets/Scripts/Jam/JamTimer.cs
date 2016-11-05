using UnityEngine;
using UnityEngine.UI;

public class JamTimer : MonoBehaviour {

    public float jamDuration, delayAfterJamEnd;

    public bool hasDoneSomething;

    public GameObject feedback;

    JamSceneManager sceneManager;

    bool jamWon;
    
    public void Initialize(JamSceneManager sceneManager) {
        this.sceneManager = sceneManager;
        Invoke("EndJam", jamDuration);
	}

    void EndJam()
    {
        if (!hasDoneSomething)
            return;

        feedback.GetComponentInChildren<Text>().text = "La jam est terminée !";
        feedback.SetActive(true);
        jamWon = false;
        Invoke("EndScene", delayAfterJamEnd);
    }

    public void EndJam(string feedbackText)
    {
        feedback.GetComponentInChildren<Text>().text = feedbackText;
        feedback.SetActive(true);
        jamWon = true;
        Invoke("EndScene", delayAfterJamEnd);
    }

    void EndScene()
    {
        sceneManager.SetEnd(jamWon);
    }
}
