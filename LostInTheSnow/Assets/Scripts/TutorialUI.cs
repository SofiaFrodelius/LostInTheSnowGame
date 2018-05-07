using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialUI : MonoBehaviour
{
    public static TutorialUI instance;
    private Text tutorialText;

    public void Awake()
    {
        tutorialText = gameObject.GetComponentInChildren<Text>();
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of TutorialUI in scene.");
            return;
        }
        instance = this;
    }


    public void setTutorial(string text)
    {
        tutorialText.text = text;
    }
}
