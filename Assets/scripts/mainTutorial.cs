using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class mainTutorial : MonoBehaviour {
	//public GameObject house;
    public GameObject card;
	public GameObject house; 
	public GameObject fence;
    public GameObject selectedFence;
	public GameObject mouse;
	public GameObject cat;
	public GameObject mouseClue;
	public GameObject catClue;
	public GameObject setB1;
	public GameObject setB2;
	public GameObject B1Clue;
	public GameObject B2Clue;
	public GameObject setC1;
	public GameObject setC2;
	public GameObject C1Clue;
	public GameObject C2Clue;

    public Sprite one;
    public Sprite two;
    public Sprite three;

	private int switchOver;
	private float width;
	private float height;
	public GameObject[] houses;
    public GameObject[] cardz;
	public GameObject[] fences;
	public GameObject[] animals;
	public GameObject[] clues;
	private string boop;
	public Text textField;
	private int tapCounter=0;
	private bool fencePressed = false;
	private int numOfHouseTaps = 2;
	Animator anim;
	Animator catAnim;
	public int block;
	private GameObject tappedFence;
	private int house1 = 70;
	private int house2;
	private int fenceIndex;
	public string userID;
	public AudioSource[] clips;
	public int round;

	private int animalSet;

	private GameObject houseParent;
	private GameObject animalParent;
	private GameObject fenceParent;
   

	private int[] cards;
	private List<int> cardList;

	public bool match;
	public Text instructions;
	private string predator;
	private string prey;
	public Image preyImage;
    
    public Image header;
    public Sprite intro2;
    public Sprite header1;
    
    public Text congrats;
    public GameObject successScreen;

	public GameObject roosterScreen;
    public GameObject circleButton;

    public GameObject successImageHolder;

    public GameObject storyHolder;

    public Sprite[] bunnyStory;
    public Sprite[] snailStory;
    public Sprite[] butterflyStory;
    //public GameObject storyHolder;

    public Sprite[] currentStory;
    public int currentPage = 0;

    public int[] switchoverPreset;
    public int[] animalPreset; //bunny, butterfly, snail

	// Use this for initialization
	void Start () {
        switchoverPreset = new int[] { 3, 1, 7 };
        animalPreset = new int[] { 0, 1, 2 };
        currentStory = bunnyStory;
        storyHolder.GetComponent<Image>().sprite = currentStory[0];
        storyHolder.SetActive(true);
        switchOver = switchoverPreset[round];
        animalSet = animalPreset[round];
        Debug.Log("animalset: " + animalSet + "switchover: " + switchOver);
        circleButton.SetActive(false);
		round = 0;
		houseParent = new GameObject("House Parent");
        //Rigidbody gameObjectsRigidBody = houseParent.AddComponent<Rigidbody>();
        var boxCollider1 = houseParent.AddComponent<BoxCollider2D>();
        boxCollider1.size = new Vector2(.64f,1.06f);
		animalParent = new GameObject("Animal Parent");
		fenceParent = new GameObject("Fence Parent");
		height = Camera.main.orthographicSize;
		width = Camera.main.orthographicSize * Screen.width / Screen.height;
		//switchOver = Random.Range (2, 7);

		cardList = new List<int>();
		cards = new int[]{0,1,2};

		//animalSet = GetUniqueRandom (true);

		//Debug.Log ("House Location: "+switchOver);
		//Debug.Log ("Screen Width: "+width);
		//Debug.Log ("Screen Height: "+height);
		houses= new GameObject[10];
		fences = new GameObject[9];
		animals = new GameObject[10];
		clues = new GameObject[10];
        cardz = new GameObject[10];
		generateHouses ();
		generateFences ();
		block = 1;

		clips = new AudioSource[2];
		clips = gameObject.GetComponents<AudioSource> ();
		clips [1].Play ();

		match = false;
		switch (animalSet) {
            case 0:
			predator = "fox";
			prey = "The rabbits are safe!";
                successImageHolder.GetComponent<Image>().sprite = three;
			break;
		case 1:
			predator = "frog";
			prey = "The butterflies are safe!";
                successImageHolder.GetComponent<Image>().sprite = one;
			break;
		case 2:
			predator = "bird";
			prey = "The snails are safe!";
                successImageHolder.GetComponent<Image>().sprite = two;
			break;
		default:
			break;
		}
        congrats.text = prey;
		//instructions.text = "There are 10 houses with animals inside them and the clues outside the house show you what animal is inside. The animals are sleeping now but, when the rooster crows, all the animals will wake up and the "+predator+ " will chase the "+prey+". I will tap a fence to separate the animals, then you can tap the houses to see if we separated the animals.";




		//anim = house.GetComponent<Animator> ();

		//anim.enabled = false;


		/* //line renderer
		lineGO = new GameObject("Line");
		lineGO.AddComponent<LineRenderer>();
		lineRenderer = lineGO.GetComponent<LineRenderer>();
		lineRenderer.material = new Material(Shader.Find("Mobile/Particles/Additive"));
		lineRenderer.startColor = c1;
		lineRenderer.endColor = c2;
		lineRenderer.startWidth = 0.2f;
		lineRenderer.endWidth = 0.2f;
		lineRenderer.positionCount = 0;
		*/
	}

	// Update is called once per frame
	void Update () {
		if (Input.touchCount > 0 )
		{
			if (Input.GetTouch (0).phase == TouchPhase.Began) {
				RaycastHit2D hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint ((Input.GetTouch (0).position)), Vector2.zero);
				if (hit.collider != null) {
					boop = hit.collider.gameObject.tag;
                    Debug.Log(boop+" collider tag");
					switch(tapCounter){
					//
					case 0:
						if (boop == "fence" && fencePressed == false) {
							tappedFence = hit.collider.gameObject;
                            Debug.Log("tappedFence");
                            
                            Vector3 tempFence = tappedFence.gameObject.transform.position;
                            tappedFence.gameObject.SetActive(false);
                            GameObject blueFence = Instantiate (selectedFence, tempFence, Quaternion.identity) as GameObject;
                            blueFence.gameObject.SetActive(true);
                            
                            
							fenceIndex = System.Array.IndexOf (fences, hit.collider.gameObject);
							//Debug.Log ("Fence tapped index: " + fenceIndex);
							fencePressed = true;
							if (fenceIndex == switchOver) {
								match = true;
							}
							clips [1].Stop ();
							clips [0].Play ();
							tapCounter++;
							Debug.Log ("tap: " + tapCounter);
                               // header.sprite = intro2;
							//StartCoroutine( wait (2));
                                showRooster();
                            
						}
						break;
					case 1:
						if (boop == "cat" || boop == "mouse") {
                            //hit.collider.gameObject
							int tempIndex = System.Array.IndexOf (houses, hit.collider.gameObject.transform.GetChild (0).gameObject);
                            Debug.Log ("temp index "+tempIndex);
							//catAnim = animals[tempIndex].gameObject.GetComponent<Animator> ();
							//catAnim ["catScale"].wrapMode = WrapMode.Once;
							//catAnim.SetTrigger ("animate");
							animals [tempIndex].gameObject.transform.rotation = Quaternion.Euler (0, 0, 0);
							clues[tempIndex].gameObject.transform.rotation = Quaternion.Euler(0,-90, 0);
							hit.collider.gameObject.transform.rotation = Quaternion.Euler(0,-90, 0);
							tapCounter++;
							Debug.Log ("tap: " + tapCounter);
                                StartCoroutine (waitPlease (hit.collider.gameObject, animals[tempIndex], clues[tempIndex]));

						}
						break;
					case 2:
						if (boop == "cat" || boop == "mouse") {
							int tempIndex = System.Array.IndexOf (houses, hit.collider.gameObject.transform.GetChild (0).gameObject);

							//catAnim = animals[tempIndex].gameObject.GetComponent<Animator> ();
							//catAnim ["catScale"].wrapMode = WrapMode.Once;
							//catAnim.SetTrigger ("animate");
							animals [tempIndex].gameObject.transform.rotation = Quaternion.Euler (0, 0, 0);
							hit.collider.gameObject.transform.rotation = Quaternion.Euler(0,-90, 0);
							clues[tempIndex].gameObject.transform.rotation = Quaternion.Euler(0,-90, 0);
							tapCounter++;
							Debug.Log ("tap: " + tapCounter);
							StartCoroutine (waitPlease (hit.collider.gameObject, animals[tempIndex], clues[tempIndex]));
							//instructions.text = "the wind blows";


						}
						break;
					default:
						if (boop == "cat" || boop == "mouse") {
							int tempIndex = System.Array.IndexOf (houses, hit.collider.gameObject.transform.GetChild (0).gameObject);

							//catAnim = animals[tempIndex].gameObject.GetComponent<Animator> ();
							//catAnim ["catScale"].wrapMode = WrapMode.Once;
							//catAnim.SetTrigger ("animate");
							animals [tempIndex].gameObject.transform.rotation = Quaternion.Euler (0, 0, 0);
							hit.collider.gameObject.transform.rotation = Quaternion.Euler(0,-90, 0);
							clues[tempIndex].gameObject.transform.rotation = Quaternion.Euler(0,-90, 0);
							tapCounter++;
							Debug.Log ("tap: " + tapCounter);
							StartCoroutine (waitPlease (hit.collider.gameObject, animals[tempIndex], clues[tempIndex]));

						}
						break;
					}

				}
			}
		}
	}

	IEnumerator wait(float seconds){
		Debug.Log ("wait called");
		float i = 0;
		while (i <= seconds) {
			i += 1;
			//Debug.Log ("wait...");
			yield return new WaitForSeconds (1);
		}
		roosterScreen.SetActive (true);
        circleButton.SetActive(true);
		//holder.SetActive (true);

	}

    public void showRooster(){
        
        //roosterScreen.SetActive(true);
        circleButton.SetActive(true);
    }

	IEnumerator waitPlease(GameObject tappedh, GameObject tappeda, GameObject tappedc){
		float i = 0;
		if (round==1 || round==2) {
			while (i <= 1) {
				i += 0.4f;
				Debug.Log ("wait...");
				yield return new WaitForSeconds (1);
			}
		
			tappedh.gameObject.transform.rotation = Quaternion.Euler (0, 0, 0);
			tappedc.gameObject.transform.rotation = Quaternion.Euler (0, 0, 0);
			tappeda.transform.rotation = Quaternion.Euler (0, -90, 0);

			Debug.Log ("rotated back");
		}
		Debug.Log ("ROUND: " + round);
		//shakeHouse (house1,0);

	}
	int GetUniqueRandom(bool reloadEmptyList){
		if(cardList.Count == 0 ){
			if(reloadEmptyList){
				cardList.AddRange(cards); 
				Debug.Log ("added cardlist to list");
			}
			else{
				return -1; // here is up to you 
				Debug.Log("no cards to add");
			}
		}
		int rand = Random.Range(0, cardList.Count);
		int value = cardList[rand];
		cardList.RemoveAt(rand);
		return value;
	}

	void scaleHouses(){
		float startingPosition = -width+(width*2)/20;
		float scaler=1;
		//Debug.Log ("starting position" + startingPosition);

		for (int i=0; i<10; i++){
			houses [i].transform.parent.gameObject.transform.localScale = new Vector3 (scaler, scaler, scaler);
			houses [i].transform.position = new Vector3 (startingPosition, 0, 2);
			scaler += .1f;
			//Debug.Log ("SCALER " + scaler);
			startingPosition += (width * 2) / 10;
			//Debug.Log ("move " + startingPosition);
		}
	}
	void generateHouses (){
		float startingPosition = -width+(width*2)/20;
		//float scaler = -.002f;
		for (int i = 0; i < 10; i++) {
			GameObject tempCard = Instantiate (card, new Vector3 (startingPosition, 0,3), Quaternion.identity) as GameObject;
            GameObject temp = Instantiate (house, new Vector3 (0,0,0), Quaternion.identity) as GameObject;
			GameObject tempParent = Instantiate (houseParent, new Vector3 (startingPosition, 0,-1), Quaternion.identity) as GameObject;
			temp.transform.SetParent (tempParent.transform);
			//scaler -= .002f;
			//temp.transform.localScale -= new Vector3(scaler, scaler, 0);
            cardz[i] = tempCard;
			houses [i] = temp;
			if (i <= switchOver) {
				GameObject temp2;
				GameObject temp3;
				tempParent.tag = "mouse";
				switch(animalSet){
				//
					case 0: 
						temp2 = Instantiate (mouse, new Vector3 (startingPosition, 0, 1), Quaternion.identity) as GameObject;
                        temp3 = Instantiate(catClue, new Vector3 (startingPosition, 1.3f,1), Quaternion.identity) as GameObject;
						break;
				case 1:
					temp2 = Instantiate (setC1, new Vector3 (startingPosition, 0, 1), Quaternion.identity) as GameObject;
					temp3 = Instantiate(C1Clue, new Vector3 (startingPosition, 1.3f,1), Quaternion.identity) as GameObject;
						break;
					case 2:
						temp2 = Instantiate (setB1, new Vector3 (startingPosition, 0, 1), Quaternion.identity) as GameObject;
					temp3 = Instantiate(B1Clue, new Vector3 (startingPosition, 1.3f,1), Quaternion.identity) as GameObject;
						break;
				default:
					temp2 = Instantiate (mouse, new Vector3 (startingPosition, 0, 1), Quaternion.identity) as GameObject;
					temp3 = Instantiate(mouseClue, new Vector3 (startingPosition, 1.3f,1), Quaternion.identity) as GameObject;
					break;
				}

				temp2.GetComponent<BoxCollider2D>().gameObject.transform.rotation = Quaternion.Euler(0,-90, 0);
				animals [i] = temp2;
				clues [i] = temp3;

			}
			else{
				tempParent.tag = "cat";
				GameObject temp2;
				GameObject temp3;
				switch(animalSet){
				//
				case 0: 
					temp2 = Instantiate (cat, new Vector3 (startingPosition, 0,1), Quaternion.identity) as GameObject;
                        temp3 = Instantiate(mouseClue, new Vector3 (startingPosition, 1.3f,1), Quaternion.identity) as GameObject;
					break;
				case 1:
					temp2 = Instantiate (setC2, new Vector3 (startingPosition, 0, 1), Quaternion.identity) as GameObject;
					temp3 = Instantiate(C2Clue, new Vector3 (startingPosition, 1.3f,1), Quaternion.identity) as GameObject;
					break;
				case 2:
					temp2 = Instantiate (setB2, new Vector3 (startingPosition, 0, 1), Quaternion.identity) as GameObject;
					temp3 = Instantiate(B2Clue, new Vector3 (startingPosition, 1.3f,1), Quaternion.identity) as GameObject;
					break;
				default:
					temp2 = Instantiate (cat, new Vector3 (startingPosition, 0, 1), Quaternion.identity) as GameObject;
					temp3 = Instantiate(catClue, new Vector3 (startingPosition, 1.3f,1), Quaternion.identity) as GameObject;
					break;
				}

				temp2.GetComponent<BoxCollider2D>().transform.rotation = Quaternion.Euler(0,-90, 0);
				animals [i] = temp2;
				clues [i] = temp3;
			}
			startingPosition += (width*2)/10;
		}
		scaleHouses ();
	}

	void generateFences(){
		float startingPosition = -width + (width*2)/10;
		for (int i = 0; i < 9; i++) {
			GameObject temp = Instantiate (fence, new Vector3 (startingPosition, 0,1), Quaternion.identity) as GameObject;
			fences [i] = temp;
			temp.transform.SetParent (fenceParent.transform);
			startingPosition += (width*2)/10;
		}
	}

	public void printInfo(){
		//save current block data to new string and save to CSV
		string[] rowData = new string[7];
		rowData [0] = GlobalControl.Instance.userIDNumber;
		rowData [1] = block.ToString();
		rowData [2] = (house1+1).ToString();
		rowData [3] = (house2+1).ToString();
		rowData [4] = (fenceIndex+1).ToString();
		rowData [5] = (switchOver+1).ToString();
		rowData [6] = "active";

		GlobalControl.Instance.addRowData (rowData);

	}
	public void resetScene(){
        printInfoTutorial();
        header.sprite = header1;
        Debug.Log("round: " + round);
        if (round==0){
            currentStory = butterflyStory;
            storyHolder.SetActive(true);
            storyHolder.GetComponent<Image>().sprite = currentStory[0];
        }
        else if (round==1){
            currentStory = snailStory;
            storyHolder.SetActive(true);
            storyHolder.GetComponent<Image>().sprite = currentStory[0];
        }

        
        GameObject[] selector = GameObject.FindGameObjectsWithTag("selected");
        if (selector.Length !=0){
            Destroy(selector[0]);
        }


        successScreen.SetActive(false);
		//animalSet = Random.Range (0, 3);
		if (tappedFence != null) {
			float r = tappedFence.GetComponent<Renderer> ().material.color.r;
			float b = tappedFence.GetComponent<Renderer> ().material.color.b;
			float g = tappedFence.GetComponent<Renderer> ().material.color.g;
			tappedFence.GetComponent<Renderer> ().material.color = new Color (r, b, g, 0.4f);
		} else {
		}
		for (int i = 0; i < 10; i++) {
			//Debug.Log ("InOut Value"+houses [i].GetComponent<Animator>().GetInteger("inOut"));
			Destroy(houses[i].transform.parent.gameObject);
            Destroy (houses [i]);
            Destroy (cardz [i]);
			Destroy (animals [i]);
			Destroy (clues [i]);
		}
		for (int i = 0; i < 9; i++) {
			//Debug.Log ("InOut Value"+houses [i].GetComponent<Animator>().GetInteger("inOut"));
			Destroy (fences [i]);
		}

        circleButton.SetActive(false);

		//animals[i]
		fencePressed=false;
        //switchOver = Random.Range (2, 7);
        //animalSet = GetUniqueRandom (true);


		if (round < 2) {
            switchOver = switchoverPreset[round + 1];
            animalSet = animalPreset[round + 1];
			match = false;
			round++;

            Debug.Log("animalset: " + animalSet + "switchover: " + switchOver);

			tapCounter = 0;

			//generate new fences and houses and animals
			generateFences ();
			generateHouses ();

			switch (animalSet) {
			case 0:
				predator = "fox";
				prey = "The rabbits are safe!";
                    successImageHolder.GetComponent<Image>().sprite = three;
				break;
			case 1:
				predator = "frog";
				prey = "The butterflies are safe!";
                    successImageHolder.GetComponent<Image>().sprite = one;
				break;
			case 2:
				predator = "bird";
				prey = "The snails are safe!";
                    successImageHolder.GetComponent<Image>().sprite = two;
				break;
			default:
				break;
			}
			congrats.text=prey;
			//preyImage
			if (round == 0 || round == 1) {
				//instructions.text = "";
			}
			else{
				//
				//instructions.text = "Now can you tap where the fence should be?";
                tapCounter = 0;
			}
			clips[1].Play();

		} else {
			round++;
			if (match == true) {
				//

				GlobalControl.Instance.round++;
				Debug.Log ("Round increased: " + GlobalControl.Instance.round);
                int animalSet = UnityEngine.Random.Range(0, 2);
                GlobalControl.Instance.cardSetUsed = animalSet;
                SceneManager.LoadScene("DogStory");
                /*
				if (GlobalControl.Instance.activeFirst == 0) {
					SceneManager.LoadScene ("catDog-Active");
					GlobalControl.Instance.firstScene = 1;
				}
				else if (GlobalControl.Instance.activeFirst==1){
					//
					SceneManager.LoadScene ("catDog-Passive");
					GlobalControl.Instance.firstScene = 2;
				}
				*/
				GameObject[] bobo = GameObject.FindGameObjectsWithTag("selected");
				Destroy(bobo[0]) ;
			} else {
				SceneManager.LoadScene ("Home");
			}
		}



	}

	public void back(){
		SceneManager.LoadScene ("Home");
	}

    public void nextSlide(){
        if (currentPage<2){
            currentPage++;
            storyHolder.GetComponent<Image>().sprite = currentStory[currentPage];
        }
        else{
            currentPage = 0;
            storyHolder.SetActive(false);
        }
    }
    public void printInfoTutorial()
    {

        //save current block data to new string and save to CSV
        string[] rowData = new string[14];
        rowData[0] = GlobalControl.Instance.startTime;
        rowData[1] = "end time";
        rowData[2] = GlobalControl.Instance.userIDNumber;
        rowData[3] = GlobalControl.Instance.assessorIDNumber;
        rowData[4] = GlobalControl.Instance.schoolIDNumber;
        rowData[5] = "";
        rowData[6] = "";
        rowData[7] = "";
        rowData[8] = "";
        rowData[9] = "";
        rowData[10] = "";
        rowData[11] = "";
        rowData[12] = "";
        rowData[13] = ""+match;

        GlobalControl.Instance.addRowData(rowData);

    }
}
