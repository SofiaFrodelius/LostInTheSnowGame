using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FallSoundOnInpact : MonoBehaviour {
    [SerializeField] private float distanceToPlaySound;
    
    [Tooltip("If downwards speed exceeds this value a land-sound will be played on land")]
    [SerializeField] private float minFallSped;
    StudioEventEmitter sEmitter;
    CharacterController cc;
    bool soundPlayed = false;

    private void Start()
    {
        cc = GetComponentInParent<CharacterController>();
        sEmitter = GetComponent<StudioEventEmitter>();


    }
    private void Update()
    {
        if (cc.velocity.y < minFallSped && !soundPlayed)
        {
            RaycastHit hit = new RaycastHit();
            Ray ray = new Ray(transform.position, Vector3.down);
            if (Physics.Raycast(ray, out hit, distanceToPlaySound))
            {
                
                sEmitter.Play();
                soundPlayed = true;
                
            }
        }
        else if(cc.isGrounded)
        {
            soundPlayed = false;
        }
    
    }

}
