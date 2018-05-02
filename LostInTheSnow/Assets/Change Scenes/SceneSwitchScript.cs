using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitchScript : MonoBehaviour
{
    public int sceneTarget = 0;

    private bool active = false;
    private int fadeTime = 0;
    private float wait = 0.0f;

    public void ActivateSceneSwitch()
    {
        GameObject ScreenFader = transform.GetChild(0).gameObject;
        if (ScreenFader != null)
            fadeTime = ScreenFader.GetComponent<ScreenFadeScript>().BeginFade(1);
        else
            Debug.LogWarning("Object does not have a Screen Fader", this);
        active = true;
    }

    public void ActivateSceneSwitch(int index)
    {
        sceneTarget = index;
        ActivateSceneSwitch();
    }

    private void Update()
    {
        if (active)
        {
            if (fadeTime == 0)
                wait = 1.0f;
            else
                wait += 1000 / fadeTime * Time.deltaTime;
            if (wait >= 1.0f)
            {
                active = false;
                wait = 0.0f;
                fadeTime = 0;
                SceneHandler.ChangeScene(sceneTarget);
            }
        }
    }
}