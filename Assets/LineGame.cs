
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

	public Vector3 offscreenPos = new Vector3 (-12,-1,1);

	public new AudioSource audio;
	public new AudioClip positive;
	public static bool audioIsPlaying = false;

	public bool onscreen = false;
	public bool end = false;

	public bool isAtTargetPos = false;
	public Vector3 whalePos;

	public GameObject[] ws;
	
	whaleWithState[] whaleList = new whaleWithState[4];
	
	void Start() {
		if (GetComponent<Main> ().enabled)
			GetComponent<Main> ().enabled = false;

		audio = GetComponent<AudioSource> ();
		for (int i = 0; i < 4; ++i) {
			Debug.Log ("target Position " + targetPos);
			whaleList [i] = new whaleWithState (Instantiate(ws[i]), objectState.NORMAL, targetPos);
			targetPos.x = targetPos.x + 3;
		}
		/*
		Instantiate (testWhale);
		testWhale.GetComponent<Whale>().Dive ();*/
	}

	void OnEnable()
	{
		if (GetComponent<Main>().enabled)
			GetComponent<Main>().enabled = false;

		onscreen = true;
		MoveOnScreen (onscreen);
	}
	
	// Initializes the location of a fish
	void InitializeFish(GameObject item, Vector3 location)
	{
		item.GetComponent<ActionObject>().Initialize(location, 5f);
	}

	void Update() {
		if (end) {
			foreach(whaleWithState w in whaleList) {
				w.whale.GetComponent<ActionObject>().MoveTowardsTarget(offscreenPos);
				offscreenPos.y = offscreenPos.y+1;
			}
			GetComponent<Main>().enabled = true;
		}
		else {
			foreach (whaleWithState w in whaleList) {
				Whale script = w.whale.GetComponent<Whale>();
				switch (w.state) {
				case objectState.NORMAL:
						if (script.ClickedOn ()) {
							w.state = objectState.MOVINGTO;
							script.MoveTowardsTarget(w.targetPos);
							break;
						}
					break;
				case objectState.MOVINGTO:
					//print ("MOVING TO");
					if (ActionObject.V3Equal(script.pos, w.targetPos)) {
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
					//print ("DIVING");
					w.targetPos.x--;
					w.targetPos.y = -2;
					script.MoveTowardsTarget(w.targetPos);
					w.state = objectState.DIVING;
					lineCount--;
					break;
				case objectState.DIVING:
					//print ("IS DIVING");
					if (ActionObject.V3Equal(script.pos, w.targetPos)) {
						lineCount++;
						print ("Line count = " + lineCount);
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
					//print ("RESTART");
					audioIsPlaying = false;
					w.state = objectState.DONE;
					end = true;
					/*
					whalePos = script.GetRandomVector(8);
					script.pos = whalePos;
					script.targetLocation = whalePos;
					w.state = objectState.NORMAL;
					w.targetPos.y = 0;
					lineCount--;
					audioIsPlaying = false;
					*/
					break;
				}
			}
		}

	}

	void MoveOnScreen(bool on)
	{
		MoveHelper (on);
	}
	
	void MoveOffScreen(bool off)
	{
		MoveHelper (off);
	}
	
	void MoveHelper(bool on)
	{
		if (on) {
			for (int i = 0; i < 4; ++i) { 
				ws[i].GetComponent<ActionObject> ().MoveTowardsTarget (GetComponent<ActionObject> ().GetRandomVector (8));
			}
		} else {
			print ("MOVING OFFSCREEN"); 
			GetComponent<ActionObject> ().MoveTowardsTarget (offscreenPos);
		}
	}

	void SetFeedbackAudio() {
		if (!audioIsPlaying) {
			audio.clip = positive;
			audio.Play ();
			audioIsPlaying = true;
		}
	}
}