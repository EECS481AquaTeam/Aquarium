
using UnityEngine;
using System.Collections;

public class ObjectSpawner : MonoBehaviour {
	public GameObject prefabVar;
	public string prefabType;
	
	void Start () {
//		print (prefabType);
		InvokeRepeating("CreateObject", 1, .01F);
	}

	void CreateObject() {
		Instantiate (prefabVar);
	}
}