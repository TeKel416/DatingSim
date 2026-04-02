using UnityEngine;

public class ItemData : MonoBehaviour
{
    public int itemID, requiredItemID;
    public string itemName;
    public GameObject[] objectsToRemove;
    public Vector2 nameTagSize = new Vector2(300, 80);
}
