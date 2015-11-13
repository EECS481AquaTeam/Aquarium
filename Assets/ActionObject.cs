using UnityEngine;
using System.Collections;

public class ActionObject : MonoBehaviour {
	
	public static float INCREASE_FACTOR = 1.01f;// The rate at which the object grows
	public static float DECREASE_FACTOR = 0.99f;// The rate at which the object shrinks
	public static float NORMAL_SIZE = 1.0f;		// The normal scale of an object
	public static int OBJECT_RADIUS = 50;		// How far around an object is considered touching the object
	public static double EQUAL_VECTORS = 0.01;	// 3d vectors are considered to be equal if the magnitude of their differences < EQUAL_VECTORS
	
	// Speed of a Fish at a given time (x,y,z) components
	private Vector3 speed;
	
	public virtual void Awake()
	{
		pos = GetRandomVector (15);
		speed = GetRandomVector (8);
		
		Debug.Log ("created");
	}
	
	// Destroy the object if it is outside the frame of the camera
	public virtual void Update()
	{
		if (OutsideCamera ()) {
			Destroy (gameObject);
			Debug.Log ("destroyed");
		}
	}
	
	// The object grows if the mouse is clicking the object, and shrinks back to its normal size otherwize 
	public void Grow ()
	{
		scale = scale * INCREASE_FACTOR;
	}
	
	public void Shrink ()
	{
		scale = scale * DECREASE_FACTOR;
	}
	
	public void ResetScale()
	{
		scale = new Vector3 (NORMAL_SIZE, NORMAL_SIZE, NORMAL_SIZE);
	}
	
	// Update the location by 1 unit of time
	public void Move() {
		Vector3 tempPos = pos;
		tempPos.x += speed[0] * Time.deltaTime;
		tempPos.y += speed[1] * Time.deltaTime;
		tempPos.z += speed[2] * Time.deltaTime;
		pos = tempPos;
	}
	
	public void MoveTowardsTarget( Vector3 targetPosition) {
		pos = Vector3.MoveTowards (pos, targetPosition, .2f);
		/*
		Vector3 directionOfTravel = targetPosition - pos;
		
		//now normalize the direction, since we only want the direction information
		directionOfTravel.Normalize();
		
		//scale the movement on each axis by the directionOfTravel vector components
		this.transform.Translate(
			(directionOfTravel.x * speed[0] * Time.deltaTime),
			(directionOfTravel.y * speed[1] * Time.deltaTime),
			(directionOfTravel.z * speed[2] * Time.deltaTime),
			Space.World);
			*/
	}
	
	public Vector3 PositionOnScreen()
	{
		return Camera.main.WorldToScreenPoint (pos);
	}
	
	public bool ClickedOn()
	{
		// mouse is not being clicked
		if (!Input.GetMouseButton (0))
			return false;
		
		// Find distance that the mouse is from the object on the screen
		double distance_to_mouse = Vector3.Distance(PositionOnScreen(), Input.mousePosition);
		
		// Mouse is clicking far from the object
		if (distance_to_mouse > OBJECT_RADIUS)
			return false;
		// Mouse is clicking close to the object
		else
			return true;
	}
	
	// Defines vectors to be equal if the magnitude of their difference is sufficiently small 
	static public bool V3Equal(Vector3 a, Vector3 b)
	{
		return Vector3.SqrMagnitude(a - b) < EQUAL_VECTORS;
	}
	
	// Getter & setter for scale of the object
	public Vector3 scale
	{
		get {
			return (this.transform.localScale);
		} set {
			this.transform.localScale = value;
		}
	}
	
	// Getter & setter for scale of the object
	public Vector3 pos {
		get {
			return (this.transform.position);
		}
		set {
			this.transform.position = value;
		}
	}
	
	private Vector3 GetRandomVector(int range=10)
	{
		return new Vector3(Random.Range (-8, 8),
		                   Random.Range (-5, 5),
		                   Random.Range (0, 5));
	}
	
	private bool OutsideCamera()
	{
		if ((pos.x > 15 || pos.x < -15) || (pos.y > 15 || pos.y < -15))
			return true;
		else
			return false;
	}
	
}