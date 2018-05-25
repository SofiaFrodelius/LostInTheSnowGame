using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClipArtFade : MonoBehaviour {
    int nrOfChildren;
    int iterator = 0;
    [SerializeField] private float fadeTime = 2;
	// Use this for initialization
	void Start () {

        nrOfChildren = transform.childCount;
        for(int i = 0; i < nrOfChildren; i++)
        {
            transform.GetChild(i).GetComponent<Image>().color = new Color(1, 1, 1, 0);
        }
        StartCoroutine(FadeIn());
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    IEnumerator FadeIn()
    {
        float tajmer = 0;
        bool fading = true;
        while (fading)
        {
            tajmer += Time.deltaTime;
            transform.GetChild(iterator % nrOfChildren).GetComponent<Image>().color = new Color(1f, 1f, 1f, tajmer / fadeTime);
            if (tajmer / fadeTime > 1) fading = false;
            
            yield return null;
        }
        StartCoroutine(FadeOut());
    }
    IEnumerator FadeOut()
    {
        float tajmer = 0;
        bool fading = true;
        while (fading)
        {
            tajmer += Time.deltaTime;
            transform.GetChild(iterator % nrOfChildren).GetComponent<Image>().color = new Color(1f, 1f, 1f, 1 -  (tajmer / fadeTime));
            if (tajmer / fadeTime > 1) fading = false;
            yield return null;
        }
        iterator++;
        StartCoroutine(FadeIn());
    }
}
