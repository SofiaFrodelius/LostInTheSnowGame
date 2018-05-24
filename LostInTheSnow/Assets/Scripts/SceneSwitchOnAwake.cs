using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSwitchOnAwake : MonoBehaviour
{
    [SerializeField] private SceneSwitchScript sceneSwitcher;
    [SerializeField] private int sceneIndex;


    private void Awake()
    {
        sceneSwitcher.ActivateSceneSwitch(sceneIndex);
    }
}
