using UnityEngine;
using UnityEngine.UI;

public class JamTimer : MonoBehaviour {

    public float jamDuration;

    public bool hasDoneSomething;

    JamSceneManager sceneManager;

    bool jamWon;
    
    public void Initialize(JamSceneManager sceneManager) {
        this.sceneManager = sceneManager;
        Invoke("EndJam", jamDuration);
        FindObjectOfType<TextDisplay>().DisplayText("Voici le thème de la jam :\n\"Déjà?\"\nFin dans 40h !", 10f);
    }

    public void EndJam()
    {
        if (!hasDoneSomething)
            return;

        FindObjectOfType<TextDisplay>().ShowEndMessage("Le temps est écoulé, vous avez perdu la jam...", false);
    }

    /*public void EndJam(string feedbackText)
    {
        feedback.GetComponentInChildren<Text>().text = feedbackText;
        feedback.SetActive(true);
        jamWon = true;
        Invoke("EndScene", delayAfterJamEnd);
    }*/
}
