using UnityEngine;
using System.Collections;

public class ProtectGame : MonoBehaviour {
//	public new AudioSource audio;
//	public AudioClip positive;
//	public static bool audioIsPlaying = false;

	private Vector3 targetPos = new Vector3(0,1,1);
	public GameObject defender;
	private int health = 10;

	public GameObject[] attackers;
	public Vector3[] destinations;
	
	void Start() {
//		audio = GetComponent<AudioSource> ();
		defender = Instantiate (defender);
		Utility.InitializeFish (defender, targetPos, 0);

		if (attackers.Length != destinations.Length) {
			Debug.LogError("Error: The number of attackers does not equal the number attacker destinations");
			return;
		}


		for (int i = 0; i < attackers.Length; ++i) {
			attackers[i] = Instantiate(attackers[i]);
			Utility.InitializeFish(attackers[i], new Vector3(Random.Range (-100,100), Random.Range (-100,100), 1), Random.Range (1,2));
			attackers[i].GetComponent<ActionObject>().MakeUndestroyable();
		}

		for (int i = 0; i < destinations.Length; ++i) {
			destinations[i] = targetPos;
		}
	}

	void Update () {
		bool attackersRemaining = false;
		for (int i = 0; i < attackers.Length; ++i)
		{
			if (!attackers[i]) continue;
			else attackersRemaining = true;

			ActionObject script = attackers[i].GetComponent<ActionObject>();

			if (Utility.V3Equal(script.pos, destinations[i]))
			{
				if (Utility.V3Equal(script.pos, targetPos))
				{
					--health;
					Debug.Log("hit!");
				}
				Destroy(attackers[i]);
				attackers[i] = null;
			}
			else
			{
				if (script.ClickedOn() && Utility.V3Equal(destinations[i], targetPos))
				{
					destinations[i] = GetNewTarget(script);
				} 
				script.MoveTowardsTarget(destinations[i]);
			}
		}

		if (!attackersRemaining)
		{
			enabled = false;
		}

		if (health == 0)
		{
			Destroy(defender);
			foreach (GameObject g in attackers)
			{
				if (g) Destroy(g);
			}
			enabled = false;
		}
	}

	Vector3 GetNewTarget(ActionObject a)
	{
		Vector3 currentLocation = a.pos;

		bool vertical = (currentLocation.x == targetPos.x);
		bool above = currentLocation.y > targetPos.y ? true : false;

		if (vertical && above)
			return new Vector3 (targetPos.x, 20, 1);
		else if (vertical && !above)
			return new Vector3 (targetPos.x, -20, 1);

		bool left = currentLocation.x < targetPos.x ? true : false;

		float slope = (targetPos.y - currentLocation.y) / (targetPos.x - currentLocation.x);

		if (left) {
			return new Vector3 (-20, 1 - (20 * slope), 1);
		} else { // right
			return new Vector3 (20, 1 + (20 * slope), 1);
		}
	}
}