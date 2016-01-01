using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextArc : MonoBehaviour {

	public float arcSpeed;
	public float arcLife;
	public float fadeSpeed;
	public float fadeTime;
	[HideInInspector] public float spawnTime;

	[HideInInspector] public Text[] text;

	public float arcSize;
	private float xOffset = 1f;	// y = -a(x+b)^2 + c
	private float yOffset = 1.3f;
	private float xStart;
	private float yStart;
	private float x = 0;
	private float y = 0;


	void Start() {
		xStart = transform.position.x;
		yStart = transform.position.y;
		spawnTime = Time.time;
		text = this.GetComponentsInChildren<Text> ();
	}

	// Update is called once per frame
	void Update () {
		arcSpeed = arcSpeed*0.9f;
		x += 0.18f * arcSpeed;
		y = -arcSize * Mathf.Pow ((x - xOffset), 2) + yOffset;
		transform.position = new Vector2 (xStart + x*0.75f, yStart + y + 0.1f);

		if (Time.time - spawnTime >= fadeTime) {
			text[0].color = new Color (text[0].color.r, text[0].color.g, text[0].color.b, text[0].color.a - fadeSpeed * Time.deltaTime);
			text[1].color = new Color (text[1].color.r, text[1].color.g, text[1].color.b, text[1].color.a - fadeSpeed * Time.deltaTime);
		}
		if (Time.time - spawnTime >= arcLife) {
			Destroy (this.gameObject);
		}
	}
}
