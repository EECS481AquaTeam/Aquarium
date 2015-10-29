using UnityEngine;
using System.Collections;

public class ObjectGrower : MonoBehaviour
{
	
	public Vector3 currentScale;
	public const float MAX_SIZE = 2.0f;
	public const float INCREASE_FACTOR = 1.01f;
	public const float DECREASE_FACTOR = 0.99f;
	public const float NORMAL_SIZE = 1.0f;
	public int OBJECT_RADIUS = 50;
	Vector3 screen_position;


	void Start()
	{	
		currentScale = this.transform.localScale;
		screen_position = Camera.main.WorldToScreenPoint (this.transform.position);
	}

	void Update()
	{
		double distance_to_mouse = Vector3.Distance(screen_position, Input.mousePosition);

		// If the mouse is far away from the object or is not being clicked
		if (distance_to_mouse > OBJECT_RADIUS || !Input.GetMouseButton(0))
		{
			// If the object is bigger than normal, shrink it down
			if (currentScale [0] > NORMAL_SIZE) {
				currentScale = currentScale * DECREASE_FACTOR;
			}
			// Or set it to the standar size
			else {
				currentScale = new Vector3 (NORMAL_SIZE, NORMAL_SIZE, NORMAL_SIZE);
			}
		}
		// If the mouse is close to the object and clicking
		else
		{
			// If the object isn't too big, increase it's size.
			if (currentScale[0] <= MAX_SIZE)
			{
				currentScale = currentScale * INCREASE_FACTOR;
			}
		}

		// Set the new object to its new scale
		this.transform.localScale = currentScale;

	}
}
