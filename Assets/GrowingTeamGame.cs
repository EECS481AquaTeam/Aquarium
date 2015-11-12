using UnityEngine;
using System.Collections;


///// for actionObject to initialize the location of an object
///// 
///// call Instiate(x);
///// x.Initialize(param1, param2,...);
//
//public virtual void Initialize (Vector3 pos_, Vector3 speed_)
//{
//	pos = pos_;
//	speed = speed_;
//}

/// change Grow() in action object to:
/// // The object grows if the mouse is clicking the object, and shrinks back to its normal size otherwize 
//public void Grow (float increase=INCREASE_FACTOR)
//{
//	scale = scale * increase;
//}
/// /
/// </summary>

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
		left.GetComponent<ActionObject> ().Initialize (Vector3(-5,1,1), Vector3(0,0,0));
		Instantiate (right);
		right.GetComponent<ActionObject> ().Initialize (Vector3(5,1,1), Vector3(0,0,0));;
	}

	void Update() {
		ActionObject grower = (turnState == turn.LEFT) ? left.GetComponent<ActionObject> () : right.GetComponent<ActionObject> ();
		ActionObject shrinker = (turnState == turn.RIGHT) ? left.GetComponent<ActionObject> () : right.GetComponent<ActionObject> ();

		// If the proper whale is clicked on, grow it
		if (grower.ClickedOn ()) {
			grower.Grow (1.05);
			turn = (turnState == turn.LEFT) ? turnState.RIGHT : turnState.LEFT;
		}

		// If the improper whale is clicked on, shrink both whales
		else if (shrinker.ClickedOn ()) {
			if (shrinker.scale > 0.5)
			{
				shrinker.Grow (0.95);
			}
			if (grower.scale > 0.5)
			{
				grower.Grow (0.95);
			}
		}

		if (grower.scale > WINNER_SCALE && shrinker.scale > WINNER_SCALE) {
			//end the game with a reward and move back to regular mode
		}
	}
}