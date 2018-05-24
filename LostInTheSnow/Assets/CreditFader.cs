using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CreditFader : MonoBehaviour {
    TextMeshProUGUI[] texts;
    int bruh = 0;
    [SerializeField] private float fadeTime;
    [SerializeField] private float showTime;


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
        float hmm;
        bool inFaded = false;
        while (!inFaded)
        {
            Tajmer += Time.deltaTime;
            hmm = Mathf.Lerp(0, 255, Tajmer / fadeTime);
            if (hmm > 1) inFaded = true;
            texts[bruh].alpha = hmm;
            yield return null;
            
        }
    }
}
