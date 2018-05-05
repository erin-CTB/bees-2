using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using UnityEngine.XR.iOS;
using UnityEngine.SceneManagement;

public class ModeSwitcher2 : MonoBehaviour {

	//public GameObject ballMake;
	//public GameObject ballMove;

	//private int appMode = 0;


	public string numberOfBees;
	//public GameObject controllerContainer;

	//public Terrain terrain;
	public int numberOfObjects; // number of objects to place
	private int currentObjects; // number of placed objects
	public GameObject objectToPlace; // GameObject to place - bee
	public GameObject island;
	public GameObject flower;
	private float terrainWidth; // terrain size (x)
	private float terrainLength; // terrain size (z)
	private int terrainPosX; // terrain position x
	private int terrainPosZ; // terrain position z
	public GameObject[] gems;
	public GameObject[] beesCreated;
	//private int totalBees = 0;
	public int num2; //array num
	//private bool move = false;
	private GameObject[] hives;
	private float centerZ = 0;
	private float centerX = 0;
	//private Vector3 myPosition;

	public int currentBee = 0;
	public Stack<GameObject> beeStack = new Stack<GameObject> ();

	public float hSliderValue = 0.0F;
	//private int grabValue = 0;
	private int totalBees = 0;

	private GameObject parentHolder;
	public Text caught;
	public Text left;

    public GameObject shovel;
	public bool createBeesCalled;

	public int currentLevel;

	public GameObject creator;
	public GameObject mover;
	public Vector3 localLevelPosition;

	public int maxTaps;
	public GameObject DND;
	public Text levelText;

	public Vector3[] flowerLocations;

	public Text maxer;
	public Text dope;

	public GameObject alert;
    private bool drawerInOut = true;


	// Use this for initialization
	void Start () {
		//set make or break mode
		setMode (true);
		
		// terrain size x
		terrainWidth = .5f;
		// terrain size z
		terrainLength =.5f;

		parentHolder = new GameObject("BeeParent");
		parentHolder.tag = "bee";
		//placement (numberOfObjects);
		createBeesCalled = false;
		flowerLocations = new Vector3[15];
		keeper.Instance.nextLevel ();


	}

	
	// Update is called once per frame
	void Update () {
		//myPosition = Camera.main.gameObject.transform.position;

		//grabValue = Mathf.RoundToInt(hSliderValue * 100);
		left.text = ((beeStack.Count).ToString ()+" bees left");
		//Debug.Log ("ALERT: "+createBeesCalled);
		dope.text = "are you resetting: "+beeStack.Count+ " "+ createBeesCalled;
		if (beeStack.Count==0 && createBeesCalled==true){
			//level completed... go to next level!
			dope.text = "Reset called...";
			reset();
		}

		else if (totalBees == 0) {
			caught.text = "0 bees caught";
			//nextLevel.gameObject.SetActive (true);
		} else {
			caught.text = ((totalBees - beeStack.Count) + " bees fed");
		}
		levelText.text = (keeper.Instance.currentLevel).ToString();
	}

	/*
	public void ResetScene() {
		ARKitWorldTrackingSessionConfiguration sessionConfig = new ARKitWorldTrackingSessionConfiguration ( UnityARAlignment.UnityARAlignmentGravity, UnityARPlaneDetection.Horizontal);
		UnityARSessionNativeInterface.GetARSessionNativeInterface().RunWithConfigAndOptions(sessionConfig, UnityARSessionRunOption.ARSessionRunOptionRemoveExistingAnchors | UnityARSessionRunOption.ARSessionRunOptionResetTracking);
	}

*/
	public void ResetScene() {
		// config is the ARKitWorldTrackingSessionConfiguration I used when the app started
		/*
		UnityARSessionNativeInterface.GetARSessionNativeInterface().RunWithConfigAndOptions(config, UnityARSessionRunOption.ARSessionRunOptionRemoveExistingAnchors | UnityARSessionRunOption.ARSessionRunOptionResetTracking);
*/
}
	public void loadScene(){
		//
		SceneManager.LoadScene("Garden");
	}


	//createBees
	public void placement(int boop){
		
		for ( int i = 0; i < mover.GetComponent<BallMover> ().flowerTaps.Length; i++)
		{
			mover.GetComponent<BallMover> ().flowerTaps[i] = 0;
			//Debug.Log ("reset flowerTap" + mover.GetComponent<BallMover> ().flowerTaps [i]);
		}

		createBeesCalled = true;
		totalBees = boop;

		hives = GameObject.FindGameObjectsWithTag ("island");

		if (hives.Length == 0) {
			//float centerX = 0;
			//float centerZ = 0;
			//Debug.Log("are there hives?"+hives.Length);
		} else {
			centerX = hives [0].gameObject.transform.position.x;
			centerZ = hives [0].gameObject.transform.position.z;
			//Debug.Log("are there hives?"+hives.Length);
		}

		for (int i =0; i<boop; i++){
			//
			float posx = Random.Range (centerX-terrainWidth, centerX + terrainWidth);
			float posz = Random.Range (centerZ-terrainLength, centerZ + terrainLength);
			GameObject newObject = (GameObject)Instantiate (objectToPlace, new Vector3 (posx, .1f, posz), Quaternion.identity);

			GameObject parental = (GameObject)Instantiate (parentHolder,new Vector3 (posx, .1f, posz), Quaternion.identity);
			//Debug.Log("PUSH!");
			newObject.transform.SetParent(parental.transform);
			newObject.transform.RotateAround (newObject.transform.position, new Vector3 (0, 1, 0), Random.Range (0, 360));
			parental.tag = "beeParent";
			newObject.tag = "bee";
			beeStack.Push (newObject);

		}
		//mover.GetComponent<BallMover> ().flowers = new GameObject[0];
		//System.Array.Clear(mover.GetComponent<BallMover> ().flowers,0, mover.GetComponent<BallMover> ().flowers.Length);

		//Debug.Log("All bees created!");

	}

