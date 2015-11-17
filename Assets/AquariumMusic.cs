using UnityEngine;
using System.Collections;

public class AquariumMusic : MonoBehaviour
{

	public AudioClip background;	// a clip of Main background music
	public AudioClip transition; 	// a clip for transitioning between screens
	public AudioClip positive;		// a clip of music with positive feedback
	public AudioClip negative;		// a clip of music with negative feedback

	public new AudioSource audio;

	// Use this for initialization
	void Start ()
	{
		// set this class to incorporate the main camera's AudioSource
		audio = GetComponent<AudioSource>();
	}

	void Update ()
	{
		if (!audio.isPlaying)
		{
			PlayBackground ();
		}
	}

	public void PlayBackground()
	{
		MusicChanger (background, true, 0.5f);
	}

	public void PlayTransition()
	{
		MusicChanger (transition, false, 0.5f);
	}

	public void PlayFeedback(AudioClip clip)
	{
		// if the clip is already playing, do nothing
		if (audio.clip == clip && audio.isPlaying)
		{
			return;
		}
		
		MusicChanger (clip, false, 0.75f);
	}
	
	// Changes the setting of the AudioSource to the given settings
	void MusicChanger(AudioClip clip, bool loop, float volume)
	{
		audio.clip = clip;
		audio.loop = loop;
		audio.volume = volume;
		audio.Play ();
	}

	// Getter & setter for position of the object
	public AudioClip pos {
		get {
			return positive;
		}
		private set {
		}
	}

	// Getter & setter for position of the object
	public AudioClip neg {
		get {
			return negative;
		}
		private set {
		}
	}
}

