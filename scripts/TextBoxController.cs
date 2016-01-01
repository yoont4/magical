using UnityEngine;
using System.Collections;

public class TextBoxController : MonoBehaviour
{
    // The text to be displayed in the box.
    public string script;

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
        if (displayText)
        {
            Vector3 convertedObjectPosition = gameCamera.WorldToScreenPoint(objectPosition + new Vector3(0, 0, 10));
            GUI.Box(new Rect(convertedObjectPosition.x - 45, (Screen.height - convertedObjectPosition.y) - 125, 100, 100), script);
        }
    }

    public void open()
    {
        displayText = true;
    }

    public void close()
    {
        displayText = false;
    }
}