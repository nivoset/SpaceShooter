using UnityEngine;
using System.Collections;

public class DestroyByBoundary : MonoBehaviour {
	
	private GameController gameController;

	void Start()
	{
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null)
			gameController = gameControllerObject.GetComponent<GameController>();
	}
	void OnTriggerExit(Collider other) {

		
		var HitBy = other.GetComponent<OnContact> ();

		if (HitBy != null)
			gameController.SubtractScore (HitBy.PointsToAdd/2);
		else if (other.tag == "Enemy") 
			gameController.SubtractScore (5);

		Destroy(other.gameObject);
	}
}
