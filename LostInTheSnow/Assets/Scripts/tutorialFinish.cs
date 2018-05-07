using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialFinish : MonoBehaviour//, ITutorialInteractable
{
    [SerializeField]
    private int tutorialToFinish;
    Tutorial tutorial;


    public void Start()
    {
        tutorial = Tutorial.instance;
    }


    public void onTutorialFinish(int id)
    {
        tutorial.finishTutorial(id);
    }

}
