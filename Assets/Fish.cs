using UnityEngine;
using System.Collections;

public enum FishColor {red, green, blue, yellow};

public class Fish : ActionObject {
	
	// Create a random initial location of the fist
	public override void Awake () {
		base.Awake ();

		FishColor color = (FishColor)Random.Range(0, 4);

		if (color == FishColor.red) {
			gameObject.GetComponent<Renderer> ().material.color = Color.red;
		} else if (color == FishColor.green) {
			gameObject.GetComponent<Renderer> ().material.color = Color.green;
		} else if (color == FishColor.blue) {
			gameObject.GetComponent<Renderer>().material.color = Color.blue;
		} else if (color == FishColor.yellow) {
			gameObject.GetComponent<Renderer>().material.color = Color.yellow;
		}
	}
}
