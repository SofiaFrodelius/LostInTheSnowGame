using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour {

    ScreenFadeScript screenFadeScript;

    void Awake()
    {
        screenFadeScript = FindObjectOfType<ScreenFadeScript>();
    }


    public void StartBtn(string SceneName)
    {
        StartCoroutine(NewGame());
    }

    IEnumerator NewGame()
    {
        screenFadeScript.InvertFade();
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(1);
    }

    public void Day1Btn(string SceneName)
    {
        if (PlayerPrefs.GetInt("Cleared1") == 1)
        {
            SceneManager.LoadScene(1);
        }
        else { }
    }

    public void Day2Btn(string SceneName)
    {
        if (PlayerPrefs.GetInt("Cleared2") == 1)
        {
            SceneManager.LoadScene(2);
        }
        else { }
    }

    public void Day3Btn(string SceneName)
    {
        if (PlayerPrefs.GetInt("Cleared3") == 1)
        {
            SceneManager.LoadScene(3);
        }
        else { }
    }

    public void BobBtn()
    {

        GameObject Bobtoggle;
        Bobtoggle = GameObject.Find("BobToggle");
        if (Bobtoggle.GetComponent<UnityEngine.UI.Toggle>().isOn == true)
        {
            PlayerPrefs.SetInt("Headbob", 1);
        }
        else if (Bobtoggle.GetComponent<UnityEngine.UI.Toggle>().isOn == false)
        {
            PlayerPrefs.SetInt("Headbob", 0);
        }
        
    }

    public void SoundBtn()
    {
        Slider Soundslider;
        Soundslider = GameObject.Find("SoundVolume").GetComponent<UnityEngine.UI.Slider>();
        PlayerPrefs.SetFloat("Sound", Soundslider.value);

    }

    public void MusicBtn()
    {
        Slider Musicslider;
        Musicslider = GameObject.Find("MusicVolume").GetComponent<UnityEngine.UI.Slider>();
        PlayerPrefs.SetFloat("Music", Musicslider.value);

    }

    public void ExitBtn()
    {
        //UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }

    public void SettingsTemp()
    {
        GameObject Bobtoggle;
        Bobtoggle = GameObject.Find("BobToggle");
        //Debug.Log(PlayerPrefs.GetInt("Headbob"));
        if (PlayerPrefs.GetInt("Headbob") == 1)
        {
            Bobtoggle.GetComponent<UnityEngine.UI.Toggle>().isOn = true;
        }
        else
        {
            Bobtoggle.GetComponent<UnityEngine.UI.Toggle>().isOn = false;
        }
        GameObject.Find("SoundVolume").GetComponent<UnityEngine.UI.Slider>().value = PlayerPrefs.GetFloat("Sound");
        GameObject.Find("MusicVolume").GetComponent<UnityEngine.UI.Slider>().value = PlayerPrefs.GetFloat("Music");
    }



    // Use this for initialization
    void Start () {
        PlayerPrefs.SetInt("Cleared1", 0);
        PlayerPrefs.SetInt("Cleared2", 0);
        PlayerPrefs.SetInt("Cleared3", 0);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
