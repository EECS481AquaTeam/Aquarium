using UnityEngine;
using System.Collections;

public enum Color {red, green, blue, yellow};

public class Fish : ActionObject {
	
	// Create a random initial location of the fist
	public override void Awake () {
		base.Awake ();

		Color color = (Color)Random.Range(0, 4);

		if (color == Color.red) {
			gameObject.GetComponent<Renderer> ().material.color = Color.red;
		} else if (color == Color.green) {
			gameObject.GetComponent<Renderer> ().material.color = Color.green;
		} else if (color == Color.blue) {
			gameObject.GetComponent<Renderer>().material.color = Color.blue;
		} else if (color == Color.yellow) {
			gameObject.GetComponent<Renderer>().material.color = Color.yellow;
		}

	}
}
