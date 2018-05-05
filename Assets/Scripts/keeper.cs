using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keeper : MonoBehaviour {
	public static keeper Instance;
	public int currentLevel;
	public GameObject currentFlower;
	public int[] lvlBees;
	public int[] lvlFlowers;
	public GameObject controller;
	public GameObject flower1;
	public GameObject flower2;
	public GameObject flower3;
	public Vector3[] twoBees;
	public Vector3[] fiveBees;
	public Vector3[] tenBees;

	public int[] gardenBeesTwo;
    public int[] gardenBeesFive;
    public int[] gardenBeesTen;
    public int[] gardenBeesMixed;
    public int[] flowerTypesGarden;

	public int currentGardenLevel;

	public string currentFl;

	public int numTotalTaps;

	// Use this for initialization
	void Start () {
		numTotalTaps = 0;
		currentLevel = 0;
		lvlFlowers = new int[] { 3, 3, 6, 9, 3, 6, 9, 3, 6, 9 };
		lvlBees = new int[] { 6, 6, 12, 18, 15, 30, 45, 30, 60, 90 };

		//bee locations for the 2, 5, and 10 bee flowers
		twoBees = new Vector3[2];
		twoBees [0] = new Vector3 (0,.05f,0);
		twoBees [1] = new Vector3 (-.03f,.03f,0);
		fiveBees = new Vector3[5];
		fiveBees [0] = new Vector3 (0,.05f,0);
		fiveBees [1] = new Vector3 (-.02f,.05f,0);
		fiveBees [2] = new Vector3 (-.02f,.05f,-.02f);
		fiveBees [3] = new Vector3 (0,.05f,-.02f);
		fiveBees [4] = new Vector3 (-.02f,.05f,.02f);
		tenBees = new Vector3[10];
		tenBees [0] = new Vector3 (0, .02f, 0);
		tenBees [1] = new Vector3 (0, .03f, 0);
		tenBees [2] = new Vector3 (0, .04f, 0);
		tenBees [3] = new Vector3 (0, .05f, 0);
		tenBees [4] = new Vector3 (0, .06f, 0);
		tenBees [5] = new Vector3 (.02f, .02f, 0);
		tenBees [6] = new Vector3 (.02f, .03f, 0);
		tenBees [7] = new Vector3 (.02f, .04f, 0);
		tenBees [8] = new Vector3 (.02f, .05f, 0);
		tenBees [9] = new Vector3 (.02f, .06f, 0);

		currentGardenLevel = 0;

        flowerTypesGarden = new int[] { 2, 2, 2, 2, 2, 5, 5, 5, 5, 10, 10, 10, 10, 0, 0, 0, 0 };

		gardenBeesTwo = new int[] { 4, 8, 10, 20 };
        gardenBeesFive = new int[] { 5, 10, 15, 20 };
        gardenBeesTen = new int[] { 10, 20, 30, 40 };
        gardenBeesMixed = new int[] { 33, 20, 30, 40 };
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void Awake ()   
	{

		//Debug.Log ("random scene: " + activeFirst);
		if (Instance == null)
		{
			DontDestroyOnLoad(gameObject);
			Instance = this;
		}
		else if (Instance != this)
		{
			Destroy (gameObject);
		}
	}

	public void nextLevel(){
		//
		currentLevel++;
		switch(currentLevel){
		//
		case 0: break;
		case 1:
		case 2:
		case 3:
			currentFlower = flower1;
			currentFl = "You collected the 2-bee flower!";
			Debug.Log ("new flower 1 set");
			break;
		case 4:
		case 5:
		case 6:
			currentFlower = flower2;
			currentFl = "You collected the 5-bee flower!";
			Debug.Log ("new flower 2 set");
			break;
		case 7:
		case 8:
		case 9:
			currentFlower = flower3;
			currentFl = "You collected the 10-bee flower!";
			Debug.Log ("new flower 3 set");
			break;
		case 10:
		case 11:
		case 12:
		case 13:
		case 14:
		case 15:
			
		default:
			break;
		}
	}
}
