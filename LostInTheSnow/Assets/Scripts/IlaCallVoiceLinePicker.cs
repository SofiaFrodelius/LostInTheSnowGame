using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class IlaCallVoiceLinePicker : MonoBehaviour {
    public enum MoodState { Happy, commanding, pleading, paniced};
    [SerializeField] private MoodState moodState;
    [SerializeField] private int happyClose;
    [SerializeField] private int happyFar;
    [SerializeField] private int commandClose;
    [SerializeField] private int commandFar;
    [SerializeField] private int pleadFar;
    [SerializeField] private int panicedFar;

    private int currentFar;
    private int currentClose;

    [SerializeField] private float closeFarThreshold;

    [SerializeField] private StudioEventEmitter voiceLineMiscEmitter;

    // Use this for initialization
    void Start () {
        switch (moodState)
        {
            case MoodState.Happy:
                currentFar = happyFar;
                currentClose = happyClose;
                break;
            case MoodState.commanding:
                currentFar = commandFar;
                currentClose = commandClose;
                break;
            case MoodState.pleading:
                currentFar = pleadFar;
                currentClose = pleadFar;
                break;
            case MoodState.paniced:
                currentClose = panicedFar;
                currentFar = panicedFar;
                break;

        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void PlayVoiceLine(float distanceToIla)
    {
        Debug.Log("Borde spela voiceline");
        voiceLineMiscEmitter.Play();
        if (distanceToIla < closeFarThreshold)
        {
            voiceLineMiscEmitter.SetParameter("Voice Line", currentClose);
        }
        else
        {
            voiceLineMiscEmitter.SetParameter("Voice Line", currentFar);
        }
        
    }
}
