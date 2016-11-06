using UnityEngine;
using UnityEngine.UI;

public class CodePeeing : MonoBehaviour {

    bool initialized;

    public GameObject codingScreen;
    public Text codeText;
    public int codePieceLength;

    public AudioClip[] keyboardSounds = new AudioClip[3];

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
		if (initialized && other.tag.Contains("Hand"))
        {
            if (currentTextLength + codePieceLength <= randomCode.Length)
                DisplayMoreCode();
        }
    }

    void DisplayMoreCode()
    {
		if (currentTextLength == 0) {
			if (!FindObjectOfType<JamTimer> ().hasDoneSomething) {
				FindObjectOfType<JamTimer>().hasDoneSomething = true;
				FindObjectOfType<JamTimer> ().Invoke ("EndJam", 5f);
			}
		}

        codeText.text += randomCode.Substring(currentTextLength, codePieceLength);
        currentTextLength += codePieceLength;

        if (!GetComponent<AudioSource>().isPlaying)
            GetComponent<AudioSource>().PlayOneShot(keyboardSounds[Random.Range(0, 2)], 1f);
    }
}
