using UnityEngine;
using System.Collections;

public class whaleClass {
	public GameObject whale;
	public Vector3 targetPos;
	public bool targetReached;
	public whaleClass(GameObject w, Vector3 targetPosition) {
		whale = w;
		targetPos = targetPosition;
		targetReached = false;
	}
}

public class AquariumGame : MonoBehaviour {
	public static int count = 0;
	public static int numObjects = 7; 
	public static int lineCount = 0;
	
	public Vector3 targetPos;
	public Vector3 offscreenPos = new Vector3 (-12,0,0);
	public Vector3 onscreenPos;
	public Vector3 startPos = new Vector3(12,3,1);
	
	public new AudioSource audio;
	public new AudioClip positive;
	public static bool audioIsPlaying = true;
	
	public bool end = false;
	
	public GameObject[] ws2;
	
	public whaleClass[] whaleList2 = new whaleClass[7];
	
	void Start() {
		if (GetComponent<Main> ().enabled)
			GetComponent<Main> ().enabled = false;
		
		audio = GetComponent<AudioSource> ();
		Debug.Log ("Start");
		for (int i = 0; i < numObjects; ++i) {
			whaleList2 [i] = new whaleClass (Instantiate(ws2[i]), targetPos);
			Debug.Log ("Instantiate whale"); 
		}
		foreach (whaleClass w in whaleList2) {
			w.whale.GetComponent<Whale>().MoveTowardsTarget(offscreenPos);
		}
	}

	void OnEnable()
	{
		Debug.Log ("Aquarium Game Enabled");
		audioIsPlaying = false;
		if (GetComponent<Main>().enabled)
			GetComponent<Main>().enabled = false;
		
		foreach (whaleClass w in whaleList2) {
			//w.whale.GetComponent<Whale>().Initialize(startPos, 0.5f);
			offscreenPos = Utility.GetRandomVector(3);
			offscreenPos.x = -12f;
			w.whale.GetComponent<Whale>().MoveTowardsTarget(offscreenPos);
			//startPos.y = startPos.y-1;
		}
	}
	// Update is called once per frame
	void Update () {
		foreach (whaleClass w in whaleList2) {
			if (w.whale.GetComponent<Whale>().ClickedOn()) {
				audioIsPlaying = false;
				SetFeedbackAudio();
			}
			if (Utility.V3Equal(w.whale.GetComponent<Whale>().pos, offscreenPos)) {
				w.targetReached = true;
			}
			if (w.targetReached) {
				lineCount++;
			}
		}
		if (lineCount == numObjects) {
			GetComponent<AquariumGame> ().enabled = false;
			GetComponent<Main> ().enabled = true;
		}
		lineCount = 0;
		Debug.Log ("Increment line count" + lineCount);
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
