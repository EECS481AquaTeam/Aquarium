using UnityEngine;
using System.Collections;
using System;

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

	static public void MoveHelper(GameObject g1, Vector3 v1, GameObject g2, Vector3 v2)
	{
		g1.GetComponent<ActionObject> ().MoveTowardsTarget(v1);
		g2.GetComponent<ActionObject> ().MoveTowardsTarget(v2);
	}

	static public void MoveHelper(GameObject g1, Vector3 v1, GameObject g2, Vector3 v2, GameObject g3, Vector3 v3)
	{
		MoveHelper (g1, v1, g2, v2);
		g3.GetComponent<ActionObject> ().MoveTowardsTarget(v3);
	}
}
