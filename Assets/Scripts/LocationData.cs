using UnityEngine;
using UnityEngine.Events;

public class LocationData : MonoBehaviour
{
    public LocationData leftLocation, rightLocation;

    [Header("Bloqueio")]
    public bool isLocked = false;
    public int requiredItemID = -1;
    public string hint;
    public string successMsg;

    [Header("História")]
    public bool removeOnEnable = true;
    [SerializeField] UnityEvent onEnable;

    private void OnEnable()
    {
        if (onEnable != null) onEnable.Invoke();

        if (removeOnEnable)
        {
            onEnable = null;
        }
    }
}
