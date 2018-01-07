using UnityEngine;
using System.Collections;


public class Menu : MonoBehaviour {
	public string playerName = "Roger";
	public bool musicOn = true;
	public bool sfxOn = true;
	public int controllerStyle;
	public int screenLimits;
	public GUIStyle StartButtonStyle;
	public GUIStyle menuStyle;
	private bool showOptionsMenu = false;
	
	public Texture startButton;
	public Texture OptionsButton;
	public Texture SFXButton;
	public Texture noSFXButton;
	public Texture MusicButton;
	public Texture NoMusicButton;
	public Texture ResetHSButton;

	void Start(){
		if (PlayerPrefs.HasKey ("name"))
			playerName = PlayerPrefs.GetString ("name");
		
		if (PlayerPrefs.HasKey ("sfx"))
			sfxOn = (PlayerPrefs.GetInt ("sfx") == 1);

		if (PlayerPrefs.HasKey ("music"))
			musicOn = (PlayerPrefs.GetInt ("music") == 1);

		if (PlayerPrefs.HasKey ("controls"))
			controllerStyle = PlayerPrefs.GetInt ("controls");
		
		if (PlayerPrefs.HasKey ("screenLimits"))
			screenLimits = (int) (PlayerPrefs.GetFloat ("screenLimits") * 10);

	}

	void LoadaLevel(string level){
		UpdatePrefs ();
		Application.LoadLevel (level);
	}

	void UpdatePrefs()
	{
		PlayerPrefs.SetString ("name", playerName);
		PlayerPrefs.SetInt("music", ((musicOn)?1:0));
		PlayerPrefs.SetInt("sfx", ((sfxOn)?1:0));
		PlayerPrefs.SetInt ("controls", controllerStyle);
		PlayerPrefs.SetFloat ("screenLimits", (float) screenLimits / 10);
	}

	void OnGUI() {
		//Start the game
#if UNITY_ANDROID 
		// new Rect(left, top, width, height);
		var LoadRect =      new Rect ((Screen.width / 2) - 60, 50, 120, 50);
		
		var ResetRect =     new Rect ((Screen.width / 2) - 60, 50, 164, 50);
		var HighscoreRect = new Rect ((Screen.width / 2) - 60, 200, 120, 50);

		
		var sfxRect =   new Rect((Screen.width / 2) - 120, (Screen.height/2)+100, 120, 50);
		var musicRect = new Rect((Screen.width / 2),       (Screen.height/2)+100, 120, 50);
		//var controllerRect = new Rect((Screen.width / 2) - 90, Screen.height- 80, 90, 60); //not used on PC yet
		
		var controllerRect = new Rect((Screen.width / 2) - (Screen.width / 6), Screen.height- 80, (Screen.width / 3), 60);

		var limitsRect = new Rect( Screen.width / 5, (Screen.height/2+60), (Screen.width * 3/5), 	80);
		var limitDisplayRect = new Rect((Screen.width / 2) - (Screen.width / 6), Screen.height/2, (Screen.width / 3), 60);
		
		controllerStyle = (int) GUI.HorizontalSlider (controllerRect, controllerStyle, 0, 1);
#else
		// new Rect(left, top, width, height);
		var LoadRect =      new Rect ((Screen.width / 2) - 60, 50, 120, 50);

		var ResetRect =     new Rect ((Screen.width / 2) - 60, 50, 164, 50);
		var HighscoreRect = new Rect ((Screen.width / 2) - 60, 200, 120, 50);


		var sfxRect =   new Rect((Screen.width / 2) - 120, (Screen.height/2)+100, 120, 50);
		var musicRect = new Rect((Screen.width / 2),       (Screen.height/2)+100, 120, 50);
		//var controllerRect = new Rect((Screen.width / 2) - 90, Screen.height- 80, 90, 60); //not used on PC yet
#endif

		var optionsRect = new Rect((Screen.width / 2) - 60,Screen.height - 180,120,50);


		
		//show options menu
		showOptionsMenu = GUI.Toggle(optionsRect, showOptionsMenu, OptionsButton, menuStyle);


		if (showOptionsMenu) {
			//Sound Effects
			sfxOn = GUI.Toggle (sfxRect, sfxOn, ((sfxOn) ? SFXButton : noSFXButton), menuStyle);

			//Music
			musicOn = GUI.Toggle (musicRect, musicOn, ((musicOn) ? MusicButton : NoMusicButton), menuStyle);
			//Load/Start Button
			if (GUI.Button (ResetRect, ResetHSButton, menuStyle))
				PlayerPrefs.SetInt ("highscore", 0);
			
#if UNITY_ANDROID 
			GUI.Label (limitDisplayRect, string.Format ("AR: {0:000}", ((float)Screen.height/(float)Screen.width*100)) + " : " + ((PlayerPrefs.HasKey ("screenLimits"))?  string.Format("{0}", PlayerPrefs.GetFloat ("screenLimits")):"6"), menuStyle); 
			screenLimits = (int) GUI.HorizontalSlider (limitsRect, screenLimits, 90, 20);
#endif
		} else {
			//Load/Start Button
			if (GUI.Button (LoadRect, startButton, menuStyle))
				LoadaLevel ("Main");

			//Show high scores! (not working yet)
			if (PlayerPrefs.HasKey ("highscore") && (PlayerPrefs.GetInt ("highscore") > 0 ))
				GUI.Label (HighscoreRect, "High Score: " + PlayerPrefs.GetInt ("highscore"), menuStyle); 

		}

		/*
		GUI.Label (new Rect (Screen.width / 2 - 100, 230, 200, 20), "Name:");
		playerName = GUI.TextField(new Rect(Screen.width / 2 - 100, 250, 200, 30), playerName, 60); //*/

		if (GUI.changed)
			UpdatePrefs ();
	}

}
