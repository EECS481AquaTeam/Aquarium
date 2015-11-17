using UnityEngine;
using System.Collections;

static public class Utility {

	static public void MusicChanger(AudioSource audio, AudioClip clip, bool loop, float volume)
	{
		audio.clip = clip;
		audio.loop = loop;
		audio.volume = volume;
		audio.Play ();
	}

	// Initializes the location of a fish
	static public void InitializeFish(GameObject item, Vector3 location)
	{
		item.GetComponent<ActionObject>().Initialize(location, 5f);
	}
}
