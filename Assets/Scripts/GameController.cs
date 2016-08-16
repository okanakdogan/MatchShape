using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {

	public bool readyForStart;

	public GameObject[] shapePrefabs;
	public Color[] colors;
	private Transform homeShapeSpawn;
	private float fallSpeed;
	public int state;
	private GameObject targetShape;
	private Transform targetShapeSpawn;
	public GameObject homeShape;
	private ShapeMatchChecker smc;
	private int lastColorInd;

	private int score;
	private float timeLeft;
	/*
	 * states:
	 * 
	 * 0 - create shape
	 * 1 - send down
	 * 2 - result
	 * 
	 */
	// Use this for initialization


	void Start () {
	
		//init 
		Screen.orientation = ScreenOrientation.Portrait;
		state = 0;
		homeShapeSpawn = GameObject.Find ("PlayerShapeSpawn").transform;
		targetShapeSpawn = GameObject.Find ("ShapeSpawn").transform;
		fallSpeed = .5f;
		//start game
		homeShape = null;
		smc = new ShapeMatchChecker() ;
		colors = new Color[]{Color.black,Color.blue,Color.cyan,
		Color.green,Color.red,Color.yellow};
		lastColorInd = 0;
		score = 0;
		timeLeft = 60f;
		readyForStart = true;
	}
	
	// Update is called once per frame
	void Update () {
		//**Game Loop

		//time update
		if (readyForStart) {

			//timeLeft -= Time.deltaTime;
			GameObject.Find ("Canvas/TimeText").GetComponent<Text> ().text = "T:" + Mathf.Round (timeLeft).ToString ();
			if(timeLeft<=0){
				readyForStart=false;
				//game over
				//show score bigger
			}

			if (state == 0) {
				//Create Shape
				targetShape = createShape ();


				//Rotate random
				changeShapeStat (targetShape);
				state = 1;

			} else if (state == 1) {
				//send down
				if (sendShapeDown (targetShape))
					state = 2;
			} else if (state == 2) {
				//check match cond.

				bool res = smc.checkShapeMatch (targetShape, homeShape);

				//Destroy(targetShape);
				Destroy (targetShape);
				//result
				result (res);
				//clean screen

				state++;
			}
			//change state
			if (state > 2)
				state = 0;
			//go back
		}
	}

	private GameObject createShape(){
		// select one prefab
		int shapeIndex = Random.Range(0,shapePrefabs.Length);
		//spawn it
		targetShape = (GameObject)Instantiate (shapePrefabs [shapeIndex], targetShapeSpawn.position, targetShapeSpawn.rotation);
		//return it
		return targetShape;
	}

	private void changeShapeStat(GameObject shape){
		int angle = Random.Range (-30, 30);
		//Debug.Log ("RandomAngle: " + angle);
		//rotate
		//shape.transform.Rotate (new Vector3(0f,0f,(float)angle));
		//Maybe more transforms
		//change color
		int colorInd = 0;
		while( (colorInd=Random.Range (0,colors.Length))==lastColorInd);
		lastColorInd = colorInd;
		shape.GetComponent<SpriteRenderer> ().color = colors [colorInd];

	}

	private bool sendShapeDown(GameObject shape){
		// vector lerp here 
		if(shape!=null)
			shape.transform.position = Vector3.Lerp (shape.transform.position,homeShapeSpawn.transform.position,
			                                         Time.deltaTime*fallSpeed);
		// check collider
		//homeCollider.bounds.Intersects (shape);
		// temp location check

		if (Vector3.Distance (shape.transform.position, homeShapeSpawn.position) < 1)
			return true;

		//maybe for many objects
		return false;
	}
	/*
	private bool checkMatchCond(GameObject targetShape, GameObject myShape){
		// check their stats and make judgement

		//check shapeType
		//call shapeChecker script let them match
		if (targetShape.GetComponent<SpriteRenderer> ().sprite.name.Equals (
			myShape.GetComponent<SpriteRenderer> ().sprite.name)==false) {
			Debug.Log("MatchCond: False");
			return false;
		}

		Debug.Log("MatchCond: True");
		return true;
	}*/

	private void result(bool matchResult){
		// give points or punish
		Debug.Log ("Match result: " + matchResult);
		if (matchResult == true) {
			score+=10;
			//update score
			GameObject.Find("Canvas/ScoreText").GetComponent<Text>().text=score.ToString();
		}
		// destroy objects
	}
	/*
	private int getPeriodAngle(string shapeName){
		int periodAngle = 60f;
		
		if (shapeName.Equals ("triangle")) {
			periodAngle=60f;
		} else if (shapeName.Equals ("square")) {
			periodAngle=90f;
		}
		else if (shapeName.Equals ("rectangle")) {
			periodAngle=90f;
			
		}else if (shapeName.Equals ("star")) {
			periodAngle=90f;
		}
		return periodAngle;
	}*/

}
