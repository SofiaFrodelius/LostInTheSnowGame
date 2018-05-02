using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenFadeScript : MonoBehaviour
{
    public Texture2D fadeTexture;
    public int fadeTime = 1000;

    private int drawDepth = -1000;
    private float alpha = 0.0f, wait = 0.0f;
    private int fadeDirection = -1, fadeWaitTime = 0;
    private bool fadeInOut = false;

    private void OnGUI()
    {
        if (fadeTime == 0)
            alpha = fadeDirection;
        else
            alpha += fadeDirection * Time.deltaTime * 1000 / fadeTime;
        alpha = Mathf.Clamp01(alpha);

        if (fadeInOut && alpha >= 1.0f)
        {
            if (fadeWaitTime == 0)
                wait = 1.0f;
            else
                wait += Time.deltaTime * 1000 / fadeWaitTime;
            if (wait >= 1.0f)
                BeginFade(-1);
        }

        GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);
        GUI.depth = drawDepth;
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeTexture);
    }

    public int BeginFade(int direction)
    {
        fadeDirection = direction;
        ResetFadeInOut();
        return fadeTime;
    }

    public void InvertFade()
    {
        fadeDirection *= -1;
        ResetFadeInOut();
    }

    public void SetFadeSpeed(int speed)
    {
        fadeTime = speed;
    }

    public void FadeInOut(int fadeWait)
    {
        fadeInOut = true;
        fadeDirection = 1;
        fadeWaitTime = fadeWait;
    }

    private void ResetFadeInOut()
    {
        fadeInOut = false;
        wait = 0.0f;
    }

    public void SetFade(float fadeValue)
    {
        alpha = fadeValue;
        ResetFadeInOut();
        if (fadeValue <= 0.0f)
            fadeDirection = -1;
        else if (fadeValue >= 1.0f)
            fadeDirection = 1;
        else
            fadeDirection = 0;
    }
}