	public void setLevel(){
		//
		localLevelPosition = creator.GetComponent<BallMaker2> ().levelPosition;
		currentLevel = keeper.Instance.currentLevel;
		alert.gameObject.SetActive (false);
		setLevels ();

	}

	void setLevels(){
		//destroy all islands, bees, flowers and reset which flowers are in the field, and which bees are in the stack
		//Debug.Log ("LEVEL" + keeper.Instance.currentLevel.ToString());
		DestroyGameObjectsWithTag ("island");
		DestroyGameObjectsWithTag ("beeParent");
		DestroyGameObjectsWithTag ("flower");
		beeStack.Clear ();

		//create flowers, bees and island
		levelator (localLevelPosition, keeper.Instance.lvlFlowers[currentLevel], keeper.Instance.lvlBees[currentLevel]);

		Debug.Log ("level set and total bees: " + totalBees);
		Debug.Log ("beestack count: " + beeStack.Count);

	}

	public void levelator(Vector3 pos, int totalFlowers, int totalBees){
		//set flower to be placed
		GameObject currentFlower = keeper.Instance.currentFlower;

		//set max taps
		maxTaps = totalBees/totalFlowers;
		maxer.text = "Max Taps: " + maxTaps;
		Debug.Log ("max taps: "+maxTaps);

		//set island location
		Vector3 flowerPos= pos+ new Vector3(0,-0.1f,0);

		//create island
		creator.GetComponent<BallMaker2> ().CreateBall(pos, creator.GetComponent<BallMaker2> ().island);

		//set first flower location
		flowerPos = flowerPos - new Vector3 (.1f, -.24f, -.1f);

		//create total flowers in array
		for (int i = 0; i < totalFlowers / 3; i++) {
			for (int p = 0; p < 3; p++) {
				creator.GetComponent<BallMaker2> ().CreateBall (flowerPos, currentFlower);
				flowerLocations [p+i] = flowerPos;
				//Debug.Log ("flowerposition index "+flowerPos);
				flowerPos = flowerPos + creator.GetComponent<BallMaker2> ().diff;
			}
			flowerPos = pos- new Vector3 (.1f,-.14f, .12f*(i));
		}

		Debug.Log ("total bees to be created: " + totalBees);
		setMode (false);

		//add bees
		placement (totalBees);
	} 

	public void reset(){
		//
		Debug.Log("lvl: "+keeper.Instance.currentLevel);
		//ResetScene ();

		if(keeper.Instance.currentLevel==10){
			//
			Debug.Log ("MOD: "+ keeper.Instance.currentLevel % 4);
			SceneManager.LoadScene("Reward");
		}
		else if (keeper.Instance.currentLevel%3==0){
			//
			SceneManager.LoadScene("Reward");
		}
		else if (keeper.Instance.currentLevel==9){
			//
			SceneManager.LoadScene("Garden");
		}
		else {
			//Debug.Log ("MOD: "+ keeper.Instance.currentLevel % 4);
			//keeper.Instance.nextLevel();
			SceneManager.LoadScene ("youGotEm");
		}
	}

	public void setMode(bool enable){
		//
		creator.SetActive (enable);
		mover.SetActive (!enable);
	}

	public static void DestroyGameObjectsWithTag(string tag)
	{
		GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(tag);
		foreach (GameObject target in gameObjects) {
			GameObject.Destroy(target);
		}
	}
		
	public void goTo(){
		//
		//SceneManager.LoadScene("youGotEm");
		keeper.Instance.GetComponent<AudioSource> ().Play ();
		keeper.Instance.nextLevel();
		setLevel();
		Handheld.Vibrate ();

	}
    public void pullOutDrawer()
    {
        if (drawerInOut==false)
        {
            iTween.MoveTo(shovel, iTween.Hash("position", new Vector3(shovel.transform.position.x + 100, shovel.transform.position.y, shovel.transform.position.z), "time", 3.0f, "oncomplete", "setPosAndSpeed", "oncompletetarget", gameObject));
        }
        else
        {
            iTween.MoveTo(shovel, iTween.Hash("position", new Vector3(shovel.transform.position.x - 100, shovel.transform.position.y, shovel.transform.position.z), "time", 3.0f, "oncomplete", "setPosAndSpeed", "oncompletetarget", gameObject));
        }
        drawerInOut = !drawerInOut;
    }

}
