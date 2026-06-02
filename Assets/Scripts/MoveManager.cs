using System;
using UnityEngine;
using UnityEngine.UI;

public class MoveManager : MonoBehaviour
{
    GameManager gameManager;
    ClickManager clickManager;

    [Header("Locais")]
    public LocationData activeLocation, lastLocation;
    public Button goBackBtn;

    private void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        clickManager = FindFirstObjectByType<ClickManager>();

        activeLocation.gameObject.SetActive(true);

        goBackBtn.onClick.AddListener(GoBack);
    }
    public void GoBack()
    {
        if (clickManager.storyCanvas.activeInHierarchy == false)
        {
            lastLocation.gameObject.SetActive(true);
            activeLocation.gameObject.SetActive(false);

            activeLocation = lastLocation;

            SetActiveGoBackBtn(false);
        }
    }

    public void GoToLocation(LocationData location)
    {
        // se nao precisar de item ou estiver destrancado, vai para o local
        if (!location.isLocked && clickManager.storyCanvas.activeInHierarchy == false)
        {
            location.gameObject.SetActive(true);
            activeLocation.gameObject.SetActive(false);

            lastLocation = activeLocation;
            activeLocation = location;

            SetActiveGoBackBtn(true);
        }
        // se precisar e tiver o item
        else if (gameManager.selectedItemID == location.requiredItemID)
        {
            location.isLocked = false;
            GameManager.collectedItems.Remove(GameManager.collectedItems[gameManager.lastSlotClickedID]);
            gameManager.UpdateEquipmentCanvas();

            clickManager.nameTextBox.GetComponent<Text>().text = "Você";
            clickManager.textBox.GetComponent<Text>().text = location.successMsg;
            clickManager.storyCanvas.SetActive(true);
        }
        // se precisar e não tiver o item
        else
        {
            clickManager.nameTextBox.GetComponent<Text>().text = "Você";
            clickManager.textBox.GetComponent<Text>().text = location.hint;
            clickManager.storyCanvas.SetActive(true);
        }
    }

    public void SetActiveGoBackBtn(bool state)
    {
        goBackBtn.gameObject.SetActive(state);
    }
}
