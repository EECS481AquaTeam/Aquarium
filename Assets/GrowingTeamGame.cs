using UnityEngine;
using System.Collections;
using System;

public enum turn {LEFT, RIGHT, START};

public class GrowingTeamGame : MonoBehaviour
{
	public static float WINNER_SCALE = 2.0f;

	turn turnState = turn.START;
	
	public GameObject left;		// the left fish in the game
	public GameObject right;	// the right fish in the game

	public AudioClip negative;	// a clip of music with negative feedback
	public AudioClip looping;	// a clip of music with background music
	public AudioClip positive;	// a clip of music with positive feedback
	public new AudioSource audio;

	public DateTime last;		// the last time that a fish has grown or shrunk
	
	void Start() {
		left = Instantiate (left);
		InitializeFish (left, new Vector3 (-5, 1, 1));
		right = Instantiate (right);
		InitializeFish (right, new Vector3 (5, 1, 1));

		audio = GetComponent<AudioSource>();

		SetRegularAudio ();
		UpdateTime ();
	}

	void InitializeFish(GameObject item, Vector3 location)
	{
		item.GetComponent<ActionObject>().Initialize(location, 0f);
	}

	void Update() {

		// Attempt to set the default audio
		SetRegularAudio ();

		// If it hasn't been half a second since the last action, don't do anything
		if ((DateTime.Now - last).TotalSeconds < 0.5f)
			return;

		// If a fish has already been clicked, 
		if (turnState != turn.START) {
			ActionObject grower = (turnState == turn.LEFT) ? left.GetComponent<ActionObject> () : right.GetComponent<ActionObject> ();
			ActionObject shrinker = (turnState == turn.RIGHT) ? left.GetComponent<ActionObject> () : right.GetComponent<ActionObject> ();
		
			// If the proper whale is clicked on, grow it
			if (grower.ClickedOn ()) {
				Grow (grower);
				turnState = (turnState == turn.LEFT) ? turn.RIGHT : turn.LEFT;
			}

			// If the improper whale is clicked on, shrink both whales
			else if (shrinker.ClickedOn ()) {
				Shrink (grower, shrinker);
			}

			if (grower.scale [0] > WINNER_SCALE && shrinker.scale [0] > WINNER_SCALE) {
				//end the game with a reward and move back to regular mode
			}
		}
		else
		{
			ActionObject left_ob = left.GetComponent<ActionObject> ();
			ActionObject right_ob = right.GetComponent<ActionObject> ();
			if (left_ob.ClickedOn())
			{
				Grow(left_ob);
				turnState = turn.RIGHT;
			}
			else if (right_ob.ClickedOn())
			{
				Grow(right_ob);
				turnState = turn.LEFT;
			}
		}
	}

	void SetRegularAudio()
	{
		// if the clip is not playing, set it to the standard audio
		if (!audio.isPlaying)
		{
			audio.clip = looping;
			audio.loop = true;
			audio.volume = 0.5f;
			audio.Play ();
		}
	}
	
	void SetFeedbackAudio(AudioClip clip)
	{
		// if the clip is already playing, do nothing
		if (audio.clip == clip && audio.isPlaying)
		{
			return;
		}

		audio.clip = clip;
		audio.loop = false;
		audio.volume = 0.75f;
		audio.Play ();
	}

	void Grow(ActionObject item)
	{
		SetFeedbackAudio (positive);
		
		item.Grow (1.05f);
		
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

	void UpdateTime()
	{
		last = DateTime.Now;
	}
}