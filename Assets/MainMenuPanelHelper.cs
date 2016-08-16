using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenuPanelHelper : MonoBehaviour {

	private Button playButton;
	private Button quitButton;

	// Use this for initialization
	void Start () {
	
		playButton = GameObject.Find ("PlayButton").GetComponent<Button>();
		quitButton = GameObject.Find ("QuitButton").GetComponent<Button>();

		playButton.onClick.AddListener(playButtonEvent);
		quitButton.onClick.AddListener (quitButtonEvent);

		Screen.orientation = ScreenOrientation.Portrait;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) 
			Application.Quit(); 
	}


	private void playButtonEvent(){
		Application.LoadLevel ("gameScene");
	}
	private void quitButtonEvent(){
		Application.Quit ();
	}
}
