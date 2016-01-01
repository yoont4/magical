using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextBoxController : MonoBehaviour
{

    // The text to be displayed in the box.
    public string dialog;

    private Canvas canvas;

    private bool opened = false;

    private bool notClosed = false;

    private int dialogIndex = 0;

    private Text text;

    private int newlineCount;

    private bool addNewChar = true;

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
            opened = false;
            dialogIndex = 0;
        }

        if (opened && addNewChar)
        {
            text.text += dialog[dialogIndex];
            dialogIndex++;
        }

        if (opened)
        {
            addNewChar = !addNewChar;
        }

        if (notClosed)
        {
            canvas.transform.position = transform.position + new Vector3(0f, 1f + (newlineCount * 0.2f), 0f);
        }
    }

    public void open()
    {
        Vector3 boxPosition = transform.position + new Vector3(0f, 1f + (newlineCount * 0.2f), 0f);
        GameObject canvasGameObject = (GameObject) Instantiate(Resources.Load("DialogCanvas"), boxPosition, Quaternion.identity);
        canvas = canvasGameObject.GetComponent<Canvas>();
        text = canvas.GetComponentInChildren<Text>();
        opened = true;
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