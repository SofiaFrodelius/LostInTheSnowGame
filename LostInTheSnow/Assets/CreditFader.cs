using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CreditFader : MonoBehaviour {
    TextMeshProUGUI[] texts;
    int bruh = 0;
    [SerializeField] private float fadeInTime;
    [SerializeField] private float fadeOutTime;
    [SerializeField] private float showTime;
    [SerializeField] private SceneSwitchScript sceneswitcher;


    // Use this for initialization
    void Start () {
        texts = GetComponentsInChildren<TextMeshProUGUI>();
        StartCoroutine(FadeIn());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    IEnumerator FadeIn()
    {
        float Tajmer = 0;
        bool inFaded = false;
        while (!inFaded)
        {
            Tajmer += Time.deltaTime;
            if (Tajmer / fadeInTime > 1) inFaded = true;
            texts[bruh].alpha = Tajmer / fadeInTime;
            yield return null;
            
        }
        StartCoroutine(StayTime());
    }
    IEnumerator FadeOut()
    {
        float Tajmer = 0;
        bool outFaded = false;
        while (!outFaded)
        {
            Tajmer += Time.deltaTime;
            if (Tajmer / fadeOutTime > 1) outFaded = true;
            texts[bruh].alpha = 1 - (Tajmer / fadeOutTime);
            yield return null;
        }
        bruh++;
        if (bruh < texts.Length)
        {
            StartCoroutine(FadeIn());
        }
        else
        {
            sceneswitcher.ActivateSceneSwitch();
        }
    }
    IEnumerator StayTime()
    {

            yield return new WaitForSeconds(showTime);
        StartCoroutine( FadeOut());

    }
}
