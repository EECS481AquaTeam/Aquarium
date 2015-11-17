using UnityEngine;
using System.Collections;

public class ActionObject : MonoBehaviour {

	public const float INCREASE_FACTOR = 1.01f;// The rate at which the object grows
	public const float DECREASE_FACTOR = 0.99f;// The rate at which the object shrinks
	public const float NORMAL_SIZE = 1.0f;		// The normal scale of an object
	public const int OBJECT_RADIUS = 50;		// How far around an object is considered touching the object

	public Vector3 targetLocation;

	private float speed;
	
	public virtual void Awake()
	{
		targetLocation = pos = Utility.GetRandomVector (15);
		speed = Random.Range (5,8);
	}

	// to initialize the location of an object, call Instiate(x); followed by x.Initialize(param1, param2,...);
	public virtual void Initialize (Vector3 pos_, float speed_)
	{
		targetLocation = pos = pos_;
		speed = speed_;
	}
	
	// Destroy the object if it is outside the frame of the camera
	public virtual void Update()
	{
		if (!Utility.V3Equal(pos, targetLocation))
		{
			MoveTowardsTarget (targetLocation);
		}

		if (OutsideCamera ()) {
			Destroy (gameObject);
			Debug.Log ("destroyed");
		}
	}
	
	// The object grows if the mouse is clicking the object, and shrinks back to its normal size otherwize 
	public void Grow (float increase=INCREASE_FACTOR)
	{
		scale = scale * increase;
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
		tempPos.x += speed * Time.deltaTime;
		tempPos.y += speed * Time.deltaTime;
		tempPos.z += speed * Time.deltaTime;
		pos = tempPos;
	}
	
	public void MoveTowardsTarget( Vector3 targetPosition) {
		targetLocation = targetPosition;
		pos = Vector3.MoveTowards (pos, targetPosition, speed*Time.deltaTime);
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
	
	// Getter & setter for scale of the object
	public Vector3 scale
	{
		get {
			return (transform.localScale);
		} set {
			transform.localScale = value;
		}
	}
	
	// Getter & setter for position of the object
	public Vector3 pos {
		get {
			return (transform.position);
		}
		set {
			transform.position = value;
		}
	}
	
	private bool OutsideCamera()
	{
		if ((pos.x > 15 || pos.x < -15) || (pos.y > 15 || pos.y < -15))
			return true;
		else
			return false;
	}
	
}