using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEditor.Progress;

public class GameManager : MonoBehaviour
{
    public static List<int> collectedItems = new List<int>();
    public RectTransform nameTag;

    public void UpdateNameTag(ItemData item)
    {
        nameTag.GetComponentInChildren<TextMeshProUGUI>().text = item.itemName;
        nameTag.sizeDelta = item.nameTagSize;
    }

    public void UpdateNameTag(BackgroundObjData obj)
    {
        nameTag.GetComponentInChildren<TextMeshProUGUI>().text = obj.objName;
        nameTag.sizeDelta = obj.nameTagSize;
    }
}
