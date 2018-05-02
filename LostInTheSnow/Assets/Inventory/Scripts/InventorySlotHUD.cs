using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class InventorySlotHUD : MonoBehaviour
{
    [SerializeField]
    private Image itemImage;
    [SerializeField]
    private Text itemDescription;
    [SerializeField]
    private Text itemName;
    [SerializeField]
    private Text numOfItemsInSlot;

    private Item currentItem;
    private Vector3 previousPosition;
    IEnumerator currentCoroutine;

    public void Awake()
    {
        itemDescription.enabled = false;
        itemName.enabled = false;
    }


    public void updateSlot(Item item, int test)
    {
        itemImage.enabled = true;
        currentItem = item;
        itemImage.sprite = item.getNeutralImage();
        itemDescription.text = item.getDescription();
        itemName.text = item.getName();
        numOfItemsInSlot.text = "x " + test.ToString();
        showInventorySlot();
    }

    public void showInventorySlot()
    {
        if (currentCoroutine != null)
            StopCoroutine(currentCoroutine);

        currentCoroutine = previewSlot(0.5f);
        StartCoroutine(currentCoroutine);
    }


    IEnumerator previewSlot(float time)
    {
        float previousAlphaImage = itemImage.color.a;
        float previousAlphaText = numOfItemsInSlot.color.a;

        for(float f = 0; f < 1.0f; f += Time.deltaTime / time)
        {
            itemImage.color = new Color(itemImage.color.r, itemImage.color.g, itemImage.color.b, Mathf.Lerp(previousAlphaImage, 1.0f, f));
            numOfItemsInSlot.color = new Color(numOfItemsInSlot.color.r, numOfItemsInSlot.color.g, numOfItemsInSlot.color.b, Mathf.Lerp(previousAlphaText, 1.0f, f));
            yield return null;
        }

        previousAlphaImage = itemImage.color.a;
        previousAlphaText = numOfItemsInSlot.color.a;
        yield return new WaitForSeconds(2.5f);

        for (float f = 0; f < 1.0f; f += Time.deltaTime / time)
        {
            itemImage.color = new Color(itemImage.color.r, itemImage.color.g, itemImage.color.b, Mathf.Lerp(previousAlphaImage, 0.0f, f));
            numOfItemsInSlot.color = new Color(numOfItemsInSlot.color.r, numOfItemsInSlot.color.g, numOfItemsInSlot.color.b, Mathf.Lerp(previousAlphaText, 0.0f, f));
            yield return null;
        }
        currentCoroutine = null;
    }

    public Item getCurrentItem()
    {
        return currentItem;
    }
}
