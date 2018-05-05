using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.iOS;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class gardenMaker : MonoBehaviour {

	public float createHeight;
	public float maxRayDistance = 30.0f;
	public LayerMask collisionLayer = 1 << 10;  //ARKitPlane layer
	private MaterialPropertyBlock props;
	private Vector3 placeLevel;
	public GameObject gardenController;
	private int totalBees;
	private float terrainWidth;
	private float terrainLength;
	private float centerZ = 0;
	private float centerX = 0;
	public GameObject twoFlower;
	private GameObject parentHolder;
	private int currentLevel;
	private int currentFlower;
	public GameObject alert;
	public Text beesCreated;
	public Text beesRemaining;
	public GameObject bee;
	public Text levelText;
	public Text beeNum;
	public RawImage popUp;
	public Text flowerNum;
	private int remainingFlowers;
	private int beesFed;
	private int flowersCreated;
	private int tapCount;
    public int currentFlowerType;

	public RawImage popUp2;

	public Stack<GameObject> beeStack;

	public Stack<GameObject> flowerStack;

	// Use this for initialization
	void Start () {
		tapCount = 0;

        //ARKitWorldTrackingSessionConfiguration sessionConfig = new ARKitWorldTrackingSessionConfiguration (UnityARAlignment.UnityARAlignmentCamera, UnityARPlaneDetection.Horizontal);
        //UnityARSessionNativeInterface.GetARSessionNativeInterface ().RunWithConfigAndOptions (sessionConfig, UnityARSessionRunOption.ARSessionRunOptionRemoveExistingAnchors | UnityARSessionRunOption.ARSessionRunOptionResetTracking);
		popUp2.gameObject.SetActive (false);
		totalBees = 0;
		currentFlower = 0;
		createHeight = 0;
		beesFed = 0;
		flowersCreated = 0;
		props = new MaterialPropertyBlock ();
		// terrain size x
		terrainWidth = .5f;
		// terrain size z
		terrainLength =.5f;
		beeStack = new Stack<GameObject> ();
		flowerStack = new Stack<GameObject> ();
		keeper.Instance.currentGardenLevel++;

		beeNum.text = keeper.Instance.gardenBeesTwo.ToString ();

		parentHolder = new GameObject("BeeParent");
		parentHolder.tag = "bee";
		levelText.text = keeper.Instance.currentGardenLevel.ToString();
		beeNum.text = gardenController.GetComponent<gardenController> ().gardenBees+"";
		Debug.Log ("current level "+ keeper.Instance.currentGardenLevel);
		setLevel ();
		//flowerNum.text = beesFed+ " Bees fed";

	}

	public void CreateBall(Vector3 atPosition, GameObject dingdong)
	{
		GameObject ballGO = Instantiate (dingdong, atPosition, Quaternion.identity);
		ballGO.transform.RotateAround (ballGO.transform.position, new Vector3 (0, 1, 0), Random.Range (0, 360));
		flowerStack.Push (ballGO);
		Debug.Log ("flower placement: "+atPosition.x+" "+atPosition.y+" "+atPosition.z);

	}

	// Update is called once per frame
	void Update () {
		remainingFlowers = gardenController.GetComponent<gardenController>().gardenFlowers-currentFlower;

		//int tempo = gardenController.GetComponent<gardenController>().gardenFlowers;
		int tempo = 100;
		beesCreated.text = gardenController.GetComponent<gardenController>().gardenBees-beeStack.Count+" bees fed";
		beesRemaining.text = beeStack.Count+" bees left";

		if (Input.touchCount > 0 )
		{
			var touch = Input.GetTouch(0);
			if (touch.phase == TouchPhase.Began)
			{
				var screenPosition = Camera.main.ScreenToViewportPoint(touch.position);
				ARPoint point = new ARPoint {
					x = screenPosition.x,
					y = screenPosition.y
				};
						
				List<ARHitTestResult> hitResults = UnityARSessionNativeInterface.GetARSessionNativeInterface ().HitTest (point, 
					ARHitTestResultType.ARHitTestResultTypeExistingPlaneUsingExtent);
				if (hitResults.Count > 0) {
					foreach (var hitResult in hitResults) {
						Vector3 position = UnityARMatrixOps.GetPosition (hitResult.worldTransform);
						//add createheight to the y below
						placeLevel = new Vector3 (position.x, position.y, position.z);

						bool UIZ= IsPointerOverUIObject();
						if (currentFlower<1000 && !UIZ){
							if (tapCount==0){
								
			
							}
							alert.SetActive(false);
							CreateBall(placeLevel, twoFlower);
							currentFlower++;
							Debug.Log("current flower: "+currentFlower+", total bees: "+beeStack.Count+", position:"+ placeLevel.x+", "+placeLevel.y+", "+placeLevel.z);
							tapCount++;
							// + ", remaining flowers: "+remainingFlowers
						}
                        else if ( totalBees==currentFlowerType && popUp.IsActive()==false){
							//
							
							Debug.Log("SCENE RESET");
							popUp2.gameObject.SetActive(true);


						}
						else if (keeper.Instance.currentGardenLevel==4){
							//

							SceneManager.LoadScene("Beez");
						}
						else{
							Debug.Log ("TRY AGAIN");
						}

						break;
					}
				}

			}
		}


	}

	public void setLevel(){
		//
		currentLevel = keeper.Instance.currentGardenLevel;
        currentFlowerType = keeper.Instance.flowerTypesGarden[currentLevel];
        Debug.Log("max tapperz: " + currentFlowerType);
		//alert.gameObject.SetActive (false);
		setLevels ();

	}

	void setLevels(){
		//destroy all islands, bees, flowers and reset which flowers are in the field, and which bees are in the stack

		DestroyGameObjectsWithTag ("beeParent");
		DestroyGameObjectsWithTag ("flower");
		beeStack.Clear ();

		//create flowers, bees and island
		//levelator (placeLevel, gardenController.GetComponent<gardenController>().gardenBees);
		placement(gardenController.GetComponent<gardenController>().gardenBees, bee);


	}
	public void placement(int boop, GameObject objectToPlace){

		//createBeesCalled = true;
		totalBees = totalBees + boop;
		Debug.Log (totalBees);

		for (int i =0; i<boop; i++){
			//
			float posx = Random.Range (centerX-terrainWidth, centerX + terrainWidth);
			float posz = Random.Range (centerZ-terrainLength, centerZ + terrainLength);
			GameObject newObject = (GameObject)Instantiate (objectToPlace, new Vector3 (posx, .1f, posz), Quaternion.identity);

			GameObject parental = (GameObject)Instantiate (parentHolder,new Vector3 (posx, .1f, posz), Quaternion.identity);
			newObject.transform.SetParent(parental.transform);
			newObject.transform.RotateAround (newObject.transform.position, new Vector3 (0, 1, 0), Random.Range (0, 360));
			parental.tag = "beeParent";
			newObject.tag = "bee";
			beeStack.Push (newObject);

		}


	}
	public static void DestroyGameObjectsWithTag(string tag)
	{
		GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(tag);
		foreach (GameObject target in gameObjects) {
			GameObject.Destroy(target);
		}
	}

	public void moveBees(int numBees, GameObject flower) {
		//
		for (int i = 0; i < numBees; i++) {
			//get a bee 
			GameObject topBee = beeStack.Pop ();
			GameObject parental = topBee.transform.parent.gameObject;
			topBee.transform.parent = parental.transform;

			//scale bee down, parent bee to flower, set position to flower
			parental.transform.localScale = new Vector3 (.4f, .4f, .4f);
			parental.transform.SetParent (flower.transform);
			parental.transform.localPosition = keeper.Instance.twoBees [i];
			Destroy (topBee.GetComponent<Rigidbody> ());
			totalBees=totalBees-1;
			Handheld.Vibrate ();
		}
	}

	private bool IsPointerOverUIObject(){
		//
		PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
		eventDataCurrentPosition.position = new Vector2 (Input.touches [0].position.x, Input.touches [0].position.y);
		List<RaycastResult> results = new List<RaycastResult> ();
		EventSystem.current.RaycastAll (eventDataCurrentPosition, results);
		Debug.Log ("ALERT: POINTER CHECK");
		return results.Count > 0;
	}
	public void moveAllBees() {
		//
		flowersCreated = flowersCreated+flowerStack.Count;
		int numFlowersTemp = flowerStack.Count;
		for (int i = 0; i < numFlowersTemp; i++) {
			Debug.Log ("number of flowers: " + flowerStack.Count+" bee count: "+beeStack.Count);
			GameObject topFlower =  flowerStack.Pop ();

            for (int j = 0; j < currentFlowerType; j++) {
				//get a bee 
				GameObject topBee = beeStack.Pop ();
				GameObject parental = topBee.transform.parent.gameObject;
				topBee.transform.parent = parental.transform;

				//scale bee down, parent bee to flower, set position to flower
				parental.transform.localScale = new Vector3 (.4f, .4f, .4f);
				parental.transform.SetParent (topFlower.transform);
				parental.transform.localPosition = keeper.Instance.twoBees [j];
				Destroy (topBee.GetComponent<Rigidbody> ());
                totalBees = totalBees - currentFlowerType;
				beesFed++;
			}
			Handheld.Vibrate ();
		}
		if (beeStack.Count == 0) {
            StartCoroutine(waitPlease());
			//SceneManager.LoadScene ("gotEm");
		}
        if (beesFed/currentFlowerType > flowersCreated){
            //
            flowerNum.gameObject.SetActive(true);
			flowerNum.text = "too many flowers!";
		}
        else if (beesFed/currentFlowerType <flowersCreated){
            //
            flowerNum.gameObject.SetActive(true);
			flowerNum.text = "some bees are still hungry!";
		}
	}
    public void deactivateMe(GameObject me){
       me.SetActive(true);
    }
    IEnumerator waitPlease()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("gotEm");
    }
}
