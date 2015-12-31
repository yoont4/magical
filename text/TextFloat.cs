using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextFloat : MonoBehaviour {
	
	public float floatSpeed;
	public float floatLife;
	public float fadeSpeed;
	public float fadeTime;
	[HideInInspector] public float spawnTime;
	[HideInInspector] public int collidedTextCount = 0;

	[HideInInspector] public Text[] text;

	void Start() {
		spawnTime = Time.time;
		text = this.GetComponentsInChildren<Text> ();
	}

	// Update is called once per frame
	void Update () {
		floatSpeed -= floatSpeed*0.1f;
		transform.position = new Vector2 (transform.position.x, transform.position.y + floatSpeed*Time.deltaTime);
		if (Time.time - spawnTime >= fadeTime) {
			text[0].color = new Color (text[0].color.r, text[0].color.g, text[0].color.b, text[0].color.a - fadeSpeed * Time.deltaTime);
			text[1].color = new Color (text[1].color.r, text[1].color.g, text[1].color.b, text[1].color.a - fadeSpeed * Time.deltaTime);
		}
		if (Time.time - spawnTime >= floatLife) {
			Destroy (this.gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D col) {
		TextFloat enteredText = col.GetComponent<TextFloat> ();
		this.collidedTextCount++;
		col.enabled = false;
		enteredText.transform.position = new Vector2 (transform.position.x, transform.position.y + 0.3f * collidedTextCount);
	}
}
