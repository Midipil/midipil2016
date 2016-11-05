using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TinderImg : MonoBehaviour 
{
	public Image img;
	public TinderBtn[] buttons;

	private bool fading = false;
	private bool fadeIn = false, fadeOut = false;
	private float startTimeFade;
	private Color fadeColor;

	void Update()
	{
		if(fading)
		{
			float t = (Time.time - startTimeFade) / 1.5f;

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
				if(fadeIn)
				{
					foreach(TinderBtn btn in buttons)
						btn.SetCollision(true);
				}

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

		if(fadeOut)
		{
			foreach(TinderBtn btn in buttons)
				btn.SetCollision(false);
		}

		fadeColor = Color.white;
		startTimeFade = Time.time;
		fading = true;
	}
}
