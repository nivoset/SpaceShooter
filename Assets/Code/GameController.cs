using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
	public GameObject[] hazard;
	public GameObject[] badShip;

	public Vector3 spawnValues;
	public int hazardCount;

	public PlayerController Player;
	public Transform spawnLocation;

	public float spawnWait;
	public float startWait;
	public float waveWait;

	public GUIText scoreText;
	public GUIText livesText;
	public GUIText highScoreText;

	public TextMesh GameOver;

	private bool isDead;
	private bool restart = false;

	private int waveNumber = 0;

	private int TextSize = 20;
	private int Score;
	public int startingLives = 3;
	private int lives;
	
	//private string text = "Pause";
	
	public Texture PauseButton;

	void OnGUI() {
		if (GUI.Button (new Rect (Screen.width - 130, Screen.height - 60, 120, 50), PauseButton, new GUIStyle()))
			Pause ();
		
	}
	
	void Pause(){
		if (Time.timeScale == 0.0f) {
			//text = "Pause";
			Time.timeScale = 1.0f;
		} else {
			//text = "Resume";
			Time.timeScale = 0.0f;
		}
	}

	void Start ()
	{
		Score = 0;
		lives = startingLives;
		UpdateScore ();
		UpdateLives ();
		StartCoroutine (SpawnWaves ());

		if (highScoreText != null) {
			highScoreText.fontSize = TextSize;
			if (PlayerPrefs.HasKey ("highscore")) {
				highScoreText.text = "High Score: " + PlayerPrefs.GetInt ("highscore");
			} else {
				highScoreText.text = " ";
			}
		}

		if (PlayerPrefs.HasKey("screenLimits")) {
			spawnValues.x = PlayerPrefs.GetFloat ("screenLimits"); //, spawnValues.y, spawnValues.z);
		}
	}

	void UpdateScore()
	{
		scoreText.fontSize = TextSize;
		if (scoreText != null)
			scoreText.text = "Score: " + Score;
	}

	void UpdateLives()
	{
		livesText.fontSize = TextSize;
		if (livesText != null)
			livesText.text = lives + " Lives";
	}
	
	public void AddScore(int newPoints)
	{
		Score += newPoints;
		UpdateScore ();
	}
	
	public void SubtractScore(int lostPoints)
	{
		Score -= lostPoints;
		if (Score < 0)
			Score = 0;
		UpdateScore ();
	}

	public void AddLife(int LivesToAdd)
	{
		lives += LivesToAdd;
		UpdateLives ();
	}

	void Update()
	{
		if (isDead && restart && Input.GetButton("Fire1")) 
		{
			Application.LoadLevel ("Menu");
			restart = false;

		}
	}

	public void die()
	{
		lives--;
		if (lives < 1) {
			GameOver.transform.position = new Vector3 (0, 0, 3);
			isDead = true;
		} else {
			GameOver.transform.position = new Vector3 (0, 0, 16);
			isDead = false;
		}

		if (lives < 0)
			lives = 0;

		UpdateLives ();
		isDead = true;
	}

	IEnumerator SpawnWaves ()
	{
		while ( isDead == false ) {
			yield return new WaitForSeconds (startWait);

			for (int i = 0; i < hazardCount + waveNumber; i++) {
				Vector3 spawnPosition = new Vector3 ((Random.Range (-spawnValues.x, spawnValues.x)), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate (hazard[(Random.Range(0, hazard.Length))], spawnPosition, spawnRotation);
				yield return new WaitForSeconds (spawnWait);
			} //*/
			if (waveNumber > 0 && badShip.Length > 0){
				for (int i = 0; i < waveNumber/2; i++) {
					yield return new WaitForSeconds (spawnWait);
					Vector3 spawnPosition = new Vector3 ((Random.Range (-spawnValues.x, spawnValues.x)), spawnValues.y, spawnValues.z);
					Quaternion spawnRotation = Quaternion.identity;

					Instantiate (badShip[(Random.Range(0, badShip.Length))], spawnPosition, spawnRotation);
					yield return new WaitForSeconds (waveWait);
				}
			}
			waveNumber++;
			yield return new WaitForSeconds (waveWait);
			if (isDead == true && lives > 0) {
				Instantiate (Player, spawnLocation.position, spawnLocation.rotation);
				isDead = false;
			}
		}

		if (isDead == true && lives < 1) 
		{
			//Press R to restart
			restart = true;
			if ( (!PlayerPrefs.HasKey ("highscore")) || (PlayerPrefs.GetInt ("highscore") < Score) ){
				PlayerPrefs.SetInt("highscore", Score);
			}

		}
	}
}