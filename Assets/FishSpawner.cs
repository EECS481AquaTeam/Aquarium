using UnityEngine;
using System.Collections;

public class FishSpawner : MonoBehaviour {
	public GameObject fishPrefabVar;
	
	// Use this for initialization
	void Start () {
		InvokeRepeating("CreateFish", 1, 2f);
	}
	
	// Update is called once per frame
	void Update () {
		//CreateFish ();
	}

	void CreateFish() {
		Instantiate (fishPrefabVar);
	}
}
