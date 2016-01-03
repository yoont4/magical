using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextBoxController : MonoBehaviour
{

    // The text to be displayed in the box.
    public string dialog;

    private Canvas canvas;

    private bool opening = false;

    private bool addingChars = false;

    [HideInInspector] public bool notClosed = false;

    private int dialogIndex = 0;

    private Text text;

    private int newlineCount;

    private bool addNewChar = true;

    // For when box is being opened.
    private float boxSize = 0.0025f;

    void Awake()
    {
        // index of the last spot where a newline was added
        int lastNewlineIndex = 0;

        newlineCount = 0;

        for (int i = 25; i < dialog.Length; i += 25)
        {

            // Go through while loop until you find first whitespace.
            int whiteSpaceLocator = i;
            while (whiteSpaceLocator != lastNewlineIndex && !char.IsWhiteSpace(dialog[whiteSpaceLocator - 1]))
            {
                whiteSpaceLocator--;
            }

            if (whiteSpaceLocator > lastNewlineIndex)
            {
                newlineCount++;
                dialog = dialog.Insert(whiteSpaceLocator, System.Environment.NewLine);
                lastNewlineIndex = whiteSpaceLocator + System.Environment.NewLine.Length;
                i++;
            }
        }

    }

    void Update()
    {
        if (dialogIndex == dialog.Length)
        {
            addingChars = false;
            dialogIndex = 0;
        }

        if (addingChars && addNewChar)
        {
            text.text += dialog[dialogIndex];
            dialogIndex++;
        }

        if (addingChars)
        {
            addNewChar = !addNewChar;
        }

        if (notClosed)
        {
            canvas.transform.position = transform.position + new Vector3(0f, 1f + (newlineCount * 0.2f), 0f);
        }

        if (opening)
        {
            canvas.transform.localScale = new Vector3(boxSize, boxSize, 1f);
            boxSize += 0.0025f;
            if (boxSize >= 0.03125)
            {

                // Make sure final size is exact
                canvas.transform.localScale = new Vector3(0.03125f, 0.03125f, 1f);

                opening = false;
                addingChars = true;
                boxSize = 0.0025f;
            }
        }
    }

    public void open()
    {
        //float y = ((transform.position.y + 1f + (newlineCount * 0.2f)) * 32f) / 32;
        Vector3 boxPosition = transform.position + new Vector3(0f, 1f + (newlineCount * 0.2f), 0f);
        //boxPosition = new Vector3(transform.position.x, y, 0f);
        GameObject canvasGameObject = (GameObject) Instantiate(Resources.Load("DialogCanvas"), boxPosition, Quaternion.identity);
        canvas = canvasGameObject.GetComponent<Canvas>();
        
        // for text crispness
        CanvasScaler scaler = canvas.GetComponent<CanvasScaler>();
        scaler.dynamicPixelsPerUnit = 8;
        scaler.referencePixelsPerUnit = 32;
        
        text = canvas.GetComponentInChildren<Text>();
        text.fontSize = 8;
        opening = true;
        notClosed = true;
    }

    public void close()
    {
        if (canvas != null)
        {
            Destroy(canvas.GetComponent<CanvasScaler>());
            Destroy(canvas.GetComponent<GraphicRaycaster>());
            Destroy(canvas);
        }

        notClosed = false;
    }
}