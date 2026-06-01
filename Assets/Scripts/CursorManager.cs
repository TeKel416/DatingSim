using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField] private Texture2D normalCursor, lockedCursor, travelCursor;
    private Vector2 cursorHotspot;

    private void Start()
    {
        ChangeCursor("normal");
    }

    public void ChangeCursor(string cursorState)
    {
        switch (cursorState)
        {
            case "locked":
                cursorHotspot = new Vector2(lockedCursor.width / 2, lockedCursor.height / 2);
                Cursor.SetCursor(lockedCursor, cursorHotspot, CursorMode.Auto);
                Cursor.visible = true;
                break;

            case "travel":
                cursorHotspot = new Vector2(travelCursor.width / 2, travelCursor.height / 2);
                Cursor.SetCursor(travelCursor, cursorHotspot, CursorMode.Auto);
                Cursor.visible = true;
                break;

            case "none":
                cursorHotspot = new Vector2(travelCursor.width / 2, travelCursor.height / 2);
                Cursor.SetCursor(null, cursorHotspot, CursorMode.Auto);
                Cursor.visible = false;
                break;

            default:
                cursorHotspot = new Vector2(normalCursor.width / 2, normalCursor.height / 2);
                Cursor.SetCursor(null, cursorHotspot, CursorMode.Auto);
                Cursor.visible = true;
                break;
        }
    }
}
