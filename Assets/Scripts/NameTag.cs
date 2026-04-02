using UnityEngine;

public class NameTag : MonoBehaviour
{
    public float offsetX, offsetY;
    
    void LateUpdate()
    {
        FollowMouse();
    }

    private void FollowMouse()
    {
        transform.position = new Vector2(Input.mousePosition.x + offsetX, Input.mousePosition.y + offsetY);
    }
}
