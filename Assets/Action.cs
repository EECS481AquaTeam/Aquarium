using UnityEngine;
using System.Collections;

public class Action : MonoBehaviour {

	private Vector3 currentScale;				// Current scale of the object
	private const float MAX_SCALE = 2.0f;		// Maximum scale that the object can be
	private const float INCREASE_FACTOR = 1.01f;// The rate at which the object grows
	private const float DECREASE_FACTOR = 0.99f;// The rate at which the object shrinks
	private const float NORMAL_SIZE = 1.0f;		// The normal scale of an object
	private int OBJECT_RADIUS = 50;				// How far around an object is considered touching the object
	private Vector3 screen_position;			// The absolute location of the object on the screen
	public bool grow_or_shrink = true;			// If the object should grow or shrink when clicked on
	
	private Vector3 currentPosition;			// Current position of the object
	private Vector3 targetPosition;		
	private Vector3 targetPosition1;			
	private Vector3 targetPosition2;		// TODO: IM NOT SURE THAT I UNDERSTAND THE DIFFERENCE BETWEEEN THESE
	private Vector3 targetPosition3;		// IF SOMEONE DOES UNDERSTAND, PLEASE COMMENT
	private Vector3 targetPosition4;
	private double EQUAL_VECTORS = 0.01;	// 3d vectors are considered to be equal if the magnitude of their differences < EQUAL_VECTORS
	public bool move = true;					// If the object should move when another location is clicked on

	// Speed of a Fish at a givien time (x,y,z) components
	private Vector3 speed; 

	public bool move_on_click = true;			// If the object should change directions when clicked on

	void Start()
	{	
		// Object starts out with a normal scale
		currentScale = scale;

		// Find the location of the object on the screen
		screen_position = Camera.main.WorldToScreenPoint (pos);

		// Initialize target positions for moving through the space
		targetPosition1 = new Vector3 (-4.8f, -7.7f, -1f);
		targetPosition2 = new Vector3 (-4.8f, 7.7f, -1f);
		targetPosition3 = new Vector3 (4.8f,7.7f,-1f);
		targetPosition4 = new Vector3 (4.8f,-7.7f,-1f);

		currentPosition = pos;

		if (V3Equal(currentPosition,targetPosition1)) 
			targetPosition = targetPosition2;
		else if (V3Equal(currentPosition,targetPosition2))
			targetPosition = targetPosition3;
		else if (V3Equal(currentPosition,targetPosition3))
			targetPosition = targetPosition4;
		else if (V3Equal(currentPosition,targetPosition4))
			targetPosition = targetPosition1;

		if (move_on_click)
		{
			speed = new Vector3(Random.Range (-8, 8),
			              Random.Range (-8, 8),
			              Random.Range (-8, 8));

			Vector3 tempPos = pos;
			tempPos.x = Random.Range (-50, 50);
			tempPos.y = Random.Range (-50, 50);
			tempPos.z = Random.Range (-50, 50);
			pos = tempPos;
		}
	}

	void Update()
	{
		if (OutsideCamera ()) {
			Destroy (gameObject);
			Debug.Log ("destroyed");
		}
		if (grow_or_shrink)
			GrowOrShrink ();
		if (move)
			MoveToTarget ();
		if (move_on_click)
			MoveBySelf ();
	}

	bool OutsideCamera()
	{
		if ((pos.x > 15 || pos.x < -15) || (pos.y > 15 || pos.y < -15))
			return true;
		else
			return false;
	}

	// When the user clicks a fish, give it a new, random speed
	void OnMouseDown() {
		if (move_on_click)
		{
			speed [0] = Random.Range (-10, 10);
			speed [1] = Random.Range (-10, 10);
			speed [2] = Random.Range (-10, 10); 
		}
	}

	// The object grows if the mouse is clicking the object, and shrinks back to its normal size otherwize 
	void GrowOrShrink()
	{
		// Find the location of the object on the screen
		screen_position = Camera.main.WorldToScreenPoint (pos);

		// Find distance that the mouse is from the object on the screen
		double distance_to_mouse = Vector3.Distance(screen_position, Input.mousePosition);
		
		// If the mouse is far away from the object or is not being clicked
		if (distance_to_mouse > OBJECT_RADIUS || !Input.GetMouseButton(0))
		{
			// If the object is bigger than normal, shrink it down
			if (currentScale [0] > NORMAL_SIZE)
			{
				currentScale = currentScale * DECREASE_FACTOR;
			}
			// Or set it to the standar size
			else
			{
				currentScale = new Vector3 (NORMAL_SIZE, NORMAL_SIZE, NORMAL_SIZE);
			}
		}
		// If the mouse is close to the object and clicking
		else
		{
			// If the object isn't too big, increase it's size.
			if (currentScale[0] <= MAX_SCALE)
			{
				currentScale = currentScale * INCREASE_FACTOR;
			}
		}
		
		// Set the new object to its new scale
		scale = currentScale;
	}

	void MoveToTarget() 
	{
		//move towards a target at a set speed.
		currentPosition = pos;
		if (V3Equal(currentPosition,targetPosition1)){
			targetPosition = targetPosition2;
			
		}
		else if (V3Equal(currentPosition,targetPosition2)) {
			targetPosition = targetPosition3;
			
		}
		else if (V3Equal(currentPosition,targetPosition3)){
			targetPosition = targetPosition4;
			
		}
		else if (V3Equal(currentPosition,targetPosition4)) {
			targetPosition = targetPosition1;	
		}
		MoveTowardsTarget ();
	}

	void MoveTowardsTarget() {
		//the speed, in units per second, we want to move towards the target
		float speed = 2;
		//move towards the center of the world (or where ever you like)
		currentPosition = pos;
		//if(Vector3.Distance(currentPosition, targetPosition) > .1f) { 
		Vector3 directionOfTravel = targetPosition - currentPosition;
		//now normalize the direction, since we only want the direction information
		directionOfTravel.Normalize();
		//scale the movement on each axis by the directionOfTravel vector components
		
		this.transform.Translate(
			(directionOfTravel.x * speed * Time.deltaTime),
			(directionOfTravel.y * speed * Time.deltaTime),
			(directionOfTravel.z * speed * Time.deltaTime),
			Space.World);
	}

	// Update the location by 1 unit of time
	 void MoveBySelf() {
		Vector3 tempPos = pos;
		tempPos.x += speed[0] * Time.deltaTime;
		tempPos.y += speed[1] * Time.deltaTime;
		tempPos.z += speed[2] * Time.deltaTime;
		pos = tempPos;
	}

	// Defines vectors to be equal if the magnitude of their difference is sufficiently small 
	public bool V3Equal(Vector3 a, Vector3 b)
	{
		return Vector3.SqrMagnitude(a - b) < EQUAL_VECTORS;
	}

	// Current scale of the Fish
	public Vector3 scale
	{
		get {
			return (this.transform.localScale);
		} set {
			this.transform.localScale = value;
		}
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