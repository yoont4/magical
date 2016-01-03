using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/**
 * test/mock controller class to experiment with dialog control
 */
public class _DialogMockController : MonoBehaviour {

    private string dialog;
    private Text text;
    public float printTime = 0.01f;
    private float lastPrint = 0;
    private int i = 0;

    public bool triggered = false;
    private float fadeDist = 10f;

    private Image bust;
    private float startX;

	// Use this for initialization
	void Start () {
        text = this.GetComponent<Text>();
        dialog = text.text;
        text.text = "";
        bust = transform.parent.GetComponentsInChildren<Image>()[1];
        bust.enabled = false;
        startX = bust.transform.position.x;
	}
	
	// Update is called once per frame
	void Update () {
        if (i != dialog.Length) {
            if (triggered) {
                fadeDist *= 0.95f;
                bust.color = new Color(1,1,1,bust.color.a + 0.05f);
                bust.transform.position = new Vector2(startX - fadeDist, bust.transform.position.y);
            }
        }
	}

    public void trigger() {
        triggered = !triggered;
        bust.color = new Color(1,1,1,0);
        bust.enabled = true;
        InvokeRepeating("addCharacter", 0, 0.05f);
    }

    public void addCharacter() {
        if (i == dialog.Length) {
            CancelInvoke();
        }
        lastPrint = Time.time;
        text.text += dialog[i];
        i++;
    }
}
