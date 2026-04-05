using UnityEngine;
using UnityEngine.UI;

public class ExplorationManager : MonoBehaviour
{
    [Header("TextBox")]
    public GameObject nameTextBox, textBox;

    public void UpdateTextBox(BackgroundObjData obj)
    {
        nameTextBox.GetComponent<Text>().text = obj.objName;
        textBox.GetComponent<Text>().text = obj.objDescription;
    }
}
