using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TinderImg : MonoBehaviour 
{
	public Image img;

	private bool fading = false;
	private bool fadeIn = false, fadeOut = false;
	private float startTimeFade;
	private Color fadeColor;

	void Update()
	{
		if(fading)
		{
			float t = (Time.time - startTimeFade) / 0.5f;

			if(t <= 1f)
			{
				if(fadeIn)
					fadeColor.a = t;
				else
					fadeColor.a = (1f - t);

				img.color = fadeColor;
			}
			else
			{
				fading = false;
			}
		}
	}

	public void Fade(bool fade_in)
	{
		if(fade_in)
		{
			fadeOut = false;
			fadeIn = true;
		}
		else
		{
			fadeIn = false;
			fadeOut = true;
		}

		fadeColor = Color.white;
		startTimeFade = Time.time;
		fading = true;
	}

    public void SetImage(Sprite new_img)
    {
        img.sprite = new_img;
    }
}
