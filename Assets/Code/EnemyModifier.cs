using UnityEngine;
using System.Collections;

public class EnemyModifier : MonoBehaviour {
	
	public enum OnCreated {
		None,
		ScalingBadguy,
		IncreaseHealth,
		IncreaseScore,
		DualShot
	}
	public OnCreated[] StartUpModifiers;

	
	public int ScaleBy = 0;
	public int IncreasedHealth = 0;
	public int IncreaseScore = 0;
	public bool linkedScaleHealthScore = true;

	// Use this for initialization
	void Awake () {
		if (ScaleBy > 0) {
			transform.localScale.x = ScaleBy;
			transform.localScale.y = ScaleBy;
			transform.localScale.z = ScaleBy;
		}
	}

	public int HealthBoost() {
		if (linkedScaleHealthScore && IncreaseScore > IncreasedHealth && ScaleBy > IncreasedHealth) {

		} else if () {

		}
		return IncreasedHealth;
	}

}
