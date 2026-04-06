using UnityEngine;
using UnityEngine.UI;

public class ClickManager : MonoBehaviour
{
    GameManager gameManager;
    public GameObject storyCanvas, nameTextBox, textBox;

    private void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
    }

    public void TryGettingItem(ItemData item)
    {
        // pega o item se ele nao tiver requisito ou se o item requisitado ja estiver no inventario
        if (item.requiredItemID == -1 || gameManager.selectedItemID == item.requiredItemID)
        {
            GameManager.collectedItems.Add(item);
            foreach (GameObject g in item.objectsToRemove)
            {
                Destroy(g);
            }

            gameManager.UpdateEquipmentCanvas();
        }
    }

    public void TryUsingItem(ItemData item)
    {
        if (item.requiredItemID == -1 || gameManager.selectedItemID == item.requiredItemID)
        {
            foreach (GameObject g in item.objectsToRemove)
            {
                Destroy(g);
            }

            GameManager.collectedItems.Remove(GameManager.collectedItems[item.requiredItemID]);
            gameManager.UpdateEquipmentCanvas();
        }
        else
        {
            nameTextBox.GetComponent<Text>().text = item.itemName;
            textBox.GetComponent<Text>().text = item.hint;
            storyCanvas.SetActive(true);
        }
    }
}
