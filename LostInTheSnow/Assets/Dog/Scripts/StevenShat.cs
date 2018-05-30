using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StevenShat : MonoBehaviour {
    public GameObject Shat;
    public GameObject Ehat;
    string code;
    void Update(){
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.S))
            code = "";
        foreach (char c in Input.inputString){
            code += c;
        }
        if (code == "shat")
            Shat.SetActive(true);
        if (code == "ehat")
            Ehat.SetActive(true);
    }
}
