using UnityEngine;
using System.Collections;

public class PlayerSFXController : MonoBehaviour {

	public AudioClip runningSound;
	public AudioClip stoppingSound;
	public AudioClip jumpingSound;
	public AudioClip landingSound;
	public AudioSource source;

	public void playRunningSound() {
		source.clip = runningSound;
		source.Play ();
	}

	public void playStoppingSound() {
		source.clip = stoppingSound;
		source.Play ();
	}

	public void playJumpingSound() {
		source.clip = jumpingSound;
		source.Play ();
	}

	public void playLandingSound() {
		source.clip = landingSound;
		source.Play ();
	}
}
