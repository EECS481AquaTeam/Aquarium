using UnityEngine;
using System.Collections;

public class Fish : MonoBehaviour {

	// Speed of a Fish at a givien time (x,y,z) components
	private Vector3 speed = new Vector3(Random.Range (-8, 8),
	                                    Random.Range (-8, 8),
	                                    Random.Range (-8, 8));

	// Create a random initial location of the fist
	void Start () {

		Vector3 tempPos = pos;
		tempPos.x = Random.Range (-50, 50);
		tempPos.y = Random.Range (-50, 50);
		tempPos.z = Random.Range (-50, 50);
		pos = tempPos;

//Should make this a static public variable, but couldn't figure out how to do this with colors
//For now keep the less efficient:
		string[] colors = {"red", "green", "blue"};
//But afterwards, change to Color[] colors = {Color.red,....};

		string col = colors [Random.Range (0, colors.Length)];

		if (col == "red") {
			gameObject.GetComponent<Renderer> ().material.color = Color.red;
		} else if (col == "green") {
			gameObject.GetComponent<Renderer> ().material.color = Color.green;
		} else if (col == "blue") {
			gameObject.GetComponent<Renderer>().material.color = Color.blue;
		}

		}
	
	// Update is called once per frame
	void Update () {
		Move ();
	}

	// When the user clicks a fish, give it a new, random speed
	void OnMouseDown() {
		speed[0] = Random.Range (-10, 10);
		speed[1] = Random.Range (-10, 10);
		speed[2] = Random.Range (-10, 10); 
	}

	// Update the location by 1 unit of time
	public virtual void Move() {
		Vector3 tempPos = pos;
		tempPos.x += speed[0] * Time.deltaTime;
		tempPos.y += speed[1] * Time.deltaTime;
		tempPos.z += speed[2] * Time.deltaTime;
		pos = tempPos;
	}

	// Current position of the Fish
	public Vector3 pos {
		get {
			return (this.transform.position);
		}
		set {
			this.transform.position = value;
		}
	}
}
