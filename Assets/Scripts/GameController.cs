using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public bool readyForStart;

	public GameObject[] shapePrefabs;
	private Transform homeShapeSpawn;
	private float fallSpeed;
	public int state;
	private GameObject targetShape;
	private Transform targetShapeSpawn;
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
		state = 0;
		homeShapeSpawn = GameObject.Find ("PlayerShapeSpawn").transform;
		targetShapeSpawn = GameObject.Find ("ShapeSpawn").transform;
		fallSpeed = 1f;
		//start game

		readyForStart = false;
	}
	
	// Update is called once per frame
	void Update () {
		//**Game Loop

		if (state == 0) {
			//Create Shape
			targetShape=createShape();

			//Rotate random

			state=1;

		} else if (state == 1) {
			//send down
			Debug.Log("SendDown");
			if( sendShapeDown(targetShape))
				state=2;
		}else if(state==2){
			//check match cond.
				//temp delete for now
			Destroy(targetShape);
			//result

			//clean screen

			state++;
		}
		//change state
		if(state>2)
			state = 0;
		//go back
	}

	private GameObject createShape(){
		// select one prefab
		int shapeIndex = Random.Range(0,shapePrefabs.Length);
		//spawn it
		targetShape = (GameObject)Instantiate (shapePrefabs [shapeIndex], targetShapeSpawn.position, targetShapeSpawn.rotation);
		//return it
		return targetShape;
	}

	private GameObject changeShapeStat(GameObject shape){

		//rotate

		//Maybe more transforms

		return null;
	}

	private bool sendShapeDown(GameObject shape){
		// vector lerp here 
		if(shape!=null)
			shape.transform.position = Vector3.Lerp (shape.transform.position,homeShapeSpawn.transform.position,
			                                         Time.deltaTime*fallSpeed);
		// check collider
		// temp location check

		if (Vector3.Distance (shape.transform.position, homeShapeSpawn.position) < 1)
			return true;

		//maybe for many objects
		return false;
	}

	private bool checkMatchCond(GameObject targetShape, GameObject myShape){
		// check their stats and make judgement

		return false;
	}

	private void result(){
		// give points or punish

		// destroy objects
	}


}
