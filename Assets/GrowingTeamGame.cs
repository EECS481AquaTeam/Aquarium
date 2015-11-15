using UnityEngine;
using System.Collections;
using System;

public enum turn {LEFT, RIGHT};

public class GrowingTeamGame : MonoBehaviour
{
	public int beats = 100; // how many times the 

	public static float WINNER_SCALE = 2.0f;
	
	public GameObject left;
	public GameObject right;
	turn turnState = turn.LEFT;

	public AudioClip negative;
	public AudioClip looping;
	public AudioClip positive;
	public AudioSource audio;

	public DateTime last = DateTime.Now;
	
	void Start() {
		left = Instantiate (left);
		left.GetComponent<ActionObject> ().Initialize (new Vector3(-5,1,1), 0f);
		right = Instantiate (right);
		right.GetComponent<ActionObject> ().Initialize (new Vector3(5,1,1), 0f);

		audio = GetComponent<AudioSource>();
		SetRegularAudio ();
	}

	void Update() {
		if (!audio.isPlaying)
		{
			SetRegularAudio ();
		}

		ActionObject grower = (turnState == turn.LEFT) ? left.GetComponent<ActionObject> () : right.GetComponent<ActionObject> ();
		ActionObject shrinker = (turnState == turn.RIGHT) ? left.GetComponent<ActionObject> () : right.GetComponent<ActionObject> ();

		if ((DateTime.Now - last).TotalSeconds < 0.5f)
			return;

		// If the proper whale is clicked on, grow it
		if (grower.ClickedOn ()) {
			SetPositiveAudio();

			grower.Grow (1.05f);
			turnState = (turnState == turn.LEFT) ? turn.RIGHT : turn.LEFT;
			StartCoroutine(Wait(2.0f));

			last = DateTime.Now;
		}

		// If the improper whale is clicked on, shrink both whales
		else if (shrinker.ClickedOn ()) {

			SetNegativeAudio();

			if (shrinker.scale[0] > 0.5)
			{
				shrinker.Grow (0.95f);
			}
			if (grower.scale[0] > 0.5)
			{
				grower.Grow (0.95f);
			}

			last = DateTime.Now;
		}

		if (grower.scale[0] > WINNER_SCALE && shrinker.scale[0] > WINNER_SCALE) {
			//end the game with a reward and move back to regular mode
		}
	}

	void SetRegularAudio()
	{
		audio.clip = looping;
		audio.loop = false;
		audio.volume = 0.5f;
		audio.Play ();
	}

	void SetPositiveAudio()
	{
		audio.loop = false;
	}

	void SetNegativeAudio()
	{
		audio.clip = negative;
		audio.loop = false;
		audio.volume = 0.75f;
		audio.Play ();
	}

	IEnumerator Wait(float duration=5.0f)
	{
		yield return new WaitForSeconds(duration);   //Wait
	}
}