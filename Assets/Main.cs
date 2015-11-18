using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour
{

	public GameObject growingGameButton;
	public GameObject lineGameButton;
	public GameObject aquariumGameButton;

	private Vector3 offscreen1 = new Vector3 (-12, 5, 1);
	private Vector3 offscreen2 = new Vector3 (14, -6, 1);
	private Vector3 offscreen3 = new Vector3 (3, 8, 1);

	private Vector3 onscreen1 = new Vector3 (0f, 5f, 0f);
	private Vector3 onscreen2 = new Vector3 (10f, 5f, 0f);
	private Vector3 onscreen3 = new Vector3 (5f, 5f, 0f);

	public Vector3 clickedPos = new Vector3 (-100, -100, -100); //kevin
	public bool kinectClickedOn = false;

	private AquariumMusic music;

	// Use this for initialization
	void Start ()
	{
		music = GetComponent<AquariumMusic> ();
		music.PlayBackground ();

		// Initialize each of the fish in the game
		growingGameButton  = Instantiate (growingGameButton);
		lineGameButton     = Instantiate (lineGameButton);
		aquariumGameButton = Instantiate (aquariumGameButton);

		Utility.InitializeFish (growingGameButton,  offscreen1);
		Utility.InitializeFish (lineGameButton,     offscreen2);
		Utility.InitializeFish (aquariumGameButton, offscreen3);

		DisableGames ();

		MoveOnScreen ();
	}


	void OnGUI() {
		GUI.Label(new Rect(300, 500, 400, 600), "Grow Game!");
		GUI.Label(new Rect(800, 500, 900, 600), "Aquarium!");
		GUI.Label(new Rect(1250, 500, 1350, 600), "Line Game!");
	}
	
	// Update is called once per frame
	void Update ()
	{
		ActionObject growingGame  = growingGameButton.GetComponent<ActionObject> ();
		ActionObject lineGame     = lineGameButton.GetComponent<ActionObject> ();
		ActionObject aquariumGame = aquariumGameButton.GetComponent<ActionObject> ();

		// If the proper fish is clicked on, grow it
		if (growingGame.ClickedOn (kinectClickedOn, clickedPos))
		{
			MoveOffScreen();
			music.PlayTransition();
			GetComponent<GrowingTeamGame>().enabled = true;
		}
		else if (lineGame.ClickedOn (kinectClickedOn, clickedPos))
		{
			MoveOffScreen();
			music.PlayTransition();
			GetComponent<LineGame>().enabled = true;
		}
		else if (aquariumGame.ClickedOn (kinectClickedOn, clickedPos))
		{
			MoveOffScreen();
			music.PlayTransition();
			GetComponent<AquariumGame>().enabled = true;
		}

		kinectClickedOn = false;
	}

	void OnEnable()
	{	
		Camera mainCam;
		mainCam = Camera.main;
		mainCam.transform.position = new Vector3 (5f, 5f, -7f);
		DisableGames ();
		MoveOnScreen ();
	}

	void MoveOnScreen()
	{
		Utility.MoveHelper (growingGameButton, onscreen1, lineGameButton, onscreen2, aquariumGameButton, onscreen3);
	}

	void MoveOffScreen()
	{
		Utility.MoveHelper (growingGameButton, offscreen1, lineGameButton, offscreen2, aquariumGameButton, offscreen3);
	}

	void DisableGames()
	{
		if (GetComponent<GrowingTeamGame>().enabled)
			GetComponent<GrowingTeamGame>().enabled = false;
		if (GetComponent<LineGame>().enabled)
			GetComponent<LineGame>().enabled = false;
		if (GetComponent<AquariumGame>().enabled)
			GetComponent<AquariumGame>().enabled = false;
	}

	// Getter & setter for position of the object
	public AquariumMusic mus {
		get {
			return music;
		}
		private set {
		}
	}
}

