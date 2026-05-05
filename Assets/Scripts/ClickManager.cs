using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using VNCreator;

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
            AddNewItem(item);

            foreach (GameObject g in item.objectsToRemove)
            {
                Destroy(g);
            }

            gameManager.UpdateEquipmentCanvas();

            if (item.successMsg != "")
            {
                nameTextBox.GetComponent<Text>().text = "Você";
                textBox.GetComponent<Text>().text = item.successMsg;
                item.hint = item.successMsg;
                storyCanvas.SetActive(true);
            }
        }
    }

    public void TryUsingItem(ItemData item)
    {
        if (item.requiredItemID == -1 || gameManager.selectedItemID == item.requiredItemID)
        {
            if (item.pickupSfx) { VNCreator_SfxSource.Instance.PlaySound2D(item.pickupSfx); }

            foreach (GameObject g in item.objectsToRemove)
            {
                Destroy(g);
            }

            GameManager.collectedItems.Remove(GameManager.collectedItems[item.requiredItemID]);
            gameManager.UpdateEquipmentCanvas();

            if (item.successMsg != null)
            {
                nameTextBox.GetComponent<Text>().text = item.itemName;
                textBox.GetComponent<Text>().text = item.successMsg;
                item.hint = item.successMsg;
                storyCanvas.SetActive(true);
            }

            if (item.itemToAdd != null) 
            {
                AddNewItem(item.itemToAdd);
            }

            // APENAS PRA ESSE J2
            if (item.itemID == 11)
            {
                SceneManager.LoadScene("EndScreen");
            }
        }
        else
        {
            nameTextBox.GetComponent<Text>().text = item.itemName;
            textBox.GetComponent<Text>().text = item.hint;
            storyCanvas.SetActive(true);
        }
    }

    public ItemData CombineItems(int slotID1, int slotID2)
    {
        ItemData newItem = GameManager.collectedItems[slotID1].itemToAdd;
        AddNewItem(newItem);

        if (slotID1 > slotID2)
        {
            GameManager.collectedItems.Remove(GameManager.collectedItems[slotID1]);
            GameManager.collectedItems.Remove(GameManager.collectedItems[slotID2]);
        }
        else
        {
            GameManager.collectedItems.Remove(GameManager.collectedItems[slotID2]);
            GameManager.collectedItems.Remove(GameManager.collectedItems[slotID1]);
        }

        return newItem;
    }

    public void AddNewItem(ItemData item)
    {
        GameManager.collectedItems.Add(item);
        
        if (item.pickupSfx) { VNCreator_SfxSource.Instance.PlaySound2D(item.pickupSfx); }

        gameManager.UpdateEquipmentCanvas();
        Destroy(item.gameObject);
    }
}
