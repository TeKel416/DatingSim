using UnityEngine;

public class ItemData : MonoBehaviour
{
    public int itemID, requiredItemID = -1, itemToCombineID = -1;
    public string itemName;
    public string hint;
    public string successMsg;
    public GameObject[] objectsToRemove;
    public Vector2 nameTagSize = new Vector2(300, 80);
    public Sprite itemSlotSprite;
    public ItemData itemToAdd;
}
