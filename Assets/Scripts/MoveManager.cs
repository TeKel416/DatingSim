using System;
using UnityEngine;
using UnityEngine.UI;

public class MoveManager : MonoBehaviour
{
    GameManager gameManager;
    ClickManager clickManager;

    [Header("Locais")]
    public LocationData activeLocation;
    private LocationData leftLocation, rightLocation;
    public Button goLeftBtn, goRightBtn;

    private void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        clickManager = FindFirstObjectByType<ClickManager>();

        activeLocation.gameObject.SetActive(true);
        goLeftBtn.onClick.AddListener(GoLeft);
        goRightBtn.onClick.AddListener(GoRight);
        UpdateMoveButtons();
    }

    public void UpdateMoveButtons()
    {
        leftLocation = activeLocation.leftLocation;
        rightLocation = activeLocation.rightLocation;

        goLeftBtn.gameObject.SetActive(leftLocation != null);
        goRightBtn.gameObject.SetActive(rightLocation != null);
    }

    public void GoLeft()
    {
        // se nao precisar de item ou estiver destrancado
        if (!leftLocation.isLocked)
        {
            leftLocation.gameObject.SetActive(true);
            activeLocation.gameObject.SetActive(false);
            activeLocation = leftLocation;
            UpdateMoveButtons();
        }
        // se precisar e tiver o item
        else if (gameManager.selectedItemID == leftLocation.requiredItemID)
        {
            leftLocation.isLocked = false;
            GameManager.collectedItems.Remove(GameManager.collectedItems[leftLocation.requiredItemID]);
            gameManager.UpdateEquipmentCanvas();

            clickManager.nameTextBox.GetComponent<Text>().text = "Você";
            clickManager.textBox.GetComponent<Text>().text = leftLocation.successMsg;
            clickManager.storyCanvas.SetActive(true);
        }
        // se precisar e não tiver o item
        else
        {
            clickManager.nameTextBox.GetComponent<Text>().text = "Você";
            clickManager.textBox.GetComponent<Text>().text = leftLocation.hint;
            clickManager.storyCanvas.SetActive(true);
        }
    }

    public void GoRight()
    {
        // se nao precisar de item ou estiver destrancado
        if (!rightLocation.isLocked)
        {
            rightLocation.gameObject.SetActive(true);
            activeLocation.gameObject.SetActive(false);
            activeLocation = rightLocation;
            UpdateMoveButtons();
        }
        // se precisar e tiver o item
        else if (gameManager.selectedItemID == rightLocation.requiredItemID)
        {
            rightLocation.isLocked = false;
            GameManager.collectedItems.Remove(GameManager.collectedItems[gameManager.lastSlotClickedID]);
            gameManager.UpdateEquipmentCanvas();

            clickManager.nameTextBox.GetComponent<Text>().text = "Você";
            clickManager.textBox.GetComponent<Text>().text = rightLocation.successMsg;
            clickManager.storyCanvas.SetActive(true);
        }
        // se precisar e não tiver o item
        else
        {
            clickManager.nameTextBox.GetComponent<Text>().text = "Você";
            clickManager.textBox.GetComponent<Text>().text = rightLocation.hint;
            clickManager.storyCanvas.SetActive(true);
        }
    }
}
