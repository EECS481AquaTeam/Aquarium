
using UnityEngine;
using System.Collections;

public enum objectState {NORMAL, MOVINGTO, DONE, SHOULD_DIVE, DIVING, RESTART};
public class whaleWithState {
	public GameObject whale;
	public objectState state;
	public Vector3 targetPos;
	public int timer = 1000;
	public whaleWithState(GameObject w, objectState s, Vector3 targetPosition) {
		whale = w;
		state = s;
		targetPos = targetPosition;
	}
}

public class Game : MonoBehaviour {
	public static int count = 0;
	public static int numObjects = 4; //hard coded this now, but make it dynamic later?
	public static int lineCount = 0;
	public static int timer = 1000;
	public static bool shouldDive = false;
	public Vector3 targetPos = new Vector3(-5,0,0);

	public new AudioSource audio;
	public AudioClip positive;

	public GameObject[] ws;
	public GameObject testWhale;
	
	whaleWithState[] whaleList = new whaleWithState[4];
	
	void Start() {

		for (int i = 0; i < 4; ++i) {
			Debug.Log("target Position " + targetPos);
			whaleList [i] = new whaleWithState (Instantiate(ws[i]), objectState.NORMAL, targetPos);
			targetPos.x = targetPos.x + 3;
		}
		SetFeedbackAudio ();
		/*
		Instantiate (testWhale);
		testWhale.GetComponent<Whale>().Dive ();*/
	}

	void Update() {
		/*
		if (shouldDive) {
			foreach(whaleWithState w in whaleList) {
				print ("DIVING");
				w.whale.GetComponent<Whale>().Dive();
			}
			lineCount = 0;
		}
		*/

		foreach (whaleWithState w in whaleList) {
			Whale script = w.whale.GetComponent<Whale>();
			switch (w.state) {
			case objectState.NORMAL:
				print ("NORMAL");
					if (script.ClickedOn ()) {
						print ("clicked on is true");
						w.state = objectState.MOVINGTO;
						break;
					}
				break;
			case objectState.MOVINGTO:
				print ("MOVING TO");
				if (script.pos == w.targetPos) {
					lineCount++;
					if (lineCount == numObjects) {
						//all objects must dive
						foreach (whaleWithState item in whaleList) {
							item.state = objectState.SHOULD_DIVE;
						}
					} else {
						w.state = objectState.DONE;
					}
				}
				else {
					script.MoveTowardsTarget(w.targetPos);
				}
				break;
			case objectState.SHOULD_DIVE:
				print ("DIVING");
				w.targetPos.x--;
				w.targetPos.y = -2;
				//w.whale.GetComponent<Whale>().Die();
				w.state = objectState.DIVING;
				//StartCoroutine(SomeCoroutine(w));
				//w.state = objectState.DONE;
				lineCount--;
				break;
			case objectState.DIVING:
				print ("IS DIVING");
				if (script.pos == w.targetPos) {
					lineCount++;
					if (lineCount == numObjects) {
						//all objects must dive
						foreach (whaleWithState item in whaleList) {
							item.state = objectState.RESTART;
						}
					} else {
						w.state = objectState.DONE;
					}
				}
				else {
					script.MoveTowardsTarget(w.targetPos);
				}
				break;
			case objectState.DONE:
				break;
			case objectState.RESTART:
				Vector3 whalePos = script.GetRandomVector(15);
				script.pos = whalePos;
				w.state = objectState.NORMAL;
				w.targetPos.y = 0;
				lineCount--;
				break;
			}
		}

	}

	void SetFeedbackAudio() {
		audio.clip = positive;
		audio.Play ();
	}

	private IEnumerator SomeCoroutine(whaleWithState w) {
		print ("PAUSE");
		yield return new WaitForSeconds (10f);
		print ("DONE WITH PAUSE");
	}
}