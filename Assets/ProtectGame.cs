using UnityEngine;
using System.Collections;

public class AttackerWithTarget {
	public GameObject attacker;
	public Vector3 target;
	public AttackerWithTarget(GameObject a, Vector3 targetPosition) {
		attacker = a;
		target = targetPosition;
	}
}

public class ProtectGame : MonoBehaviour {
//	public new AudioSource audio;
//	public AudioClip positive;
//	public static bool audioIsPlaying = false;

	private Vector3 targetPos = new Vector3(0,1,1);
	public GameObject defender;
	private int health = 10;

	public GameObject[] attacks;
	AttackerWithTarget[] attackers = new AttackerWithTarget[10];
	
	void Start() {
//		audio = GetComponent<AudioSource> ();
		defender = Instantiate (defender);
		Utility.InitializeFish (defender, targetPos, 0);

		for (int i = 0; i < attackers.Length; ++i) {
			attackers [i] = new AttackerWithTarget (Instantiate (attacks [i]), targetPos);

			GameObject attacker = attackers [i].attacker;
			Utility.InitializeFish (attacker, new Vector3 (Random.Range (-100, 100), Random.Range (-100, 100), 1), Random.Range (5, 10));
			attacker.GetComponent<ActionObject> ().MakeUndestroyable ();
		}
	}

	void Update () {
		bool attackersRemaining = false;
		for (int i = 0; i < attackers.Length; ++i) {
			if (!attackers [i].attacker)
				continue;
			else
				attackersRemaining = true;

			GameObject attacker = attackers [i].attacker;

			ActionObject script = attacker.GetComponent<ActionObject> ();

			if (Utility.V3Equal (script.pos, attackers [i].target)) {
				if (Utility.V3Equal (script.pos, targetPos)) {
					--health;
					print (string.Format ("Hit! {0} health remaining.", health));
					Destroy (attacker);
					attackers [i].attacker = null;
				} else {
					attackers [i].target = targetPos;
				}

			} else {
				if (script.ClickedOn () && Utility.V3Equal (attackers [i].target, targetPos)) {
					attackers [i].target = GetNewTarget (script);
				} 
				script.MoveTowardsTarget (attackers [i].target);
			}
		}

		if (!attackersRemaining) {
			enabled = false;
		}

		if (health == 0) {
			print ("Game over, attackers win!");
			Destroy (defender);
			foreach (AttackerWithTarget g in attackers) {
				if (g.attacker)
					Destroy (g.attacker);
			}
			enabled = false;
		}
	}

	Vector3 GetNewTarget(ActionObject a)
	{
		Vector3 currentLocation = a.pos;

		if (currentLocation.x == targetPos.x) { // In a vertical line
			if (currentLocation.y > targetPos.y) // Attacker is above target
				return new Vector3 (targetPos.x, 20, 1);
			else // Attacker is below target
				return new Vector3 (targetPos.x, -20, 1);
		}

		bool left = currentLocation.x < targetPos.x ? true : false;

		if (currentLocation.y == targetPos.y) { // In a horizontal line
			if (left) // Attacker is to the left of the target
				return new Vector3 (-20, targetPos.y, 1);
			else // Attacker is to the right of the target
				return new Vector3 (20, targetPos.y, 1);
		}

		float slope = (targetPos.y - currentLocation.y) / (targetPos.x - currentLocation.x);

		if (left)
			return new Vector3 (-20, 1 - (20 * slope), 1);
		else // right
			return new Vector3 (20, 1 + (20 * slope), 1);
	}
}