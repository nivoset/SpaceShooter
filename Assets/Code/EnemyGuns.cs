using UnityEngine;
using System.Collections;
	

public class EnemyGuns : MonoBehaviour
{	
	public GameObject shot;
	public Transform[] shotSpawn;
	public float fireRate = 3;
	private float nextFire;
	public int overheat = 3;

	private int howOverheated;
	
	void Update ()
	{
		if ((nextFire -= Time.deltaTime) <= 0 && fireRate > 0)
		{
			if (howOverheated > overheat){
				howOverheated = 0;
				nextFire = fireRate * (float) overheat;
			} else {
				howOverheated++;
				nextFire = fireRate;
			}
			if (shot != null) {
				for (int i = 0; i < shotSpawn.Length; i++){
					Instantiate(shot, shotSpawn[i].position, shotSpawn[i].rotation);
				}
			
				if (PlayerPrefs.HasKey ("sfx")) {
					if (PlayerPrefs.GetInt ("sfx") == 1)
						audio.Play ();
				}
			}
		}
	}
}