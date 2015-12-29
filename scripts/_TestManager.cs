using UnityEngine;
using System.Collections;

public class _TestManager : MonoBehaviour {

	public int enemiesKilled = 0;
	public float shake;
	private bool shaking = false;
	private float shakeFactor;

	public Camera cam;
	public AudioClip[] killSounds;
	public AudioSource soundSource;

	void Update() {
		if (shaking) {
			cam.transform.localPosition = Random.insideUnitCircle * shakeFactor;
			shakeFactor -= 0.2f;
			if (shakeFactor <= 0.2f) {
				shaking = false;
				shakeFactor = 0;
				cam.transform.localPosition = Vector2.zero;
			}
		}

	}

	
	public void killEnemy() {
		enemiesKilled++;
		if (enemiesKilled == 1) {
			soundSource.clip = killSounds [0];
			soundSource.Play();
		} else if (enemiesKilled == 2) {
			soundSource.clip = killSounds [1];
			soundSource.Play();
		} else if (enemiesKilled == 3) {
			soundSource.clip = killSounds [2];
			soundSource.Play();
		} else if (enemiesKilled == 4) {
			soundSource.clip = killSounds [3];
			soundSource.Play();
		} else if (enemiesKilled == 5) {
			soundSource.clip = killSounds [4];
			soundSource.Play();
		} else if (enemiesKilled == 6) {
			soundSource.clip = killSounds [5];
			soundSource.Play();
		} else if (enemiesKilled == 7) {
			soundSource.clip = killSounds [6];
			soundSource.Play();
		} else if (enemiesKilled == 8) {
			soundSource.clip = killSounds [7];
			soundSource.Play();
		} else if (enemiesKilled == 9) {
			soundSource.clip = killSounds [8];
			soundSource.Play();
		}

		shaking = true;
		shakeFactor = shake;
	}
}
