using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeSwing : MonoBehaviour, IUsable
{
    bool InspectBool = false;
    bool TreeChopBool = false;
    [SerializeField] private GameObject chopParticles;
    [SerializeField] private Transform particlePos;
    Animator anim;
    private bool isChopping = false;
    private int counter = 0;

    void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void Use(ItemHand ih)
    {
        SwingAxe();
    }
    void SwingAxe()
    {
        if (!isChopping)
        {
            anim.SetTrigger("AxeChop");
            isChopping = true;
        }
    }
    private void Update()
    {
        if (isChopping)
        {
            counter++;
            if (counter >= 275)
            {
                counter = 0;
                isChopping = false;
            }
        }
    }
    public void AxeHit()
    {
        Debug.Log("Instansierar partikelsystem");
        Instantiate(chopParticles, particlePos);
    }

    public bool IsChopping
    {
        get { return isChopping; }
    }
}
