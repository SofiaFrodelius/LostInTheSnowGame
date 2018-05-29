using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttableTree : MonoBehaviour//, IInteractible
{
    private int hitPoints = 3;
    private bool activated = false;
    private bool chopFinished = true;
    private GameObject playerObj;
    private CharacterMovement charMove = null;
    GameObject activeItem = null;
    [SerializeField] private GameObject firewood;
    [SerializeField] private int firewoodAmount = 10;
    [SerializeField] private float cuttingDistance = 1.8f;

    public void AlternateInteract()
    {
        throw new System.NotImplementedException();
    }

    public void AxeInteract()
    {
        playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
            charMove = playerObj.GetComponent<CharacterMovement>();
        if (charMove != null)
        {
            ItemHand itemHand = Camera.main.GetComponentInChildren<ItemHand>();
            if (itemHand != null)
                activeItem = itemHand.ActiveItem;
            if (activeItem != null)
            {
                if (activeItem.tag == "Axe")
                {
                    activated = true;
                    float hAngle;
                    Vector2 deltaVector = new Vector2(transform.position.x - playerObj.transform.position.x, transform.position.z - playerObj.transform.position.z);
                    hAngle = Mathf.Atan2(deltaVector.x, deltaVector.y);
                    hAngle *= 360;
                    hAngle /= Mathf.PI * 2;
                    hAngle += 1.8f;
                    while (hAngle < 0)
                        hAngle += 360;
                    while (hAngle >= 360)
                        hAngle -= 360;
                    Vector2 targetLook = new Vector2(hAngle, 0f);
                    Vector3 cuttingposition = new Vector3(-deltaVector.x, 0, -deltaVector.y);
                    cuttingposition.Normalize();
                    charMove.ForceMovement(transform.position + new Vector3(cuttingposition.x * cuttingDistance, charMove.transform.position.y - transform.position.y,
                        cuttingposition.z * cuttingDistance), targetLook, false);
                }
            }
        }
    }

    private void Update()
    {
        if (activated)
        {
            if (charMove != null)
            {
                if (!charMove.GetForcedMove())
                {
                    AxeSwing axeSwing = activeItem.GetComponent<AxeSwing>();
                    if (axeSwing != null)
                    {
                        // Play animation here
                        ItemHand itemHand = Camera.main.GetComponentInChildren<ItemHand>();
                        if (itemHand != null && chopFinished)
                        {
                            axeSwing.Use(itemHand);
                            chopFinished = false;
                        }
                        if (!chopFinished)
                        {
                            if (!axeSwing.IsChopping)
                            {
                                chopFinished = true;
                            }
                        }
                        if (chopFinished)
                        {
                            activated = false;
                            charMove.CutsceneRelease();
                            hitPoints--;
                            if (hitPoints <= 0)
                            {
                                // Tree dying stuff goes here
                                for (int i = 0; i < firewoodAmount; i++)
                                {
                                    Instantiate(firewood, transform.position + new Vector3(0, 0.2f + (0.5f * i), 0), Quaternion.LookRotation(Vector3.up));
                                }
                                gameObject.SetActive(false);
                            }
                        }
                    }
                    else Debug.Log("axeSwing is null");
                }
            }
            else Debug.Log("charMove is null");
        }
    }
}