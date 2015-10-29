using UnityEngine;
using System.Collections;

public class ObjectGrower : MonoBehaviour
{
	
	public Vector3 currentScale;
	public const float MAX_SIZE = 2.0f;
	public const float INCREASE_FACTOR = 1.01f;
	public const float DECREASE_FACTOR = 0.99f;
	public const float NORMAL_SIZE = 1.0f;
	Ray ray;
	RaycastHit hit;

	// Use this for initialization
	void Start()
	{	
		currentScale = this.transform.localScale;
	}

	void Update()
	{
		ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		// If a user is holding down on the object, increase its scale
		if (Input.GetMouseButton (0))
		{
			if (Physics.Raycast (ray, out hit))
			{
				if (currentScale[0] <= MAX_SIZE)
				{
					currentScale = currentScale * INCREASE_FACTOR;
				}
			}
		} 

		// Otherwise, return the scale of the object back to the standard size
		else
		{
			if (currentScale[0] > NORMAL_SIZE)
			{
				currentScale = currentScale * DECREASE_FACTOR;
			}
			else
			{
				currentScale = new Vector3(NORMAL_SIZE, NORMAL_SIZE, NORMAL_SIZE);
			}
		}
		this.transform.localScale = currentScale;

	}
}
