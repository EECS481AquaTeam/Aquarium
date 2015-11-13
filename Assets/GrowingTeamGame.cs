using UnityEngine;
using System.Collections;

public enum turn {LEFT, RIGHT};


public class GameTeamGame : MonoBehaviour
{
	public int beats = 100; // how many times the 
//	public static int numObjects = 4; //hard coded this now, but make it dynamic later?
//	public static int lineCount = 0;
//	public static bool shouldDive = false;
//	public Vector3 targetPos = new Vector3(-8,0,0);

	public static float WINNER_SCALE = 2.0f;

	public GameObject left;
	public GameObject right;
	turn turnState = turn.LEFT;
	
//	objectState objState = objectState.NORMAL;
	
//	whaleWithState[] whaleList = new whaleWithState[4];
	
	void Start() {
		Instantiate (left);
		left.GetComponent<ActionObject> ().Initialize (new Vector3(-5,1,1), new Vector3(0,0,0));
		Instantiate (right);
		right.GetComponent<ActionObject> ().Initialize (new Vector3(5,1,1), new Vector3(0,0,0));;
	}

	void Update() {
		ActionObject grower = (turnState == turn.LEFT) ? left.GetComponent<ActionObject> () : right.GetComponent<ActionObject> ();
		ActionObject shrinker = (turnState == turn.RIGHT) ? left.GetComponent<ActionObject> () : right.GetComponent<ActionObject> ();

		// If the proper whale is clicked on, grow it
		if (grower.ClickedOn ()) {
			grower.Grow (1.05f);
			turnState = (turnState == turn.LEFT) ? turn.RIGHT : turn.LEFT;
		}

		// If the improper whale is clicked on, shrink both whales
		else if (shrinker.ClickedOn ()) {
			if (shrinker.scale[0] > 0.5)
			{
				shrinker.Grow (0.95f);
			}
			if (grower.scale[0] > 0.5)
			{
				grower.Grow (0.95f);
			}
		}

		if (grower.scale[0] > WINNER_SCALE && shrinker.scale[0] > WINNER_SCALE) {
			//end the game with a reward and move back to regular mode
		}
	}
}