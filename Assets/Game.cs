
using UnityEngine;
using System.Collections;

public enum objectState {NORMAL, MOVINGTO, DONE};

public class whaleWithState {
	public GameObject whale;
	public objectState state;
	public whaleWithState(GameObject w, objectState s) {
		whale = w;
		state = s;
	}
}

public class Game : MonoBehaviour {
	public static int count = 0;
	public static int numObjects = 4; //hard coded this now, but make it dynamic later?
	public static int lineCount = 0;
	public static bool shouldDive = false;
	public Vector3 targetPos = new Vector3(-8,0,0);
	
	public GameObject[] ws;

	whaleWithState[] whaleList = new whaleWithState[4];
	
	void Start() {
		for (int i = 0; i < 4; ++i) {
			whaleList [i] = new whaleWithState (Instantiate(ws[i]), objectState.NORMAL);
		}
	}
	
	void Update() {
		if (shouldDive) {
			foreach(whaleWithState w in whaleList) {
				print ("DIVING");
				w.whale.GetComponent<Whale>().Dive();
				w.state = objectState.DONE;
			}
			lineCount = 0;
		}
		foreach (whaleWithState w in whaleList) {
			//print(w.whale.transform.position);
			Whale script = w.whale.GetComponent<Whale>();
			switch (w.state) {
			case objectState.NORMAL:
				if (script.ClickedOn ()) {
					print ("clicked on is true");
					w.state = objectState.MOVINGTO;
					script.MoveTowardsTarget(targetPos);
					targetPos.x = targetPos.x + (5 * count);
					count ++;
					break;
				}
				break;
			case objectState.MOVINGTO:
				if (script.pos == targetPos) {
					lineCount++;
					if (lineCount == numObjects) {
						//all objects must dive
						shouldDive = true;
					} else {
						w.state = objectState.DONE;
					}
				}
				break;
			case objectState.DONE:
				break;
			}
		}
	}
}