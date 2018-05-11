using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private DogAI dog;

    private ChracterInteract ci;

    [SerializeField]
    private Transform dogWaypoint;
    Coroutine currentCR;

    TutorialUI tutorialUI;
    [SerializeField]
    private string[] tutorialTexts;
    private int activeTutorial = 99999;

    public static Tutorial instance;

    private bool[] tutorialFinished;

    public void Awake()
    {
        tutorialFinished = new bool[tutorialTexts.Length];
        tutorialFinished[0] = false;
        if(instance != null)
        {
            Debug.LogWarning("Two or more instances of Tutorial in scene.");
            return;
        }
        instance = this;
    }


    public void Start()
    {
        
        tutorialUI = TutorialUI.instance;
        ci = player.GetComponent<ChracterInteract>();
    }


    public void Update()
    {


    }



    public void triggerTutorial(int tutorialID)
    {
        tutorialUI.setTutorial(tutorialTexts[tutorialID]);
        activeTutorial = tutorialID;
        Debug.Log("Triggering: " + tutorialID);
        switch(tutorialID)
        {
            case 0:
                //dog moves to player

                player.GetComponent<CharacterMovement>().CutsceneLock = true;
                ci.PermitAction(0, false);
                ci.PermitAction(1, false);
                ci.PermitAction(2, false);
                ci.PermitAction(3, false);
                dog.StartAction(new Call(dog.GetComponent<Dog>(), player.transform));
                finishTutorial(activeTutorial);
                break;

            case 1:
                //pet dog
                ci.PermitAction(2, true);
                //tutorial 1 starting
                break;
            case 2:
                //dog go to waypoint
                ci.PermitAction(2, false);
                dog.StartAction(new GoStraightToPosition(dog.GetComponent<Dog>(), dogWaypoint.position));
                //tutorial 2 starting
                break;

            case 3:
                ci.PermitAction(1, true);
                //enable call dog thing
                break;
            case 4:
                ci.PermitAction(1, false);
                ci.PermitAction(3, true);
                //pick up dog enable
                break;
        }


    }



    public void finishTutorial(int tutorialID)
    {
        if (!tutorialFinished[tutorialID] && activeTutorial == tutorialID)
        {
            tutorialFinished[tutorialID] = true;
            tutorialUI.setTutorial("");

            switch(tutorialID)
            {
                case 0:
                    //Tutorial 0 finished

                    currentCR = StartCoroutine(waitForIdleTrigger(activeTutorial + 1));
                    break;
                case 1:
                    currentCR = StartCoroutine(waitForIdleTrigger(activeTutorial + 1));

                    //tutorial 1 finished
                    break;
                case 2:
                    //tutorial 2 finished
                    break;
                case 3:
                    //tutorial 3 finished
                    break;
                case 4:
                    player.GetComponent<CharacterMovement>().CutsceneLock = false;
                    ci.PermitAction(0, true);
                    ci.PermitAction(1, true);
                    ci.PermitAction(2, true);
                    ci.PermitAction(3, true);
                    dog.GetComponent<ScriptedDog>().enabled = true;
                    break;
            }


        }
    }



    private IEnumerator waitForIdleTrigger(int id)
    {
        while (true)
        {
            if (dog.GetComponent<Dog>().IsIdle())
            {
                triggerTutorial(id);
                StopCoroutine(currentCR);
            }
            yield return new WaitForSeconds(0.2f);
        }
    }
}


