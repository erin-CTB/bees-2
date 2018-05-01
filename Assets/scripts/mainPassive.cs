using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mainPassive : MonoBehaviour {
	//public GameObject house;
	public GameObject card;
	public GameObject house;
	public GameObject fence;
    public GameObject selectedFence;
	public GameObject mouse;
	public GameObject cat;
	public GameObject setB1;
	public GameObject setB2;
	public GameObject setC1;
	public GameObject setC2;
	private int switchOver;
	private float width;
	private float height;
	public GameObject[] houses;
	public GameObject[] fences;
	public GameObject[] animals;
	public GameObject[] cardz;
	private string boop;
	//public Text textField;
	public int tapCounter=0;
	private bool fencePressed = false;
	private int numOfHouseTaps = 2;
	Animator anim;
	Animator catAnim;
	public int block;
	private GameObject tappedFence;
	private int house1 = 0;
	private int house2=0;
	private int fenceIndex;
	private GameObject houseParent;
	private GameObject animalParent;
	private GameObject fenceParent;
	private int animalSet;
	public Text instructions;
	public AudioSource[] clips;
	public int[] boundaries = new int[]{3,5,7};

    public Sprite[] wallPages;
    public Sprite[] dogPages;
    public Sprite[] catPages;

    public string[] textInstructions;
    public GameObject holder;
    //public Text instructions;
    private int instructionCounter = 0;

    public GameObject rightButton;
    public bool touchEnabled = false;
    private string animalias;

    public GameObject topPosition;
    public GameObject bottomPosition;
    public Sprite roundButton;
    public Sprite sideButton;

	// Use this for initialization
	void Start () {

		int rand = Random.Range (0,3);
		switchOver = boundaries [rand];
		Debug.Log ("switchover: " + rand);
        Debug.Log("switchy: " + switchOver);

		height = Camera.main.orthographicSize;
		width = Camera.main.orthographicSize * Screen.width / Screen.height;
		//switchOver = Random.Range (2, 7);
		houses = new GameObject[10];
		fences = new GameObject[9];
		animals = new GameObject[10];
		cardz = new GameObject[10];
        if (GlobalControl.Instance.activeFirst == 1) {
			//if passive is first
			//animalSet = Random.Range (0, 2);
			//GlobalControl.Instance.cardSetUsed = animalSet;
            animalSet = GlobalControl.Instance.cardSetUsed;
            Debug.Log("CARDSETUSED: " + animalSet);
        } else if (GlobalControl.Instance.activeFirst == 0) {
            animalSet = GlobalControl.Instance.cardSetUsed;
            Debug.Log("CARDSETUSED: " + animalSet);
			//if passive is second
            /*
			if (GlobalControl.Instance.cardSetUsed == 0) {
				animalSet = 1;
			} else {
				animalSet = 0;
			}
			*/
            //Debug.Log(animalSet);
		}
        Debug.Log("card set: " + GlobalControl.Instance.cardSetUsed);
        if (GlobalControl.Instance.cardSetUsed == 0)
        {
            wallPages = catPages;
        }
        else if (GlobalControl.Instance.cardSetUsed == 1)
        {
            wallPages = dogPages;
        }

		switch (animalSet) {
		case 0:
			//instructions.text = "Cats live in the big houses and mice live in the small houses. Put a fence between where the cats and mice live before the rooster crows. Sometimes, the animals get thirsty and go outside to get water. Oh look, this animal got thirsty and came out.";
			break;
		case 1:
			//instructions.text = "Dogs live in the big houses and squirrels live in the small houses. Put a fence between where the dogs and squirrels live before the rooster crows";
			break;
		default:
			break;
		}
		houseParent = new GameObject ("House Parent");
		animalParent = new GameObject ("Animal Parent");
		fenceParent = new GameObject ("Fence Parent");
		while (house1 == house2) {
			house1 = Random.Range (0, 10);
			house2 = Random.Range (0, 10);
		}
		generateHouses ();
		generateFences ();
		//shakeHouse (house1, 1);

		clips = new AudioSource[2];
		clips = gameObject.GetComponents<AudioSource> ();
		clips [1].Play ();

		//houseParent.transform.localScale = new Vector3 (10, 10, 10);
		anim = house.GetComponent<Animator> ();
		block = 1;

	}

	// Update is called once per frame
	void Update () {
		
        if (Input.touchCount > 0 && touchEnabled)
		{
			if (Input.GetTouch (0).phase == TouchPhase.Began) {
				RaycastHit2D hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint ((Input.GetTouch (0).position)), Vector2.zero);
				if (hit.collider != null) {
					//boop is the tag of the object that has been hit
					boop = hit.collider.gameObject.tag;
					int tappedHouse = System.Array.IndexOf (houses, hit.collider.gameObject);
					switch(tapCounter){
					//
					case 0:
						//instructions.text = "Oh look, this animal got thirsty and came out!";

						if ((boop == "mouse" || boop == "cat") && (tappedHouse == house1 || tappedHouse == house2)) {
							//Debug.Log ("you tapped a house!");
							hit.collider.gameObject.GetComponent<Animator> ().SetInteger ("inOut", 0);
							hit.collider.gameObject.transform.rotation = Quaternion.Euler (0, -90, 0);
							animals [tappedHouse].gameObject.transform.rotation = Quaternion.Euler (0, 0, 0);
							tapCounter++;
							StartCoroutine (waitPlease (hit.collider.gameObject, animals[tappedHouse]));
							Debug.Log ("tapCounter: " + tapCounter);
                                //next();
							//hit.collider.gameObject.transform.rotation = Quaternion.Euler (0, 0, 0);
							//animals [tappedHouse].gameObject.transform.rotation = Quaternion.Euler (0, -90, 0);
							//yield return new WaitForSeconds(5);
							//WaitForSeconds (5);
							//shakeHouse (house2,1);
						}
						break;
					case 1:
						
						//instructions.text = "where should we put the fence?";
						if ((boop == "mouse" || boop == "cat") && (tappedHouse == house1 || tappedHouse == house2)) {
							//Debug.Log ("you tapped a house!");
							hit.collider.gameObject.GetComponent<Animator> ().SetInteger ("inOut", 0);
							hit.collider.gameObject.transform.rotation = Quaternion.Euler (0, -90, 0);
							animals [tappedHouse].gameObject.transform.rotation = Quaternion.Euler (0, 0, 0);
							StartCoroutine (waitPlease (hit.collider.gameObject, animals[tappedHouse]));
							tapCounter++;
							Debug.Log ("tapCounter: " + tapCounter);
                                next();
						}
						break;
					case 2:
						//instructions.text = "Wind blows...";
						if (boop == "fence" && fencePressed == false) {

							tappedFence = hit.collider.gameObject;
							Vector3 tempFence = tappedFence.gameObject.transform.position;
                            tappedFence.gameObject.SetActive(false);
                            GameObject blueFence = Instantiate(selectedFence, tempFence, Quaternion.identity) as GameObject;
                            blueFence.gameObject.SetActive(true);


							fenceIndex = System.Array.IndexOf (fences, hit.collider.gameObject);
							//Debug.Log ("Fence tapped index: " + fenceIndex);
							Debug.Log ("YOU TAPPED the " + fenceIndex + " FENCE!");
							fencePressed = true;
							Debug.Log ("tapCounter: " + tapCounter);
							clips [1].Stop ();
							clips [0].Play ();
							tapCounter++;
                                rightButton.SetActive(true);
                                //next();

						} 
						break;
					default:
						break;
					}
				}
			}
		}
	}
	IEnumerator waitPlease(GameObject tappedh, GameObject tappeda){
		float i = 0;
		while (i <= 1) {
			i += 0.5f;
			//Debug.Log ("wait...");
			yield return new WaitForSeconds (1);
		}
		tappedh.gameObject.transform.rotation = Quaternion.Euler (0, 0, 0);
		tappeda.transform.rotation = Quaternion.Euler (0, -90, 0);
		shakeHouse (house1,0);
		if (tapCounter == 1) {
			shakeHouse (house2, 1);
            next();
			Debug.Log ("shake second house");
		}
		Debug.Log ("rotated back");
	}
	void shakeHouse(int trip, int onOff){
		houses[trip].GetComponent<Animator> ().SetInteger ("inOut", onOff);
		Debug.Log ("HOUSES SHAKING");
		Debug.Log ("HOUSES "+ house1+" AND "+ house2);

	}
	void scaleHouses(){
		float startingPosition = -width+(width*2)/20;
		float scaler=20;
		//Debug.Log ("starting position" + startingPosition);

		for (int i=0; i<10; i++){
			houses [i].transform.parent.gameObject.transform.localScale = new Vector3 (scaler, scaler, scaler);
			houses [i].transform.position = new Vector3 (startingPosition, 0, 2);
			scaler += 4f;
			//Debug.Log ("SCALER " + scaler);
			startingPosition += (width * 2) / 10;
			//Debug.Log ("move " + startingPosition);
		}
	}
	//generates houses, mice, and cats
	void generateHouses (){
		float startingPosition = -width+(width*2)/20;

		for (int i = 0; i < 10; i++) {
			GameObject tempCard = Instantiate (card, new Vector3 (startingPosition, 0,3), Quaternion.identity) as GameObject;
			GameObject temp = Instantiate (house, new Vector3 (0,0,0), Quaternion.identity) as GameObject;
			var boxCollider1 = temp.GetComponent<BoxCollider2D> ();
			boxCollider1.size = new Vector2(1.423137f,2.302651f);
			boxCollider1.offset = new Vector2(-0.01040119f, -0.003331721f);
			GameObject tempParent = Instantiate (houseParent, new Vector3 (startingPosition, 0,2), Quaternion.identity) as GameObject;
			temp.transform.SetParent (tempParent.transform);
			//tempParent.transform.localScale = new Vector3(scaler, scaler, 1);
			cardz[i] = tempCard;
			houses [i] = temp;
			if (i <= switchOver) {
				GameObject temp2;
				temp.tag = "mouse";
				switch(animalSet){
				//
				case 0: 
					temp2 = Instantiate (mouse, new Vector3 (startingPosition, 0, 1), Quaternion.identity) as GameObject;
					break;
				case 1:
					temp2 = Instantiate (setB1, new Vector3 (startingPosition, 0, 1), Quaternion.identity) as GameObject;
					break;
				default:
					temp2 = Instantiate (mouse, new Vector3 (startingPosition, 0, 1), Quaternion.identity) as GameObject;
					break;
				}

				temp2.GetComponent<BoxCollider2D>().gameObject.transform.rotation = Quaternion.Euler(0,-90, 0);
				animals [i] = temp2;
			}
			else{
				temp.tag = "cat";
				GameObject temp2;
				switch(animalSet){
				//
				case 0: 
					temp2 = Instantiate (cat, new Vector3 (startingPosition, 0,1), Quaternion.identity) as GameObject;
					break;
				case 1:
					temp2 = Instantiate (setB2, new Vector3 (startingPosition, 0, 1), Quaternion.identity) as GameObject;
					break;
				default:
					temp2 = Instantiate (cat, new Vector3 (startingPosition, 0, 1), Quaternion.identity) as GameObject;
					break;
				}

				temp2.GetComponent<BoxCollider2D>().transform.rotation = Quaternion.Euler(0,-90, 0);
				animals [i] = temp2;
				temp2.transform.SetParent (animalParent.transform);
				//GameObject temp3 = Instantiate(catClue, new Vector3 (startingPosition+((width*2)/40), -.5f,1), Quaternion.identity) as GameObject;
				//clues [i] = temp3;
			}
				startingPosition += (width * 2) / 10;

		}
		scaleHouses ();
	}

	void generateFences(){
		//float houseWidth = house.GetComponent<Renderer> ().bounds.size.x;
		float startingPosition = -width + (width*2)/10;
		for (int i = 0; i < 9; i++) {
			GameObject temp = Instantiate (fence, new Vector3 (startingPosition, 0,1), Quaternion.identity) as GameObject;
			temp.transform.SetParent (fenceParent.transform);
			fences [i] = temp;
			startingPosition += (width*2)/10;
		}
	}
	public void printInfo(){
        if (animalSet==0){
            animalias = "cats";
        }
        else{
            animalias = "dogs";
        }
		//save current block data to new string and save to CSV
		string[] rowData = new string[14];
        rowData[0] = GlobalControl.Instance.startTime;
        rowData[1] = "end time";
        rowData [2] = GlobalControl.Instance.userIDNumber;
        rowData [3] = GlobalControl.Instance.assessorIDNumber;
        rowData [4] = GlobalControl.Instance.schoolIDNumber;
        rowData [5] = animalias+ " " + animalSet;
		rowData [6] = "passive";
        rowData[7] =  (switchOver + 1).ToString();//animalSet.ToString();
        rowData [8] = block.ToString();
        rowData [9] = (house1 + 1).ToString();
        rowData [10] = (house2 + 1).ToString();
        rowData[11] = (fenceIndex + 1).ToString();
        rowData[12] = "";
        rowData[13] = "";
		GlobalControl.Instance.addRowData (rowData);

	}
	public void resetScene(){
		if (block < 5) {
			//reset houses to be drawn
				house1 = Random.Range (0, 10);
				house2 = Random.Range (0, 10);

			while (house1 == house2) {
				house1 = Random.Range (0, 10);
				house2 = Random.Range (0, 10);
			}
			//reset tap counter and whether a fence has been pressed
			tapCounter = 0;

			//reset fence appearance
            GameObject[] selector = GameObject.FindGameObjectsWithTag("selected");
            Destroy(selector[0]);

			} 
        else {
			}

			fencePressed = false;
			fenceIndex = 70;

			for (int i = 0; i < 10; i++) {
				Destroy(houses[i].transform.parent.gameObject);
				Destroy (houses [i]);
				Destroy (animals [i]);
				Destroy (cardz [i]);
			}
			for (int i = 0; i < 9; i++) {
				Destroy (fences [i]);
			}
            //set instructions
            /*
			switch (animalSet) {
			case 0:
				instructions.text = "Cats live in the big houses and mice live in the small houses. Put a fence between where the cats and mice live before the rooster crows. Sometimes, the animals get thirsty and go outside to get water. Oh look, this animal got thirsty and came out.";
				break;
			case 1:
				instructions.text = "Dogs live in the big houses and squirrels live in the small houses. Put a fence between where the dogs and squirrels live before the rooster crows";
				break;
			case 2:
				instructions.text = "Birds live in the big houses and worms live in the small houses. Put a fence between where the birds and worms live before the rooster crows";
				break;
			default:
				break;
			}
*/
            instructions.text = " Oh look! An animal got thirsty and wants to come out! Go ahead and touch the house with one finger to see what lives inside.";

			//generate new fences and houses and animals
			generateFences ();
			generateHouses ();
			clips[1].Play();
			shakeHouse (house1, 1);
            next();
			block++;
    }

	

    public void next(){
        Debug.Log(instructionCounter);
        if (holder.gameObject.activeSelf && instructionCounter<3){
            instructionCounter++;   
            holder.GetComponent<Image>().sprite = wallPages[instructionCounter];
            touchEnabled = false;

        }
        else if (holder.gameObject.activeSelf && instructionCounter==3){
            instructionCounter = 2;
            instructions.text = textInstructions[instructionCounter];
            Debug.Log("i'm in");
            holder.SetActive(false);
            touchEnabled = true;
            //rightButton.transform.position=new Vector3(348.3f, 0, 0);
            rightButton.transform.parent = bottomPosition.transform;
            rightButton.GetComponent<Image>().sprite = roundButton;
            rightButton.transform.localPosition = new Vector3(0, 0, 0);
            //this.GetComponent<resetPassive>().GO();
            this.GetComponentInParent<resetPassive>().GO();
            //touchEnabled = false;

            //instructionCounter = 0;
        }
        else if (!holder.gameObject.activeSelf){
            if (instructionCounter<5){
                instructions.text = textInstructions[instructionCounter + 1];
                instructionCounter++;
                if(instructionCounter==3){
                    shakeHouse(house1, 1);
                    rightButton.SetActive(false);
                }
                if (instructionCounter>2){
                    touchEnabled = true;
                }
            }
            else if  (instructionCounter==5 && block<4){
                holder.SetActive(true);
                holder.GetComponent<Image>().sprite = wallPages[0];
                //rightButton.transform.position = new Vector3(348.3f, -165, 0);
                rightButton.transform.parent = topPosition.transform;
                rightButton.GetComponent<Image>().sprite = sideButton;
                rightButton.transform.localPosition= new Vector3(0, 0, 0);
                instructionCounter = 0;
            }
            else if (block==4){
                this.GetComponentInParent<resetPassive>().GO();
            }
            else{
                this.GetComponentInParent<resetPassive>().GO();
            }
        }


    }
}
