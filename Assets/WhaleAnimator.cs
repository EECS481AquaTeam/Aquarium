
using UnityEngine;
using System.Collections;

public class WhaleAnimator : MonoBehaviour {

	private int current_animation;
	private string[] animations = new string[5];

	void Start()
	{
		animations [0] = "swim";
		animations [1] = "fastswim";
		animations [2] = "death";
		animations [3] = "fastswim2";
		animations [4] = "dive";

		current_animation = 0;
		GetComponent<Animation> ().Play(animations[current_animation]);
	}
	
	void Update() {
		// if the whale died already, can't do another action
		if (current_animation == 2)
			return;

		if (Input.GetMouseButtonDown (0))
			current_animation = Random.Range (0, 5);
			GetComponent<Animation> ().Play(animations[current_animation]);
	}

}
