using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    [SerializeField]
    private int tutorialToTrigger;
    private Tutorial tutorial;
    [SerializeField]
    bool triggerByPlayer = true;
    [SerializeField]
    bool triggerByDog = false;
    [SerializeField]
    private BoxCollider nextTutorialTrigger;


    public void Start()
    {
        tutorial = Tutorial.instance;
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && triggerByPlayer)
        {
            tutorial.triggerTutorial(tutorialToTrigger);
            if (nextTutorialTrigger != null) nextTutorialTrigger.enabled = true;
            Destroy(gameObject);
        }

        else if (other.tag == "Dog" && triggerByDog)
        {
            tutorial.triggerTutorial(tutorialToTrigger);
            if (nextTutorialTrigger != null) nextTutorialTrigger.enabled = true;
            Destroy(gameObject);
        }

    }

}
