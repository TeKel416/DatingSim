using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class GameManager : MonoBehaviour
{
    ClickManager clickManager;
    public static List<ItemData> collectedItems = new List<ItemData>();
    public RectTransform nameTag;

    [Header("Equipment")]
    public GameObject equipmentCanvas;
    public Image[] equipmentSlots, equipmentImages;
    public Sprite emptyItemSlotSprite;
    public Color selectedItemColor;
    public int selectedCanvasSlotID = 0, selectedItemID = -1;

    private void Start()
    {
        clickManager = FindFirstObjectByType<ClickManager>();
    }

    public void UpdateNameTag(ItemData item)
    {
        nameTag.parent.gameObject.SetActive(true);
        nameTag.GetComponentInChildren<TextMeshProUGUI>().text = item.itemName;
        nameTag.sizeDelta = item.nameTagSize;
    }

    public void UpdateNameTag(BackgroundObjData obj)
    {
        nameTag.parent.gameObject.SetActive(true);
        nameTag.GetComponentInChildren<TextMeshProUGUI>().text = obj.objName;
        nameTag.sizeDelta = obj.nameTagSize;
    }

    public void SelectItem(int equipmentCanvasID)
    {
        Color c = Color.white;
        c.a = 0;

        // deselecionar o mesmo item
        if (selectedCanvasSlotID == equipmentCanvasID && equipmentSlots[equipmentCanvasID].color == selectedItemColor)
        {
            equipmentSlots[equipmentCanvasID].color = c;
            selectedItemID = -1;
            selectedCanvasSlotID = 0;
            return;
        }

        // change the alpha of the previous slot to 0
        equipmentSlots[selectedCanvasSlotID].color = c;

        // selecionar campo vazio
        // save changes and stop if an empty slot is clicked or the last item is removed
        if (equipmentCanvasID >= collectedItems.Count || equipmentCanvasID < 0)
        {
            // no items selected
            selectedItemID = -1;
            selectedCanvasSlotID = 0;
            return;
        }

        Debug.Log(collectedItems[equipmentCanvasID].itemName);
        /*
        if (collectedItems[equipmentCanvasID].itemToCombineID == collectedItems[selectedCanvasSlotID].itemID)
        {
            clickManager.CombineItem(collectedItems[selectedCanvasSlotID]);

            // no items selected
            equipmentSlots[equipmentCanvasID].color = c;
            selectedItemID = -1;
            selectedCanvasSlotID = 0;

            UpdateEquipmentCanvas();
        }
        else
        {
            // selecionar item
            // change the alpha of the new slot to the select color
            equipmentSlots[equipmentCanvasID].color = selectedItemColor;
            selectedItemID = collectedItems[selectedCanvasSlotID].itemID;
            selectedCanvasSlotID = equipmentCanvasID;
        }*/

        // selecionar item
        // change the alpha of the new slot to the select color
        equipmentSlots[equipmentCanvasID].color = selectedItemColor;
        selectedItemID = collectedItems[selectedCanvasSlotID].itemID;
        selectedCanvasSlotID = equipmentCanvasID;
    }

    public void ShowItemName(int equipmentCanvasID)
    {
        if (equipmentCanvasID < collectedItems.Count)
        {
            UpdateNameTag(collectedItems[equipmentCanvasID]);
        }
    }

    public void UpdateEquipmentCanvas()
    {
        // find out how many items we have
        int itemsAmount = collectedItems.Count, itemSlotsAmount = equipmentSlots.Length;
        // replace no item sprites and old sprites with collectedItems[x].itemImage
        for (int i = 0; i < itemSlotsAmount; i++) 
        { 
            // choose between no item image and item sprite
            if (i < itemsAmount)
            {
                equipmentImages[i].sprite = collectedItems[i].itemSlotSprite;
            }
            else 
            {
                equipmentImages[i].sprite = emptyItemSlotSprite;
            }
        }

        // add special conditions for selecting items
        if (itemsAmount == 0)
        {
            SelectItem(-1);
        }
    }
}
