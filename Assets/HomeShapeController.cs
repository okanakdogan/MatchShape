using UnityEngine;
using System.Collections;

public class HomeShapeController : MonoBehaviour {


	private GameController gc;
	private GameObject[] shapes;
	private Color[] colors;
	private GameObject homeShape;
	private GameObject leftShape;
	private GameObject rightShape;
	private int shapeIndex;
	private int colorIndex;
	private Transform homeShapeSpawn;

	private float M_SMALL=0.3f;
	private float M_BIG = 3f;
	private float moveSpeed=0.01f;

	//private SwipeManager swipeMngr;
	//For swipe
	/*
	private Vector2 firstPressPos;
	private Vector2 secondPressPos;
	private Vector2 currentSwipe;
*/
	// Use this for initialization
	void Start () {
		gc = GameObject.Find ("GameControl").GetComponent<GameController> ();
		shapes = gc.shapePrefabs;
		colors = gc.colors;
		homeShape = null;
		shapeIndex = 0;
		colorIndex = 0;
		homeShapeSpawn = GameObject.Find ("PlayerShapeSpawn").transform;
		//swipeMngr = GameObject.Find ("SwipeControl").GetComponent<SwipeManager> ();
	}
	
	// Update is called once per frame
	void Update () {
	
		// if game ready
		if(gc.readyForStart){
		// if have no shape create (you must have one child)
			if(homeShape==null){
				homeShape = createHomeShape();
				createSideShapes();
				updateGameContShape();
			}else{
				//input handle
				inputHandle();
			}
		

		}
	}

	private GameObject createHomeShape(){

		//select a shape and create
		//return it
		GameObject hShape = (GameObject)Instantiate (shapes [shapeIndex], homeShapeSpawn.position, homeShapeSpawn.rotation);
		hShape.GetComponent<SpriteRenderer> ().color = colors [colorIndex];
		return hShape;
	}

	private GameObject createLeftShape(){
		int leftIndex = (shapeIndex - 1) >= 0 ? shapeIndex - 1 : shapes.Length - 1;
		if (leftShape != null)
			Destroy (leftShape);
		leftShape = (GameObject)Instantiate (shapes [leftIndex], homeShapeSpawn.position + new Vector3 (-2, 0, 0),
		                                     homeShapeSpawn.rotation);
		scaleShape (leftShape, M_SMALL);
		leftShape.GetComponent<SpriteRenderer> ().color = colors [colorIndex];

		return leftShape;
	}

