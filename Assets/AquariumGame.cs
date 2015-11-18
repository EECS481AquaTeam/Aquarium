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

	private AquariumMusic music;  // how this module plays music in the application
	
//	public new AudioSource audio;
//	public new AudioClip positive;
//	public static bool audioIsPlaying = true;
	
	public bool end = false;
	
	public GameObject[] ws2;
	
	public whaleClass[] whaleList2 = new whaleClass[7];
	
	void Start() {
		if (GetComponent<Main> ().enabled)
			GetComponent<Main> ().enabled = false;

		music = GetComponent<AquariumMusic>();
		
//		audio = GetComponent<AudioSource> ();
		Debug.Log ("Start");
	}
	
	void OnEnable()
	{
		Debug.Log ("Aquarium Game Enabled");
//		audioIsPlaying = false;
		if (GetComponent<Main>().enabled)
			GetComponent<Main>().enabled = false;

		for (int i = 0; i < numObjects; ++i) {
			ws2[i] = Instantiate(ws2[i]);
			ws2[i].GetComponent<ActionObject>().Initialize(Utility.GetRandomVector(), Random.Range (0.5f, 1.5f));

			whaleList2 [i] = new whaleClass (ws2[i], targetPos);

			Debug.Log ("Instantiate whale"); 
		}

		foreach (whaleClass w in whaleList2) {
			//w.whale.GetComponent<Whale>().Initialize(startPos, 0.5f);
			//offscreenPos.y = Random.Range(-10,10);
			//offscreenPos.x = -12f;
			w.whale.GetComponent<ActionObject>().MoveTowardsTarget(offscreenPos);
			//startPos.y = startPos.y-1;
		}
	}
	// Update is called once per frame
	void Update () {
		foreach (whaleClass w in whaleList2) {
			if (w.whale.GetComponent<ActionObject>().ClickedOn()) {
//				audioIsPlaying = false;
//				SetFeedbackAudio();
				music.PlayFeedback(music.pos);
			}
			if (Utility.V3Equal(w.whale.GetComponent<ActionObject>().pos, offscreenPos)) {
				w.targetReached = true;
			}
			if (w.targetReached) {
				lineCount++;
			}
		}
		if (lineCount == numObjects) {
			GetComponent<AquariumGame> ().enabled = false;
			GetComponent<Main> ().enabled = true;

			foreach (whaleClass w in whaleList2) {
				Vector3 off = new Vector3(-20,0,0);
				w.whale.GetComponent<ActionObject>().MoveTowardsTarget(off);
			}
		}
		Debug.Log ("Increment line count" + lineCount);
		lineCount = 0;

	}
//	
//	void SetFeedbackAudio() {
//		if (!audioIsPlaying) {
//			print ("play sound");
//			audio.clip = positive;
//			audio.loop = false;
//			audio.Play ();
//			audioIsPlaying = true;
//		}
//	}
}