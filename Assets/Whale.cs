
using UnityEngine;
using System.Collections;

public class Whale : ActionObject {
	void Swim()
	{
		GetComponent<Animation> ().Play("swim");
	}

	void SwimFast()
	{
		GetComponent<Animation> ().Play("fastswim");
	}

	void SwimFast2()
	{
		GetComponent<Animation> ().Play("fastswim2");
	}

	void Dive()
	{
		GetComponent<Animation> ().Play("dive");
	}

	void Die()
	{
		GetComponent<Animation> ().Play("death");
	}

}
