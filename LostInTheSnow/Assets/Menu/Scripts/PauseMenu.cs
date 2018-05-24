using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {
    [SerializeField] private GameObject pauseMenuFull;
    private GameObject pauseMenuMain;
    private GameObject optionsMenu;
    private GameObject exitMenu;
    [SerializeField] private GameObject[] uiToHide;
    [SerializeField] private CharacterMovement charMov;
    [SerializeField] private CameraController camCon;
    private bool isPaused = false;

    private bool inCameraLockSinceBefore = false;
    private bool inCharacterLockSinceBefore = false;
    Vector2 camLooks;
    

    void Start () {
        if (!pauseMenuFull)
        {
            pauseMenuFull = GameObject.FindGameObjectWithTag("PauseMenu");
        }
        if (pauseMenuFull)
        {
            pauseMenuMain = pauseMenuFull.transform.GetChild(0).gameObject;
            
            optionsMenu = pauseMenuFull.transform.GetChild(1).gameObject;
            exitMenu = pauseMenuFull.transform.GetChild(2).gameObject;

            for(int i = 0; i < pauseMenuFull.transform.childCount; i++)
            {
                pauseMenuFull.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
        if (!camCon)
        {
            camCon = GameObject.FindWithTag("Player").GetComponentInChildren<CameraController>();
        }
        if (!charMov)
        {
            charMov = GameObject.FindWithTag("Player").GetComponent<CharacterMovement>();
        }
    }
	
	void Update () {
        if (Input.GetButtonDown("Cancel"))
        {
            if (pauseMenuFull)
            {
                if (!isPaused)
                    PauseGame();
                else
                    UnPauseGame();
            }
        }	
	}

    void PauseGame()
    {
        camLooks = camCon.getLook();
        Cursor.lockState = CursorLockMode.Confined;
        isPaused = true;
        inCameraLockSinceBefore = camCon.CutsceneLock;
        inCharacterLockSinceBefore = charMov.CutsceneLock;
        camCon.CutsceneLock = isPaused;
        charMov.CutsceneLock = isPaused;
        pauseMenuMain.SetActive(isPaused);
        Time.timeScale = 0;
        foreach (GameObject g in uiToHide)
        {
            g.SetActive(!isPaused);
        }
    }

    public void UnPauseGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        isPaused = false;
        camCon.CutsceneLock = inCameraLockSinceBefore;
        charMov.CutsceneLock = inCharacterLockSinceBefore;
        pauseMenuMain.SetActive(isPaused);
        Time.timeScale = 1;
        foreach (GameObject g in uiToHide)
        {
            g.SetActive(!isPaused);
        }
        camCon.setLook(camLooks);
    }

    public void OptionsToggle(bool showOptions)
    {
        pauseMenuMain.SetActive(!showOptions);
        optionsMenu.SetActive(showOptions);
    }

    public void ExitGameToggle(bool showExit)
    {
        pauseMenuMain.SetActive(!showExit);
        exitMenu.SetActive(showExit);
    }

    public void ExitGame()
    {
        SceneHandler.ChangeScene(0);
    }
}
