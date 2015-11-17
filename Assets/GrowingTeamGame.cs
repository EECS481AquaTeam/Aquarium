using UnityEngine;
using System.Collections;
using System;

public enum turn {LEFT, RIGHT, START, END};

public class GrowingTeamGame : MonoBehaviour
{
	public static float WINNER_SCALE = 1.5f;

	turn turnState = turn.START; // which fish should grow next? 
	
	public GameObject left;		// the left fish in the game
	public GameObject right;	// the right fish in the game

	public AudioClip negative;	// a clip of music with negative feedback
	public AudioClip looping;	// a clip of music with background music
	public AudioClip positive;	// a clip of music with positive feedback
	public new AudioSource audio;

	public DateTime last;		// the last time that a fish has grown or shrunk

	public Vector3 offscreenLeft = new Vector3 (-14, 1, 1);
	public Vector3 offscreenRight = new Vector3 (14, 1, 1);
	
	public Vector3 onscreenLeft = new Vector3 (-5, 1, 1);
	public Vector3 onscreenRight = new Vector3 (5, 1, 1);

	// Runs on the beginning of instantiation of this class
	void Start() {

		if (GetComponent<Main>().enabled)
			GetComponent<Main>().enabled = false;

		// set this class to incorporate the main camera's AudioSource
		audio = GetComponent<AudioSource>();

		// Initialize each of the fish in the game
		left = Instantiate (left);
		right = Instantiate (right);
		Utility.InitializeFish (left, offscreenLeft);
		Utility.InitializeFish (right, offscreenRight);

		MoveOnScreen ();

		SetRegularAudio ();	// begin the game with the default audio
		UpdateTime ();		// set the initial time of the game
	}

	void OnEnable()
	{
		if (GetComponent<Main>().enabled)
			GetComponent<Main>().enabled = false;

		MoveOnScreen ();
		turnState = turn.START;

		RescaleFish ();
	}

	// Runs once per frame
	void Update() {

		// Attempt to set the default audio
		SetRegularAudio ();

		// If it hasn't been half a second since the last action, don't do anything
		if ((DateTime.Now - last).TotalSeconds < 0.5f)
			return;

		// If a fish has already been clicked, 
		if (turnState == turn.LEFT || turnState == turn.RIGHT) {

			// set grower to be the fish whose turn it is to grow
			ActionObject grower = (turnState == turn.LEFT) ? left.GetComponent<ActionObject> () : right.GetComponent<ActionObject> ();
			// set shrinker to be the fish whose turn it isn't to grow (it will shrink if clicked)
			ActionObject shrinker = (turnState == turn.RIGHT) ? left.GetComponent<ActionObject> () : right.GetComponent<ActionObject> ();
		
			// If the proper fish is clicked on, grow it
			if (grower.ClickedOn ()) {
				Grow (grower, (turnState == turn.LEFT) ? turn.RIGHT : turn.LEFT);
			}

			// If the improper whale is clicked on, shrink both whales
			else if (shrinker.ClickedOn ()) {
				Shrink (grower, shrinker);
			}

			// If both of the fish are of the winning scale, the user wins!
			if (WinningScale (grower) && WinningScale (shrinker)) {
				turnState = turn.END;
				MoveOffScreen();
				GetComponent<Main>().enabled = true;
			}
		}

		// No fish has been clicked yet
		else if (turnState == turn.START) {

			// Get the ActionObject components of each of the fish
			ActionObject left_ob = left.GetComponent<ActionObject> ();
			ActionObject right_ob = right.GetComponent<ActionObject> ();

			// Grow a fish if it is clicked on
			if (left_ob.ClickedOn ())
			{
				Grow (left_ob, turn.RIGHT);
			}
			else if (right_ob.ClickedOn ())
			{
				Grow (right_ob, turn.LEFT);
			}
		}
		else if (turnState == turn.END)
		{

		}
	}

	void RescaleFish()
	{
		Vector3 s = left.GetComponent<ActionObject> ().scale;
		left.GetComponent<ActionObject> ().scale = new Vector3 (s.x / WINNER_SCALE, s.y / WINNER_SCALE, s.z / WINNER_SCALE);
		s = right.GetComponent<ActionObject> ().scale;
		right.GetComponent<ActionObject> ().scale = new Vector3 (s.x / WINNER_SCALE, s.y / WINNER_SCALE, s.z / WINNER_SCALE);
	}

	void MoveOnScreen()
	{
		Utility.MoveHelper (left, onscreenLeft, right, onscreenRight);
	}
	
	void MoveOffScreen()
	{
		Utility.MoveHelper (left, offscreenLeft, right, offscreenRight);
	}


	void SetRegularAudio()
	{
		// if the clip is not playing, set it to the standard audio
		if (!audio.isPlaying)
		{
			Utility.MusicChanger(audio, looping, true, 0.5f);
		}
	}
	
	void SetFeedbackAudio(AudioClip clip)
	{
		// if the clip is already playing, do nothing
		if (audio.clip == clip && audio.isPlaying)
		{
			return;
		}

		Utility.MusicChanger (audio, clip, false, 0.75f);
	}

	void Grow(ActionObject item, turn new_state)
	{
		SetFeedbackAudio (positive);
		
		item.Grow (1.05f);

		turnState = new_state;
		
		UpdateTime ();
	}

	void Shrink(ActionObject item1, ActionObject item2)
	{
		SetFeedbackAudio (negative);
		
		ShrinkHelper (item1);
		ShrinkHelper (item2);
		
		UpdateTime ();
	}

	void ShrinkHelper(ActionObject item)
	{
		if (item.scale [0] > 0.5)
		{
			item.Grow (0.95f);
		}
	}

	bool WinningScale(ActionObject item)
	{
		return item.scale [0] >= WINNER_SCALE;
	}

	void UpdateTime()
	{
		last = DateTime.Now;
	}
}