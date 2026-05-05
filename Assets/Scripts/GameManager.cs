using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    public int lastSlotClickedID = -1, selectedItemID = -1;

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

    public void SelectItem(int slotID)
    {
        Color emptyColor = Color.white;
        emptyColor.a = 0;

        // selecionar campo vazio
        // save changes and stop if an empty slot is clicked or the last item is removed
        if (slotID >= collectedItems.Count || slotID < 0)
        {
            // no items selected
            DeselectSlot(lastSlotClickedID);
            selectedItemID = -1;
        }
        // deselecionar o mesmo item
        else if (lastSlotClickedID == slotID && equipmentSlots[slotID].color == selectedItemColor)
        {
            DeselectSlot(slotID);
            selectedItemID = -1;
        }
        else {
            // verifica se o novo item selecionado e o item anterior se combinam
            if (lastSlotClickedID != -1 
                && collectedItems[slotID].itemToCombineID == collectedItems[lastSlotClickedID].itemID)
            {
                ItemData newItem = clickManager.CombineItems(slotID, lastSlotClickedID);

                UpdateEquipmentCanvas();

                // selecionar novo item criado
                for (int i = 0; i < collectedItems.Count; i++)
                {
                    if (newItem.itemID == collectedItems[i].itemID)
                    {
                        DeselectSlot(lastSlotClickedID);
                        SelectSlot(i);
                    }
                }
            }
            else
            {
                // selecionar item
                if (lastSlotClickedID != -1) { DeselectSlot(lastSlotClickedID); }
                SelectSlot(slotID);
            }
        }
    }

    private void SelectSlot(int slotID)
    {
        equipmentSlots[slotID].color = selectedItemColor;
        selectedItemID = collectedItems[slotID].itemID;
        lastSlotClickedID = slotID;
    }

    private void DeselectSlot(int slotID)
    {
        Color emptyColor = Color.white;
        emptyColor.a = 0;

        equipmentSlots[slotID].color = emptyColor;
        lastSlotClickedID = -1;
    }

    public void ShowItemName(int slotID)
    {
        if (slotID < collectedItems.Count)
        {
            UpdateNameTag(collectedItems[slotID]);
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
    }
}