	private GameObject createRightShape(){
		int rightIndex = (shapeIndex + 1) < shapes.Length ? shapeIndex + 1 : 0;
		if (rightShape != null)
			Destroy (rightShape);
		rightShape=(GameObject)Instantiate (shapes [rightIndex], homeShapeSpawn.position + new Vector3 (2, 0, 0),
		                                    homeShapeSpawn.rotation);
		scaleShape (rightShape, M_SMALL);
		rightShape.GetComponent<SpriteRenderer> ().color = colors [colorIndex];
		return rightShape;
	}
	private void createSideShapes(){
		createLeftShape ();
		createRightShape ();
	}
	private void changeShape(){
		if (homeShape != null) {
			Destroy(homeShape);
			nextShapeIndex();
			homeShape=createHomeShape();
			homeShape.GetComponent<SpriteRenderer>().color= colors[colorIndex];
			updateGameContShape();
		}
	}
	private void swipeShape(int direction){

		if (direction < 0) {
			//swipe to left

			//kill left
			Destroy(leftShape);
			leftShape= homeShape;
			//leftShape.transform.position = homeShapeSpawn.transform.position + new Vector3(-2,0,0);
			scaleShape(leftShape,M_SMALL);

			homeShape=rightShape;
			//homeShape.transform.position= homeShapeSpawn.transform.position;
			scaleShape(homeShape,M_BIG);

			nextShapeIndex();
			updateGameContShape();

			//create right side
			rightShape=null;
			createRightShape();

		}else if(direction>0){
			//swipe to right

			//kill right
			Destroy(rightShape);
			rightShape=homeShape;
			//rightShape.transform.position = homeShapeSpawn.transform.position + new Vector3(2,0,0);
			scaleShape(rightShape,M_SMALL);

			homeShape=leftShape;
			//homeShape.transform.position=homeShapeSpawn.transform.position;
			scaleShape(homeShape,M_BIG);

			prevShapeIndex();
			updateGameContShape();

			//create left side
			leftShape=null;
			createLeftShape();
		}

	}
	private void scaleShape(GameObject shape ,float multp){

		shape.transform.localScale= new Vector3 (shape.transform.localScale.x*multp,
		                                         shape.transform.localScale.y*multp,
		                                         shape.transform.localScale.z);
	
	}
	private void inputHandle(){

		if (Input.GetKeyDown(KeyCode.Escape)) 
			Application.Quit(); 

		//tap

		if (SwipeManager.IsTapped ()) {
			//changeShape();
		}
		if (SwipeManager.IsSwipingLeft()) {
			//rotate left
			if(homeShape!=null){
				//homeShape.transform.Rotate(new Vector3(0,0,22.5f));

				swipeShape(-1);
				StartCoroutine("MoveShapesLeft");
			}

		}
		if (SwipeManager.IsSwipingRight ()) {
			//rotate right
			if(homeShape!=null){
				//homeShape.transform.Rotate(new Vector3(0,0,-22.5f));

				swipeShape(1);
				StartCoroutine("MoveShapesRight");

			}
		}
		if (SwipeManager.IsSwipingUp ()) {
			changeShapeColorsForw();
		}
		if (SwipeManager.IsSwipingDown ()) {
			changeShapeColorsBack();
		}
	}

	public void explodeAndDestroyHomeShape(bool correctMatch){

		//destroy shape
	}
	private void nextShapeIndex(){
		if (shapeIndex < shapes.Length - 1) {
			shapeIndex++;
		} else {
			shapeIndex=0;
		}
	}
	private void prevShapeIndex(){
		if (shapeIndex > 0) {
			--shapeIndex;
		} else {
			shapeIndex=shapes.Length-1;
		}
	
	}

	private void updateGameContShape(){
		gc.homeShape=homeShape;
	}
	private void changeShapeColorsForw(){
		colorIndex= (colorIndex+1)% colors.Length;
		changeShapeColorsWithIndex (colorIndex);
	}
	private void changeShapeColorsBack(){
		--colorIndex;
		if(colorIndex<0)
			colorIndex=colors.Length-1;
		changeShapeColorsWithIndex (colorIndex);
	}
	private void changeShapeColorsWithIndex(int colorInd){
		homeShape.GetComponent<SpriteRenderer> ().color = colors [colorInd];
		leftShape.GetComponent<SpriteRenderer> ().color = colors [colorInd];
		rightShape.GetComponent<SpriteRenderer> ().color = colors [colorInd];
	}

	//coroutine for swipe shape

	IEnumerator MoveShapesLeft() {

		while ((homeShape.transform.position - homeShapeSpawn.transform.position).magnitude >0.01f) {
			leftShape.transform.position = Vector3.Lerp (leftShape.transform.position,
		                                             homeShapeSpawn.transform.position + new Vector3 (-2, 0, 0),
		                                             Time.deltaTime * moveSpeed);

			homeShape.transform.position = Vector3.Lerp (homeShape.transform.position,
		                                            homeShapeSpawn.transform.position, Time.deltaTime * moveSpeed);
		}
		yield return null;
	}

	IEnumerator MoveShapesRight() {
		while ((homeShape.transform.position - homeShapeSpawn.transform.position).magnitude >0.01f) {
			rightShape.transform.position = Vector3.Lerp (rightShape.transform.position,
			                                             homeShapeSpawn.transform.position+new Vector3(2,0,0),
			                                             Time.deltaTime*moveSpeed);
			
			homeShape.transform.position = Vector3.Lerp (homeShape.transform.position,
			                                             homeShapeSpawn.transform.position, Time.deltaTime * moveSpeed);
		}
		yield return null;
	}
}


