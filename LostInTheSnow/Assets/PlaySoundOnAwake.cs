using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class PlaySoundOnAwake : MonoBehaviour
{
    [SerializeField] private StudioEventEmitter see;



    private void Awake()
    {
        see.Play();
    }
}
