using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour {
	private string whatSetting = "music";
	public AudioSource Sound;
	// Use this for initialization
	void Start () {
		if (PlayerPrefs.HasKey (whatSetting)) {
			if (PlayerPrefs.GetInt (whatSetting) == 1) {
				audio.Play ();
			} else {
				audio.Stop ();
			}
		}
	}

	void Update(){
		if (PlayerPrefs.HasKey (whatSetting)) {
			if (PlayerPrefs.GetInt (whatSetting) == 1) {
				if (Sound.isPlaying == false)
					audio.Play ();
			} else {
				audio.Stop ();
			}
		}
	}
}