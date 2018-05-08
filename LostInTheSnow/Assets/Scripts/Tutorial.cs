using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private DogAI dog;

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
    }


    public void Update()
    {
    }



    public void triggerTutorial(int tutorialID)
    {
        tutorialUI.setTutorial(tutorialTexts[tutorialID]);
        activeTutorial = tutorialID;
        switch(tutorialID)
        {
            case 0:
                //enable input for pet.. rasmus
                //pet dog


                //tutorial 0 starting
                break;

            case 1:
                //dog go to waypoint

                dog.StartAction(new GoStraightToPosition(dog.GetComponent<Dog>(), dogWaypoint.position));
                //tutorial 1 starting
                break;
            case 2:
                //tutorial 2 starting
                //enable call dog thing

                break;

            case 3:
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

                    //waitForIdleTrigger(activeTutorial+1);
                    currentCR = StartCoroutine(waitForIdleTrigger(activeTutorial+1));
                    break;
                case 1:
                    //tutorial 1 finished
                    break;
                case 2:
                    //tutorial 2 finished
                    break;
                case 3:
                    //tutorial 3 finished
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

                Debug.Log("Triggering: " + id);
                triggerTutorial(id);
                StopCoroutine(currentCR);
            }
            yield return new WaitForSeconds(0.2f);
        }
        
    }
}
