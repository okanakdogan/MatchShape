using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public bool readyForStart;

	private int state;
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

		//start game

		readyForStart = false;
	}
	
	// Update is called once per frame
	void Update () {
		//**Game Loop

		if (state == 0) {
			//Create Shape

			//Rotate random
		} else if (state == 1) {
			//send down
		}else if(state==2){
			//check match cond.

			//result

			//clean screen
		}
		//change state
		//go back
	}

	private GameObject createShape(){
		// select one prefab

		//spawn it

		//return it
		return null;
	}

	private GameObject changeShapeStat(GameObject shape){

		//rotate

		//Maybe more transforms

		return null;
	}

	private bool sendShapeDown(GameObject shape){
		// vector lerp here 
		//maybe for many objects
	}

	private bool checkMatchCond(GameObject targetShape, GameObject myShape){
		// check their stats and make judgement

	}

	private void result(){
		// give points or punish

		// destroy objects
	}


}
