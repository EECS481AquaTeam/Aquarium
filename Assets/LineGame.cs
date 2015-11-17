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

public class LineGame : MonoBehaviour {
	public static int count = 0;
	public static int numObjects = 4; //hard coded this now, but make it dynamic later?
	public static int lineCount = 0;
	public static int timer = 1000;
	public static bool shouldDive = false;
	public Vector3 targetPos = new Vector3(-5,0,0);
	
	public new AudioSource audio;
	public AudioClip positive;
	public static bool audioIsPlaying = false;
	
	public bool isAtTargetPos = false;
	
	public GameObject[] ws;
	
	whaleWithState[] whaleList = new whaleWithState[4];
	
	void Start() {
		audio = GetComponent<AudioSource> ();
		for (int i = 0; i < 4; ++i) {
			Debug.Log("target Position " + targetPos);
			whaleList [i] = new whaleWithState (Instantiate(ws[i]), objectState.NORMAL, targetPos);
			targetPos.x = targetPos.x + 3;
		}
		/*
		Instantiate (testWhale);
		testWhale.GetComponent<Whale>().Dive ();*/
	}
	
	void Update() {
		foreach (whaleWithState w in whaleList) {
			Whale script = w.whale.GetComponent<Whale>();
			switch (w.state) {
			case objectState.NORMAL:
				print ("NORMAL");
				if (script.ClickedOn ()) {
					print ("clicked on is true");
					w.state = objectState.MOVINGTO;
					script.MoveTowardsTarget(w.targetPos);
					break;
				}
				break;
			case objectState.MOVINGTO:
				print ("MOVING TO");
				if (Utility.V3Equal(script.pos, w.targetPos)) {
					lineCount++;
					if (lineCount == numObjects) {
						//all objects must dive
						SetFeedbackAudio();
						foreach (whaleWithState item in whaleList) {
							item.state = objectState.SHOULD_DIVE;
						}
					} 
					else {
						w.state = objectState.DONE;
					}
				}
				break;
			case objectState.SHOULD_DIVE:
				print ("DIVING");
				w.targetPos.x--;
				w.targetPos.y = -2;
				script.MoveTowardsTarget(w.targetPos);
				w.state = objectState.DIVING;
				lineCount--;
				break;
			case objectState.DIVING:
				print ("IS DIVING");
				if (Utility.V3Equal(script.pos, w.targetPos)) {
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
				break;
			case objectState.DONE:
				break;
			case objectState.RESTART:
				Vector3 whalePos = Utility.GetRandomVector(8);
				script.pos = whalePos;
				script.targetLocation = whalePos;
				w.state = objectState.NORMAL;
				w.targetPos.y = 0;
				lineCount--;
				audioIsPlaying = false;
				break;
			}
		}
		
	}
	
	void SetFeedbackAudio() {
		if (!audioIsPlaying) {
			audio.clip = positive;
			audio.Play ();
			audioIsPlaying = true;
		}
	}
	
	private IEnumerator SomeCoroutine(whaleWithState w) {
		print ("PAUSE");
		yield return new WaitForSeconds (10f);
		print ("DONE WITH PAUSE");
	}
}