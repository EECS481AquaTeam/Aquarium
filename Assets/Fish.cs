using UnityEngine;
using System.Collections;

public class Fish : MonoBehaviour {

//	// Speed of a Fish at a givien time (x,y,z) components
//	private Vector3 speed = new Vector3(Random.Range (-8, 8),
//	                                    Random.Range (-8, 8),
//	                                    Random.Range (-8, 8));

	// Create a random initial location of the fist
	void Start () {

//		Vector3 tempPos = pos;
//		tempPos.x = Random.Range (-50, 50);
//		tempPos.y = Random.Range (-50, 50);
//		tempPos.z = Random.Range (-50, 50);
//		pos = tempPos;

//TODO: Should make this a static public variable, but couldn't figure out how to do this with colors
//For now keep the less efficient:
		string[] colors = {"red", "green", "blue"};
//But afterwards, change to Color[] colors = {Color.red,....};

//		string col = m.colors [Random.Range (0, colors.Length)];
		string col = colors [Random.Range (0, colors.Length)];

		if (col == "red") {
			gameObject.GetComponent<Renderer> ().material.color = Color.red;
		} else if (col == "green") {
			gameObject.GetComponent<Renderer> ().material.color = Color.green;
		} else if (col == "blue") {
			gameObject.GetComponent<Renderer>().material.color = Color.blue;
		}

	}
}
