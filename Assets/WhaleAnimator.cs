
using UnityEngine;
using System.Collections;

public class WhaleAnimator : MonoBehaviour {

	void Start()
	{
		GetComponent<Animation> ().Play("fastswim");
	}
	
	void Update() {
		if (Input.GetMouseButtonDown(0))
			GetComponent<Animation> ().Play("death");
	}

}


