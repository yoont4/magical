using UnityEngine;
using System.Collections;

/**
 * test/mock controller class to experiment with dialog control
 */
public class _DialogMockTrigger : MonoBehaviour {
    public TextBoxController textController;
    public _DialogMockController dialogController;
    void OnMouseDown() {
        if (textController.notClosed) {
            textController.close();
            dialogController.trigger();
        } else {
            textController.open();

            StartCoroutine(trigger());
        }
    }

    IEnumerator trigger() {
        yield return new WaitForSeconds(2f);
        dialogController.trigger();
    }
}
