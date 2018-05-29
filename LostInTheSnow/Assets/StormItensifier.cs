using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class StormItensifier : MonoBehaviour {
    [SerializeField] private float newMoveSpeed;
    [SerializeField] private float newSprintMultiplier;
    [SerializeField] private GameObject newSnowParticleSystem;
    [SerializeField] private bool changeMusic = false;
    [SerializeField] private StudioEventEmitter musicToChange;
    [SerializeField] private float fogAfterEnter;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            CharacterMovement cm = other.gameObject.GetComponent<CharacterMovement>();
            cm.MovementSpeed = newMoveSpeed;
            cm.SprintMultiplier = newSprintMultiplier;
            Transform pp = other.transform.Find("ParticlePos");
            for(int i = 0; i < pp.childCount; i++)
            {
                Destroy(pp.GetChild(i).gameObject);
            }
            Instantiate(newSnowParticleSystem, pp);
            if (changeMusic)
            {
                musicToChange.SetParameter("Last_Day_END", 1f);
            }

            RenderSettings.fog = true;
            RenderSettings.fogEndDistance = fogAfterEnter;
            
        }
    }
}
