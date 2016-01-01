using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBarUpdate : MonoBehaviour {

	private CreatureBehavior target;
	private Slider healthSlider;
	private Canvas healthCanvas;
	private int startHealth;

	// Use this for initialization
	void Start () {
		// instantiate starting health and creature reference
		target = this.transform.parent.GetComponentInChildren<CreatureBehavior>();
		startHealth = target.health;
		// instantiate slider and starting value
		healthSlider = this.GetComponentInChildren<Slider> ();
		healthSlider.value = 1f;	// start full
		healthCanvas = this.GetComponent<Canvas>();
	}
	
	// Update is called once per frame
	void Update () {
		// should display current health out of total health
		healthSlider.value = ((float)target.health) / ((float)startHealth);
		Vector3 position = new Vector3(target.transform.position.x, target.transform.position.y + 1f, target.transform.position.z);
		healthCanvas.transform.position = position;
	}
}
