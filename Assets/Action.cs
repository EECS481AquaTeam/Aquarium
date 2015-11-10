using UnityEngine;
using System.Collections;

//public enum objectState {NORMAL, MOVINGTO, DONE, DIVE};


public class Action : MonoBehaviour {
	public static int count = 0;
	public static int numObjects = 2; //hard coded this now, but make it dynamic later
	public static int lineCount = 0;

	public static bool shouldDive = false;

	public Vector3 targetPos = new Vector3(-8,0,0);
	// Speed of a Object at a givien time (x,y,z) components
	private Vector3 speed = new Vector3(Random.Range (-3, 3),
	                                    Random.Range (-3, 3),
	                                    Random.Range (-3, 3));

	public objectState objState = objectState.NORMAL;
	private Vector3 screen_position;
	private int OBJECT_RADIUS = 50;

	// Create a random initial location of the fist
	void Start () { //add in rotation value
		screen_position = Camera.main.WorldToScreenPoint (pos);
		Vector3 tempPos = pos;
		tempPos.x = Random.Range (-10, 10);
		tempPos.y = Random.Range (-10, 10);
		tempPos.z = Random.Range (-10, 10);
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
		if (OutsideCamera ()) {
			Destroy (gameObject);
			Debug.Log ("destroyed");
		}
		if (shouldDive) {
			print ("DIVING");
			GetComponent<Animation> ().Play("dive");
			lineCount--;
			if (lineCount == 0) {
				shouldDive = false;
				objState = objectState.DONE;
			}
		}
		switch (objState) {
		case objectState.NORMAL:
			move();
			isClicked();
			break;
		case objectState.MOVINGTO:
			moveTo();
			if (pos == targetPos) {
				lineCount++;
				if (lineCount == numObjects) {
					//all objects must dive
					shouldDive = true;
				}
				else {
					objState = objectState.DONE;
				}
			}
			break;
		case objectState.DONE:
			break;
		}
	}

	bool OutsideCamera()
	{
		if ((pos.x > 15 || pos.x < -15) || (pos.y > 15 || pos.y < -15))
			return true;
		else
			return false;
	}

	void isClicked() {
		// Find the location of the object on the screen
		screen_position = Camera.main.WorldToScreenPoint (pos);
		
		// Find distance that the mouse is from the object on the screen
		double distance_to_mouse = Vector3.Distance(screen_position, Input.mousePosition);
		
		// If the mouse is far away from the object or is not being clicked
		if (distance_to_mouse > OBJECT_RADIUS || !Input.GetMouseButton(0))
		{
			//do nothing
		}
		// If the mouse is close to the object and clicking
		else
		{
			objState = objectState.MOVINGTO;
			targetPos.x = targetPos.x + (5 * count);
			count ++;
		}
	}
	void OnMouseDown() {
		objState = objectState.MOVINGTO;
		targetPos.x = targetPos.x + (2 * count);
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
		pos = Vector3.MoveTowards (pos, targetPos, .2f);
	}
		
	// Current position of the Object
	public Vector3 pos {
		get {
			return (this.transform.position);
		}
		set {
			this.transform.position = value;
		}
	}

}
