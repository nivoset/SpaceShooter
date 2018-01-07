using UnityEngine;
using System.Collections;

public class OnContact : MonoBehaviour {

	public Transform destroyEffect;
	public int PointsToAdd = 10;
	private GameController gameController;

	public int MaxHealth = 1;
	public int Damage = 1;
	private int currentHealth;
	private OnContact HitBy;
	
	public enum TypeOfContact {
		Player,
		PlayerProjectile,
		Enemy,
		EnemyProjectile,
		PowerUp
	}

	public enum OnDestroyed {
		None,
		ExtraLife,
		ScalingBadguy,
		DualShot
	}

	public TypeOfContact Type = TypeOfContact.Enemy;
	public OnDestroyed DestroyBonus = OnDestroyed.None;
	
	public TypeOfContact ContactType(){
		return Type;
	}
	
	public int DealDamage (){
		return Damage;
	}
	
	public void TakeDamage (int damageDone){
		currentHealth -= damageDone;
		if (currentHealth <= 0) 
			Death ();
	}

	void Death(){

		Destroy (gameObject);
		if (destroyEffect != null)
			Instantiate (destroyEffect, transform.position, transform.rotation);

		if (Type == TypeOfContact.Player) {
			gameController.die ();
		} else {
			//If the other tag is Player Generated, then grant points.
			
			if (transform.localScale.x > 1) {
				PointsToAdd = (int) transform.localScale.x * PointsToAdd;
			}
			if (HitBy.ContactType() == TypeOfContact.PlayerProjectile) {				
				if (PointsToAdd > 0) {
					gameController.AddScore (PointsToAdd);
				} else {
					gameController.SubtractScore (PointsToAdd);
				}
			}
		}

	}

	//public Texture startButton;

	void Start()
	{
		currentHealth = MaxHealth;
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null)
			gameController = gameControllerObject.GetComponent<GameController>();

		if (gameController == null)
			Debug.Log ("Could not find a 'gameController' script");

	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Boundary") 
			return;
		if (other.tag == transform.tag)
			return;

		HitBy = other.GetComponent<OnContact> ();

		//What am i and what will i do!
		switch (Type) {
			case TypeOfContact.PlayerProjectile:
				TakeDamage(HitBy.DealDamage());
				break;

			case TypeOfContact.Player:
			if ((HitBy.ContactType () == TypeOfContact.EnemyProjectile) || 
			    (HitBy.ContactType () == TypeOfContact.Enemy)) {
					TakeDamage(HitBy.DealDamage());
				}
				
				break;
			case TypeOfContact.Enemy:
			case TypeOfContact.EnemyProjectile:
			if ((HitBy.ContactType () == TypeOfContact.PlayerProjectile) || 
			    (HitBy.ContactType () == TypeOfContact.Player)) {
					TakeDamage(HitBy.DealDamage());
				}
				break;

			case TypeOfContact.PowerUp:
				if (HitBy.ContactType () == TypeOfContact.Player) {
					switch (DestroyBonus){
					case OnDestroyed.ExtraLife:
						gameController.AddLife(1);
						Death();
						break;
					}
				} else if (HitBy.ContactType () == TypeOfContact.PlayerProjectile) {
					TakeDamage(HitBy.DealDamage());
				}
				break;
		}


	}

}
