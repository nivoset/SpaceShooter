using UnityEngine;
using System.Collections;

public class DestroyEffects : MonoBehaviour {
	private string whatSetting = "sfx";
	public AudioClip Sound;
	// Use this for initialization
	void Start () {
		if (PlayerPrefs.HasKey (whatSetting)) {
			if (PlayerPrefs.GetInt (whatSetting) == 1) {
				AudioSource.PlayClipAtPoint(Sound, transform.position);
			}
		}
	}
}
