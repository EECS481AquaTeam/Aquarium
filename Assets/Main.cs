using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour
{
	
	public GameObject growingGameButton;
	public GameObject lineGameButton;
	public GameObject aquariumGameButton;
	
	public AudioClip mainClip;	// a clip of Main background music
	public AudioClip transitionClip; // a clip for transitioning between screens
	public new AudioSource audio;
	
	public Vector3 offscreen1 = new Vector3 (-12, 5, 1);
	public Vector3 offscreen2 = new Vector3 (12, -3, 1);
	public Vector3 offscreen3 = new Vector3 (3, 8, 1);
	
	public Vector3 onscreen1 = new Vector3 (-6, 1, 1);
	public Vector3 onscreen2 = new Vector3 (6, 1, 1);
	public Vector3 onscreen3 = new Vector3 (0, 1, 1);
	
	// Use this for initialization
	void Start ()
	{
		// set this class to incorporate the main camera's AudioSource
		audio = GetComponent<AudioSource>();
		
		// Initialize each of the fish in the game
		growingGameButton  = Instantiate (growingGameButton);
		lineGameButton     = Instantiate (lineGameButton);
		aquariumGameButton = Instantiate (aquariumGameButton);
		
		InitializeFish (growingGameButton,  offscreen1);
		InitializeFish (lineGameButton,     offscreen2);
		InitializeFish (aquariumGameButton, offscreen3);
		
		DisableGames ();
		
		MoveOnScreen ();
		
		StartMusic ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		ActionObject growingGame  = growingGameButton.GetComponent<ActionObject> ();
		ActionObject lineGame     = lineGameButton.GetComponent<ActionObject> ();
		ActionObject aquariumGame = aquariumGameButton.GetComponent<ActionObject> ();
		
		// If the proper fish is clicked on, grow it
		if (growingGame.ClickedOn ())
		{
			Debug.Log ("Growing game clicked on");
			MoveOffScreen();
			GetComponent<GrowingTeamGame>().enabled = true;
		}
		else if (lineGame.ClickedOn ())
		{
			Debug.Log ("Line game clicked on");
			MoveOffScreen();
			GetComponent<LineGame>().enabled = true;
		}
		else if (aquariumGame.ClickedOn ())
		{
			Debug.Log ("Aquarium Game clicked on");
			MoveOffScreen();
			GetComponent<AquariumGame>().enabled = true;
		}
	}
	
	void OnEnable()
	{
		Debug.Log ("Main game enabled");
		DisableGames ();
		MoveOnScreen ();
	}
	
	// Initializes the location of a fish
	void InitializeFish(GameObject item, Vector3 location)
	{
		item.GetComponent<ActionObject>().Initialize(location, 5f);
	}
	
	void MoveOnScreen()
	{
		MoveHelper (onscreen1, onscreen2, onscreen3);
	}
	
	void MoveOffScreen()
	{
		MoveHelper (offscreen1, offscreen2, offscreen3);
	}
	
	void MoveHelper(Vector3 v1, Vector3 v2, Vector3 v3)
	{
		growingGameButton .GetComponent<ActionObject> ().MoveTowardsTarget(v1);
		lineGameButton    .GetComponent<ActionObject> ().MoveTowardsTarget(v2);
		aquariumGameButton.GetComponent<ActionObject> ().MoveTowardsTarget(v3);
	}
	
	void DisableGames()
	{
		if (GetComponent<GrowingTeamGame>().enabled)
			GetComponent<GrowingTeamGame>().enabled = false;
		if (GetComponent<LineGame>().enabled)
			GetComponent<LineGame>().enabled = false;
		//		if (GetComponent<Aqu>().enabled)
		//			GetComponent<GrowingTeamGame>().enabled = false;
	}
	
	void StartMusic()
	{
		MusicHelper (mainClip, true, 0.5f);
	}
	
	void TransitionMusic()
	{
		MusicHelper (transitionClip, false, 0.5f);
	}
	
	void MusicHelper(AudioClip clip, bool loop, float volume)
	{
		audio.clip = clip;
		audio.loop = loop;
		audio.volume = volume;
		audio.Play ();
	}
}