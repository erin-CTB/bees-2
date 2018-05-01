using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour {
	//public GameObject house;
	public GameObject card;
	public GameObject house;
	public GameObject fence;
	public GameObject selectedFence;
	public GameObject mouse;
	public GameObject cat;
	public GameObject setB1;
	public GameObject setB2;
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
	public bool fencePressed = false;
	private int numOfHouseTaps = 2;
	Animator anim;
	Animator catAnim;
	public int block;
	private GameObject tappedFence;
	private int house1 = 70;
	private int house2;
	private int fenceIndex;
	public string userID;
	private int animalSet;
	public Text instructions;
	public AudioSource[] clips;

    public GameObject rightButton;
    public GameObject storyButton;


	private GameObject houseParent;
	private GameObject animalParent;
	private GameObject fenceParent;

	public string[] instructionsCat;

	public Image imageHolder;

    public int[] boundaries;

    public bool touchEnabled = false;
    private string animalias;




	// Use this for initialization
	void Start () {
		//instructionsCat = new string[5];
        boundaries = new int[] { 2, 4, 6 };
		int rand = Random.Range (0,3);
		switchOver = boundaries [rand];
		Debug.Log ("switchover: " + rand);
        Debug.Log("switcher: " + switchOver);


		houseParent = new GameObject("House Parent");
		animalParent = new GameObject("Animal Parent");
		fenceParent = new GameObject("Fence Parent");
		height = Camera.main.orthographicSize;
		width = Camera.main.orthographicSize * Screen.width / Screen.height;


		houses= new GameObject[10];
		fences = new GameObject[9];
		animals = new GameObject[10];
		cardz = new GameObject[10];

        if (GlobalControl.Instance.activeFirst==1){
			//if active is first
            animalSet = GlobalControl.Instance.cardSetUsed;
            Debug.Log("ANIMALSET: " + animalSet);
            Debug.Log("CARDSETUSED: " + GlobalControl.Instance.cardSetUsed);
            Debug.Log(animalSet);
		}
        else if (GlobalControl.Instance.activeFirst==0){
            animalSet = GlobalControl.Instance.cardSetUsed;
            Debug.Log("CARDSETUSED: " + animalSet);
			//if active is second
          /*
			if (GlobalControl.Instance.cardSetUsed == 0) {
				animalSet = 1;
			} else {
				animalSet = 0;
			}
			*/

            Debug.Log(animalSet);
		}

		generateHouses ();
		generateFences ();
		block = 1;

		clips = new AudioSource[2];
		clips = gameObject.GetComponents<AudioSource> ();
		clips [1].Play ();
		if (GlobalControl.Instance.block == 0) {
			instructions.text = instructionsCat [0];
		} else {
			instructions.text = instructionsCat [3];
		}
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.touchCount > 0 && touchEnabled)
		{
			if (Input.GetTouch (0).phase == TouchPhase.Began) {
				RaycastHit2D hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint ((Input.GetTouch (0).position)), Vector2.zero);
				if (hit.collider != null) {
					boop = hit.collider.gameObject.tag;
					//if you have tapped fewer than 2 houses
					switch(tapCounter){
					case 0:
						//textField.text = "A " + boop + " lives here";
						//confirming if you have tapped a house, increase the tap counter
						if (boop == "cat" || boop == "mouse") {
							tapCounter++;
							hit.collider.gameObject.transform.rotation = Quaternion.Euler (0, -90, 0);

							int tempIndex = System.Array.IndexOf (houses, hit.collider.gameObject);
							if (house1 == 70) {
								house1 = tempIndex;
								//Debug.Log ("House 1  tapped index: " + house1);
							} else {
								house2 = tempIndex;
								//Debug.Log ("House 2  tapped index: " + house2);
							}
							animals [tempIndex].gameObject.transform.rotation = Quaternion.Euler (0, 0, 0);
							catAnim = animals [tempIndex].gameObject.GetComponent<Animator> ();
							catAnim.SetTrigger ("animate");
							//Debug.Log("This house has a "+animals [tempIndex].gameObject.name);
							Debug.Log ("index of house tapped: " + tempIndex);
							instructions.text = instructionsCat[4];
							StartCoroutine (waitPlease (hit.collider.gameObject, animals[tempIndex]));

							//Debug.Log ("tap counter: " + tapCounter);
						}
						break;
					case 1:
						//textField.text = "A " + boop + " lives here";
						//confirming if you have tapped a house, increase the tap counter

						if (boop == "cat" || boop == "mouse") {
							instructions.text = "where should we put the fence?";
							tapCounter++;
							hit.collider.gameObject.transform.rotation = Quaternion.Euler (0, -90, 0);

							int tempIndex = System.Array.IndexOf (houses, hit.collider.gameObject);
							if (house1 == 70) {
								house1 = tempIndex;
								//Debug.Log ("House 1  tapped index: " + house1);
							} else {
								house2 = tempIndex;
								//Debug.Log ("House 2  tapped index: " + house2);
							}
							animals [tempIndex].gameObject.transform.rotation = Quaternion.Euler (0, 0, 0);
							catAnim = animals [tempIndex].gameObject.GetComponent<Animator> ();
							catAnim.SetTrigger ("animate");
							//Debug.Log("This house has a "+animals [tempIndex].gameObject.name);
							Debug.Log ("index of house tapped: " + tempIndex);
				
								//
								instructions.text = instructionsCat[5];
				
							StartCoroutine (waitPlease (hit.collider.gameObject, animals[tempIndex]));
							//Debug.Log ("tap counter: " + tapCounter);
						}
						break;
					case 2:
						
						if (boop == "fence" && fencePressed==false) {
							Debug.Log("tappedFence");
							//instructions.text = "The wind blows...";
							//textField.text = "YOU CHOSE A FENCE!";
							tappedFence = hit.collider.gameObject;

							Vector3 tempFence = tappedFence.gameObject.transform.position;
							tappedFence.gameObject.SetActive(false);
							GameObject blueFence = Instantiate (selectedFence, tempFence, Quaternion.identity) as GameObject;
							blueFence.gameObject.SetActive(true);

							fenceIndex = System.Array.IndexOf (fences, hit.collider.gameObject);
							//Debug.Log ("Fence tapped index: " + fenceIndex);
							fencePressed = true;
							clips [1].Stop ();
							clips [0].Play ();
							this.GetComponent<activeStory> ().slideShowSet = 1;
                                touchEnabled = false;
                                storyButton.SetActive(true);
                                rightButton.SetActive(false);
                                //this.GetComponent<activeStory>().holder.SetActive(true);
                            //StartCoroutine( wait (2));
							//StartCoroutine (wait (.4f));



						}
						break;
					default:
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
		this.GetComponent<activeStory> ().holder.SetActive (true);
		//holder.SetActive (true);

	}
    
	IEnumerator waitPlease(GameObject tappedh, GameObject tappeda){
		float i = 0;
		while (i <= 1) {
			i += 0.4f;
			//Debug.Log ("wait...");
			yield return new WaitForSeconds (1);
		}
		tappedh.gameObject.transform.rotation = Quaternion.Euler (0, 0, 0);
		tappeda.transform.rotation = Quaternion.Euler (0, -90, 0);
		//shakeHouse (house1,0);

		Debug.Log ("rotated back");
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
	void generateHouses (){
		float startingPosition = -width+(width*2)/20;
		//float scaler = -.002f;
		for (int i = 0; i < 10; i++) {
			GameObject tempCard = Instantiate (card, new Vector3 (startingPosition, 0,3), Quaternion.identity) as GameObject;
			GameObject temp = Instantiate (house, new Vector3 (0,0,0), Quaternion.identity) as GameObject;
			var boxCollider1 = temp.GetComponent<BoxCollider2D> ();
			boxCollider1.size = new Vector2(1.423137f,2.302651f);
			boxCollider1.offset = new Vector2(-0.01040119f, -0.003331721f);
			GameObject tempParent = Instantiate (houseParent, new Vector3 (startingPosition, 0,2), Quaternion.identity) as GameObject;
			temp.transform.SetParent (tempParent.transform);
			//scaler -= .002f;
			//temp.transform.localScale -= new Vector3(scaler, scaler, 0);
			cardz[i] = tempCard;
			houses [i] = temp;
			if (i <= switchOver) {
				GameObject temp2;
				temp.tag = "mouse";
				switch(animalSet){
				//
				case 0: 
					temp2 = Instantiate (mouse, new Vector3 (startingPosition, 0, 1), Quaternion.identity) as GameObject;
					//Debug.Log ("set mc");
					break;
				case 1:
					temp2 = Instantiate (setB1, new Vector3 (startingPosition, 0, 1), Quaternion.identity) as GameObject;
					//Debug.Log ("set b");
					break;
				default:
					temp2 = Instantiate (mouse, new Vector3 (startingPosition, 0, 1), Quaternion.identity) as GameObject;
					break;
				}

				temp2.GetComponent<BoxCollider2D>().gameObject.transform.rotation = Quaternion.Euler(0,-90, 0);
				animals [i] = temp2;
				//GameObject temp3 = Instantiate(mouseClue, new Vector3 (startingPosition+((width*2)/40), -.5f,1), Quaternion.identity) as GameObject;
				//clues [i] = temp3;

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
				//GameObject temp3 = Instantiate(catClue, new Vector3 (startingPosition+((width*2)/40), -.5f,1), Quaternion.identity) as GameObject;
				//clues [i] = temp3;
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
        if (animalSet == 0)
        {
            animalias = "cats";
        }
        else
        {
            animalias = "dogs";
        }

		//save current block data to new string and save to CSV
		string[] rowData = new string[14];
        rowData[0] = GlobalControl.Instance.startTime;
        rowData[1] = "end time";
        rowData[2] = GlobalControl.Instance.userIDNumber;
        rowData [3] = GlobalControl.Instance.assessorIDNumber;
        rowData [4] = GlobalControl.Instance.schoolIDNumber;
        rowData [5] = animalias+" "+animalSet;
		rowData [6] = "active";
        rowData [7] = (switchOver + 1).ToString();//animalSet.ToString();
        rowData [8] = block.ToString();
        rowData [9] = (house1 + 1).ToString();
        rowData [10] = (house2 + 1).ToString();
        rowData [11] = (fenceIndex + 1).ToString();
        rowData[12] = "";
        rowData[13] = "";

		GlobalControl.Instance.addRowData (rowData);

	}
	public void resetScene(){
        //printInfo ();
        rightButton.SetActive(false);
		if (block < 5) {
			//reset switchover
			//switchOver = Random.Range (1, 9);
			//reset tap counter and whether a fence has been pressed
			tapCounter = 0;

			//reset fence appearance
			if (tappedFence != null) {
				/*
				float r = tappedFence.GetComponent<Renderer> ().material.color.r;
				float b = tappedFence.GetComponent<Renderer> ().material.color.b;
				float g = tappedFence.GetComponent<Renderer> ().material.color.g;
				tappedFence.GetComponent<Renderer> ().material.color = new Color (r, b, g, 0.4f);
				*/
				//Vector3 tempFence = tappedFence.gameObject.transform.position;
				GameObject[] selector = GameObject.FindGameObjectsWithTag("selected");
				Destroy(selector[0]) ;
				//GameObject blueFence = Instantiate (selectedFence, tempFence, Quaternion.identity) as GameObject;
				//blueFence.gameObject.SetActive(true);

			} else {
			}
			//animalSet = Random.Range (0, 3);
			fencePressed = false;
			fenceIndex = 70;
			house1 = 70;



			for (int i = 0; i < 10; i++) {
				//Debug.Log ("InOut Value"+houses [i].GetComponent<Animator>().GetInteger("inOut"));
				Destroy(houses[i].transform.parent.gameObject);
				Destroy (houses [i]);
				Destroy (animals [i]);
				Destroy (cardz [i]);
			}
			for (int i = 0; i < 9; i++) {
				//Debug.Log ("InOut Value"+houses [i].GetComponent<Animator>().GetInteger("inOut"));
				Destroy (fences [i]);
			}
		


			//generate new fences and houses and animals
			generateFences ();
			generateHouses ();
			clips[1].Play();
			instructions.text = instructionsCat [3];
			block++;
			//return "Next Block" ;
		}
		else {
			//cricketContainer.Stop();
			//roosterContainer.Play ();
			clips[1].Stop();
			//clips [0].Play ();
		}

	}
    public void toAnimalStory()
    {
        SceneManager.LoadScene("DogStory");
    }
}
