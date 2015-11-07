using UnityEngine;
using System.Collections;

public enum fishState {NORMAL, MOVINGTO};


public class Fish : MonoBehaviour {
	public static int count = 0;
	public Vector3 dest = new Vector3(-5,0,0);
	// Speed of a Fish at a givien time (x,y,z) components
	private Vector3 speed = new Vector3(Random.Range (-3, 3),
	                                    Random.Range (-3, 3),
	                                    Random.Range (-3, 3));

	fishState fState = fishState.NORMAL;

	// Create a random initial location of the fist
	void Start () {

		Vector3 tempPos = pos;
		tempPos.x = Random.Range (-20, 20);
		tempPos.y = Random.Range (-20, 20);
		tempPos.z = Random.Range (-20, 20);
		pos = tempPos;

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

	void Update() {
		switch (fState) {
		case fishState.NORMAL:
			move();
			break;
		case fishState.MOVINGTO:
			moveTo();
			break;
		}
	}

	void OnMouseDown() {
		fState = fishState.MOVINGTO;
		dest.x = dest.x + (2 * count);
		count ++;
//		speed[0] = Random.Range (-10, 10);
//		speed[1] = Random.Range (-10, 10);
//		speed[2] = Random.Range (-10, 10); 
	}

	public virtual void move() {
		Vector3 tempPos = pos;
		tempPos.x += speed[0] * Time.deltaTime;
		tempPos.y += speed[1] * Time.deltaTime;
		tempPos.z += speed[2] * Time.deltaTime;
		pos = tempPos;
	}

	public virtual void moveTo() {
		pos = Vector3.MoveTowards (pos, dest, .2f);
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
