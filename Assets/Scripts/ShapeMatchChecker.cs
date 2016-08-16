using UnityEngine;
using System.Collections;

public class ShapeMatchChecker : MonoBehaviour {

	public static float angleDiff;

	// Use this for initialization
	void Start () {
		angleDiff = 2f;
	}

	public bool checkShapeMatch(GameObject target,GameObject myShape){
		// check shapee types
		if (checkShapeType (target, myShape)) {
			if(checkShapeColor(target,myShape)){

				return true;
			}
			else
				return false;
		
		} else {
			return false;
		}

	}
	// take two shape check is same
	public static bool checkShapeType(GameObject target,GameObject myShape){
		/*
		if (target.GetComponent<SpriteRenderer> ().sprite.name.Equals (
			myShape.GetComponent<SpriteRenderer> ().sprite.name)==false) {*/

		if (shapeName(target).Equals (shapeName(myShape))==false) {
			Debug.Log("MatchCond: False");
			return false;
		}
		Debug.Log("MatchCond: True");
		return true;
	}
	public static string shapeName(GameObject shape){
		return shape.GetComponent<SpriteRenderer> ().sprite.name;
	}

	public bool checkShapeColor(GameObject target,GameObject myShape){
		return (target.GetComponent<SpriteRenderer> ().color == myShape.GetComponent<SpriteRenderer> ().color);
	}

	public static bool checkShapeRotation(GameObject target,GameObject myShape){

		// take type/name and work specific
		string typeName = shapeName (myShape);
		float periodAngle = 60f;

		if (typeName.Equals ("triangle")) {
			periodAngle=60f;
		} else if (typeName.Equals ("square")) {
			periodAngle=90f;
		}
		else if (typeName.Equals ("rectangle")) {
			periodAngle=90f;

		}else if (typeName.Equals ("star")) {
			periodAngle=90f;
		}



		float targetAngle = target.transform.rotation.eulerAngles.z % periodAngle;
		float myShapeAngle = myShape.transform.rotation.eulerAngles.z % periodAngle;
		Debug.Log("Check Rotation: typename: "+typeName+" target.z :"+targetAngle +"myShape.z: "+myShapeAngle);
		return (targetAngle-myShapeAngle < angleDiff);

	}
}
