using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IlaWayPointSitPlaceSitter : MonoBehaviour {
    
	// Use this for initialization
	void Start () {
        ChracterInteract characterIntercat = GameObject.FindGameObjectWithTag("Player").GetComponent<ChracterInteract>();
        characterIntercat.PermitAction(0, false);
        characterIntercat.PermitAction(1, false);
        characterIntercat.PermitAction(2, false);
        characterIntercat.PermitAction(3, false);
        characterIntercat.PermitAction(4, true);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            

            Vector3 ilaTelPos = other.transform.position + -other.transform.right * 20;
            Vector3 ilaSitPos = other.transform.position + -other.transform.right * 1.5f + other.transform.forward * 3f;



            Dog dog = GameObject.FindGameObjectWithTag("Dog").GetComponent<Dog>();
            //dog.gameObject.transform.position = ilaTelPos;
            DogAI dogai = dog.gameObject.GetComponent<DogAI>();
            dogai.StartAction(new GoStraightToPosition(dog, ilaTelPos));

            //dogai.StartAction(new GoStraightToPosition(dog, ilaSitPos));
            StartCoroutine(blabla(dog, dogai, new GoStraightToPosition(dog, ilaSitPos)));
            

        }
    }
    IEnumerator blabla(Dog dog, DogAI dogai, DogAction dogaction) {
        bool dogready = false;
        while (!dogready) {
            dogready = dogai.gameObject.GetComponent<Dog>().IsIdle();
            
            yield return null;
        }
        dogai.StartAction(dogaction);
        StartCoroutine(blabla(dog, dogai, new Rest(dog, -1)));
    }
}
