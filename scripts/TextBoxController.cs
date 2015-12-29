using UnityEngine;
using System.Collections;

public class TextBoxController : MonoBehaviour
{
    // The text to be displayed in the box.
    public string script;

    // true if gameObject was clicked on. False otherwise.
    private bool objectClicked = false;

    // true if textbox should be displayed. False otherwise.
    private bool displayText = false;

    // Position of the object in world-space
    private Vector3 objectPosition;

    private Camera gameCamera;

    void Awake()
    {
        objectPosition = gameObject.transform.position;
        gameCamera = (Camera) FindObjectOfType(typeof(Camera));
    }

    void OnGUI()
    {
        Event e = Event.current;
        if (e.type == EventType.MouseUp && e.button == 0 && objectClicked)
        {
            objectClicked = false;
            displayText = true;
        } else if (isClicked(e))
        {
            objectClicked = true;
        }

        if (displayText)
        {
            Vector3 convertedObjectPosition = gameCamera.WorldToScreenPoint(objectPosition + new Vector3(0, 0, 10));
            GUI.Box(new Rect(convertedObjectPosition.x - 45, convertedObjectPosition.y - 125, 100, 100), script);
        }
    }

    // Returns true if gameObject was clicked. False otherwise.
    private bool isClicked(Event e)
    {
        Vector3 spriteDimensions = GetComponent<SpriteRenderer>().sprite.bounds.size;
        Vector3 convertedMousePosition = gameCamera.ScreenToWorldPoint((Vector3) e.mousePosition + new Vector3(0, 0, 10));

        return e.type == EventType.MouseDown &&
                         Mathf.Abs(convertedMousePosition.x - objectPosition.x) <= spriteDimensions.x &&
                         Mathf.Abs(convertedMousePosition.y - objectPosition.y) <= spriteDimensions.y;
    }
}