using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeSwing : MonoBehaviour, IUsable
{
    bool InspectBool = false;
    bool TreeChopBool = false;
    [SerializeField] private GameObject chopParticles;
    [SerializeField] private Transform particlePos;
    [SerializeField] private LayerMask interactLayerMask;
    [SerializeField] private float maxInteractLength;
    Animator anim;
    private bool isChopping = false;
    private int counter = 0;
    [SerializeField] private int swingCounterLength = 140;
    Camera playerCam;

    void Start()
    {
        playerCam = Camera.main;
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
            RaycastHit hit = new RaycastHit();
            Ray ray = new Ray(playerCam.transform.position, playerCam.transform.forward);
            if (Physics.Raycast(ray, out hit, maxInteractLength, interactLayerMask))
            {
                CuttableTree ct = hit.transform.gameObject.GetComponent<CuttableTree>();
                if (ct)
                {

                    Debug.Log(hit.transform.gameObject);
                    hit.transform.GetComponent<CuttableTree>().AxeInteract();
                    anim.SetTrigger("AxeChop");
                    isChopping = true;
                }

            }
            else
            {
                anim.SetTrigger("AxeChop");
                isChopping = true;
            }
        }
    }
    private void Update()
    {
        if (isChopping)
        {
            counter++;
            if (counter >= swingCounterLength)
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
