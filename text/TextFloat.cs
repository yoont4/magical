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

	private Text text;

	void Start() {
		spawnTime = Time.time;
		text = this.GetComponentInChildren<Text> ();

	}

	// Update is called once per frame
	void Update () {
		floatSpeed -= floatSpeed*0.1f;
		transform.position = new Vector2 (transform.position.x, transform.position.y + floatSpeed*Time.deltaTime);
		if (Time.time - spawnTime >= fadeTime) {
			text.color = new Color (text.color.r, text.color.g, text.color.b, text.color.a - fadeSpeed * Time.deltaTime);
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
		//transform.position = new Vector2 (transform.position.x, transform.position.y + 0.3f);
	}
}
