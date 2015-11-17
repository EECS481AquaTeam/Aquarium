﻿using UnityEngine;
using System.Collections;

static public class Utility {
	private const double EQUAL_VECTORS = 0.01;	// 3d vectors are considered to be equal if the magnitude of their differences < EQUAL_VECTORS


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

	// Defines vectors to be equal if the magnitude of their difference is sufficiently small 
	static public bool V3Equal(Vector3 a, Vector3 b)
	{
		return Vector3.SqrMagnitude(a - b) < EQUAL_VECTORS;
	}

	static public Vector3 GetRandomVector(int range=10)
	{
		return new Vector3(Random.Range (-8, 8),
		                   Random.Range (-5, 5),
		                   Random.Range (0, 5));
	}

}
