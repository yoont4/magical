using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextBoxController : MonoBehaviour
{

    public static int MAX_WIDTH = 160;

    // The text to be displayed in the box.
    public string dialog;

    public Font font;

    public int fontSize;

    private Canvas canvas;

    public void open()
    {
        // Will add in newlines to break up long text. This is the broken up version.
        string displayedDialog = dialog;

        // index of the last spot where a newline was added
        int lastNewlineIndex = 0;

        for (int i = 25; i < displayedDialog.Length; i += 25)
        {
            
            // Go through while loop until you find first whitespace.
            int whiteSpaceLocator = i;
            while (whiteSpaceLocator != lastNewlineIndex && !char.IsWhiteSpace(displayedDialog[whiteSpaceLocator - 1]))
            {
                whiteSpaceLocator--;
            }

            if (whiteSpaceLocator > lastNewlineIndex)
            {
                print("whitespace: " + whiteSpaceLocator);
                print("lastNewine: " + lastNewlineIndex);
                displayedDialog = displayedDialog.Insert(whiteSpaceLocator, System.Environment.NewLine);
                lastNewlineIndex = whiteSpaceLocator + System.Environment.NewLine.Length;
                i++;
            }
        }

        GameObject canvasGameObject = (GameObject) Instantiate(Resources.Load("DialogCanvas"), transform.position, Quaternion.identity);
        canvas = canvasGameObject.GetComponent<Canvas>();
        Text text = canvas.GetComponentInChildren<Text>();
        text.text = displayedDialog;
    }

    public void close()
    {
        if (canvas != null)
        {
            Destroy(canvas.GetComponent<CanvasScaler>());
            Destroy(canvas.GetComponent<GraphicRaycaster>());
            Destroy(canvas);
        }
    }
}