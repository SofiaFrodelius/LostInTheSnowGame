using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuHandler : MonoBehaviour {
    [SerializeField] private GameObject mainMenuObject;
    private GameObject mainMenu;
    private GameObject optionsMenu;
    private GameObject ExitMenu;
    string[] headbobStrings = {"Headbob is ON", "Headbob is OFF" };
    private bool headbob = true;
    // Use this for initialization

    ScreenFadeScript screenFadeScript;

    void Start () {
        screenFadeScript = FindObjectOfType<ScreenFadeScript>();
        mainMenu = mainMenuObject.transform.GetChild(0).gameObject;
        optionsMenu = mainMenuObject.transform.GetChild(1).gameObject;
        ExitMenu = mainMenuObject.transform.GetChild(2).gameObject;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void StartGame()
    {
        StartCoroutine(NewGame());
    }
    IEnumerator NewGame()
    {
        screenFadeScript.InvertFade();
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(1);
    }
    public void ExitToggle(bool showExit)
    {
        ExitMenu.SetActive(showExit);
        mainMenu.SetActive(!showExit);
    }
    public void OptionsToggle(bool showOptions)
    {
        optionsMenu.SetActive(showOptions);
        mainMenu.SetActive(!showOptions);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void HeadBobToggle(GameObject temp)
    {
        headbob = !headbob;
        TextMeshProUGUI headBobText = temp.GetComponent<TextMeshProUGUI>();
        if (headbob)
        {
            headBobText.text = headbobStrings[0];
            PlayerPrefs.SetInt("Headbob", 1);
        }
        else
        {
            headBobText.text = headbobStrings[1];
            PlayerPrefs.SetInt("Headbob", 0);
        }
    }
}
