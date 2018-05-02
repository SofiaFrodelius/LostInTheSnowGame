using UnityEngine;


[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class Item : ScriptableObject
{
    [SerializeField]
    private int itemID, maxStack;
    [SerializeField]
    private string itemName, itemDescription;
    [SerializeField]
    private Sprite neutralImage;
    [SerializeField]
    private Sprite selectedImage;
    [SerializeField]
    private GameObject associatedGameobject;
    [SerializeField]
    private bool holdableItem;



    public Sprite getNeutralImage()
    {
        return neutralImage;
    }

    public Sprite getSelectedImage()
    {
        return selectedImage;
    }
    
    public bool getHoldable()
    {
        return holdableItem;
    }

    public int getId()
    {
        return itemID;
    }

    public int getMaxStack()
    {
        return maxStack;
    }

    public string getName()
    {
        return itemName;
    }

    public string getDescription()
    {
        return itemDescription;
    }

    public GameObject getAssociatedGameobject()
    {
        return associatedGameobject;
    }
}
