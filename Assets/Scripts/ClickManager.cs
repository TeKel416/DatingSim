using UnityEngine;

public class ClickManager : MonoBehaviour
{
    public void TryGettingItem(ItemData item)
    {
        // pega o item se ele nao tiver requisito ou se o item requisitado ja estiver no inventario
        if (item.requiredItemID == -1 || GameManager.collectedItems.Contains(item.requiredItemID))
        {
            GameManager.collectedItems.Add(item.itemID);
            foreach (GameObject g in item.objectsToRemove)
            {
                Destroy(g);
            }
        }
    }
}
