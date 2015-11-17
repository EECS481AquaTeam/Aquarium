using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour
{

	public GameObject growingGameButton;
	public GameObject lineGameButton;
	public GameObject aquariumGameButton;

	public Vector3 offscreen1 = new Vector3 (-12, 5, 1);
	public Vector3 offscreen2 = new Vector3 (14, -6, 1);
	public Vector3 offscreen3 = new Vector3 (3, 8, 1);

	public Vector3 onscreen1 = new Vector3 (-6, 1, 1);
	public Vector3 onscreen2 = new Vector3 (6, 1, 1);
	public Vector3 onscreen3 = new Vector3 (0, 1, 1);

	public AquariumMusic music;

	// Use this for initialization
	void Start ()
	{
		music = GetComponent<AquariumMusic> ();

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
	
	// Update is called once per frame
	void Update ()
	{
		ActionObject growingGame  = growingGameButton.GetComponent<ActionObject> ();
		ActionObject lineGame     = lineGameButton.GetComponent<ActionObject> ();
		ActionObject aquariumGame = aquariumGameButton.GetComponent<ActionObject> ();

		// If the proper fish is clicked on, grow it
		if (growingGame.ClickedOn ())
		{
			MoveOffScreen();
			music.PlayTransition();
			GetComponent<GrowingTeamGame>().enabled = true;
		}
		else if (lineGame.ClickedOn ())
		{
			MoveOffScreen();
			music.PlayTransition();
			GetComponent<LineGame>().enabled = true;
		}
		else if (aquariumGame.ClickedOn ())
		{
			MoveOffScreen();
			music.PlayTransition();
//			aquariumGame.enabled = true;
		}
	}

	void OnEnable()
	{
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
		//		if (GetComponent<Aqu>().enabled)
		//			GetComponent<GrowingTeamGame>().enabled = false;
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

