using UnityEngine;
using System.Collections;

public enum objectState {NORMAL, MOVINGTO, DONE, SHOULD_DIVE, DIVING, RESTART};
public class whaleWithState {
	public GameObject whale;
	public objectState state;
	public Vector3 targetPos;
	public Vector3 diveTargetPos;
	public int timer = 1000;
	public whaleWithState(GameObject w, objectState s, Vector3 targetPosition, Vector3 diveTargetPosition) {
		whale = w;
		state = s;
		targetPos = targetPosition;
		diveTargetPos = diveTargetPosition;
	}
}

public class LineGame : MonoBehaviour {
	public static int count = 0;
	public static int numObjects = 4; 
	public static int lineCount = 0;

	public Vector3 targetPos = new Vector3(-5,0,0);
	public Vector3 diveTargetPos = new Vector3 (0, 0, 0);

	public Vector3 offscreenPos = new Vector3 (-12,0,0);

	public Vector3 onscreenPos;

	public new AudioSource audio;
	public new AudioClip positive;
	public static bool audioIsPlaying = true;
	
	public bool end = false;

	public Vector3 whalePos;

	public GameObject[] ws;
	
	public whaleWithState[] whaleList = new whaleWithState[4];
	
	void Start() {
		if (GetComponent<Main> ().enabled)
			GetComponent<Main> ().enabled = false;

		audio = GetComponent<AudioSource> ();
		Debug.Log ("Start");
		for (int i = 0; i < 4; ++i) {
			whaleList [i] = new whaleWithState (Instantiate(ws[i]), objectState.NORMAL, targetPos, diveTargetPos);
			Debug.Log ("Instantiate whale");
			targetPos.x = targetPos.x + 3;
		}
		/*
		Instantiate (testWhale);
		testWhale.GetComponent<Whale>().Dive ();*/
	}

	void OnEnable()
	{
		Debug.Log ("Line Game Enabled");
		end = false;
		if (GetComponent<Main>().enabled)
			GetComponent<Main>().enabled = false;

		foreach (whaleWithState w in whaleList) {
			w.state = objectState.NORMAL;
			onscreenPos = Utility.GetRandomVector(8);
			w.whale.GetComponent<Whale>().MoveTowardsTarget(onscreenPos);
		}
	}
	
	// Initializes the location of a fish
	void InitializeFish(GameObject item, Vector3 location)
	{
		item.GetComponent<ActionObject>().Initialize(location, 5f);
	}

	void Update() {
		if (end) {
			Debug.Log ("Moving offscreen");
			foreach (whaleWithState w in whaleList) {
				w.whale.GetComponent<Whale>().MoveTowardsTarget(offscreenPos);
			}
			GetComponent<LineGame>().enabled = false;
			GetComponent<Main>().enabled = true;
		}
		else {
			audioIsPlaying = false;
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
					//print ("DIVING");
					w.diveTargetPos.x = w.targetPos.x - 1;
					w.diveTargetPos.y = w.targetPos.y - 2;
					script.MoveTowardsTarget(w.diveTargetPos);
					w.state = objectState.DIVING;
					lineCount--;
					break;
				case objectState.DIVING:
					//print ("IS DIVING");
					if (Utility.V3Equal(script.pos, w.diveTargetPos)) {
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
					Debug.Log ("RESTART");
					foreach (whaleWithState item in whaleList) {
						item.state = objectState.DONE;
						lineCount--;
					}
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

	void MoveOnScreen(GameObject g1,GameObject g2, GameObject g3, GameObject g4)
	{
		Vector3 v1 = Utility.GetRandomVector (8);
		Vector3 v2 = Utility.GetRandomVector (8);
		Vector3 v3 = Utility.GetRandomVector (8);
		Vector3 v4 = Utility.GetRandomVector (8);
		MoveHelper (g1, v1, g2, v2, g3, v3, g4, v4);
	}
	
	void MoveOffScreen(GameObject g1, GameObject g2, GameObject g3, GameObject g4)
	{
		Vector3 v1 = Utility.GetRandomVector (14);
		Vector3 v2 = Utility.GetRandomVector (14);
		Vector3 v3 = Utility.GetRandomVector (14);
		Vector3 v4 = Utility.GetRandomVector (14);
		MoveHelper (g1, v1, g2, v2, g3, v3, g4, v4);
	}
	
	void MoveHelper(GameObject g1, Vector3 v1, GameObject g2, Vector3 v2, GameObject g3, Vector3 v3, GameObject g4, Vector3 v4)
	{
			g1.GetComponent<ActionObject> ().MoveTowardsTarget(v1);
			g2.GetComponent<ActionObject> ().MoveTowardsTarget(v2);
			g3.GetComponent<ActionObject> ().MoveTowardsTarget(v3);
			g4.GetComponent<ActionObject> ().MoveTowardsTarget(v4);
	}

	void SetFeedbackAudio() {
		if (!audioIsPlaying) {
			print ("play sound");
			audio.clip = positive;
			audio.loop = false;
			audio.Play ();
			audioIsPlaying = true;
		}
	}
}