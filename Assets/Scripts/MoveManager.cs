using UnityEngine;
using UnityEngine.UI;

public class MoveManager : MonoBehaviour
{
    [Header("Locais")]
    public GameObject activeLocation;
    private GameObject leftLocation, rightLocation;
    public Button goLeftBtn, goRightBtn;

    private void Start()
    {
        activeLocation.SetActive(true);
        goLeftBtn.onClick.AddListener(GoLeft);
        goRightBtn.onClick.AddListener(GoRight);
        UpdateMoveButtons();
    }

    public void UpdateMoveButtons()
    {
        leftLocation = activeLocation.GetComponent<LocationData>().leftLocation;
        rightLocation = activeLocation.GetComponent<LocationData>().rightLocation;

        goLeftBtn.gameObject.SetActive(leftLocation != null);
        goRightBtn.gameObject.SetActive(rightLocation != null);
    }

    public void GoLeft()
    {
        leftLocation.SetActive(true);
        activeLocation.SetActive(false);
        activeLocation = leftLocation;
        UpdateMoveButtons();
    }

    public void GoRight()
    {
        rightLocation.SetActive(true);
        activeLocation.SetActive(false);
        activeLocation = rightLocation;
        UpdateMoveButtons();
    }
}
