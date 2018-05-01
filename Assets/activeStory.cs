using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class activeStory : MonoBehaviour {
	public Sprite p1;
	public Sprite p2;
	public Sprite p3;
	public Sprite p4;

	public GameObject holder;

	public Sprite[] pages;
	public Sprite[] wallPages;

    public Sprite[] windBlowsDog;
    public Sprite[] windBlowsCat;

	public int currentPage;

	public int slideShowSet;

    public Text instructions;
    private int instructionCounter = 0;

    public GameObject rightButton;
    public GameObject storyButton;
	//0 - intro, 1 - wall falls,




	// Use this for initialization
	void Start () {
		currentPage = 0;
		pages = new Sprite[] {p1, p2, p3, p4};
		slideShowSet = 0;
        Debug.Log("card set: "+GlobalControl.Instance.cardSetUsed);
        if (GlobalControl.Instance.cardSetUsed==0){
            wallPages = windBlowsCat;
        }
        else if (GlobalControl.Instance.cardSetUsed==1){
            wallPages = windBlowsDog;
        }
	}

	// Update is called once per frame
	void Update () {

	}

    public void next()
    {
        //
        Debug.Log("currentPage: " + currentPage);
        if (holder.activeSelf) //if the story screen is active
        {
            this.GetComponentInParent<Main>().touchEnabled = false;
            if (currentPage < 3)
            {
                currentPage++;
                if (slideShowSet == 0)
                {
                    holder.GetComponent<Image>().sprite = pages[currentPage];

                }
                else if (slideShowSet == 1)
                {
                    //
                    holder.GetComponent<Image>().sprite = wallPages[currentPage];
                }
            }
            else
            {
                //SceneManager.LoadScene ("catDog-Tutorial");
                //GlobalControl.Instance.tutorial = true;
                slideShowSet = 0;
                currentPage = 0;
                holder.SetActive(false);
                storyButton.SetActive(false);
                if (SceneManager.GetActiveScene().name=="catDog-Active"){
                    this.GetComponentInParent<Main>().touchEnabled = true;
                }
                else if (SceneManager.GetActiveScene().name=="catDog-Passive"){
                    
                }

                //StartCoroutine(waitPlease());
                //rightButton.SetActive(true);
                //this.GetComponent<Main>().resetScene();
            }
        }
        else { //if story screen is hidden

            if (instructionCounter < 3)
            {
                instructions.text = this.GetComponent<Main>().instructionsCat[instructionCounter];
                instructionCounter++;
            }
            else
            {
                instructions.text = this.GetComponent<Main>().instructionsCat[3];
                rightButton.SetActive(false);
                this.GetComponentInParent<Main>().touchEnabled = true;
                //StartCoroutine(waitPlease());
                //storyButton.SetActive(true);
                //rightButton.SetActive(false);
            }
           

        }

	}
	public void last(){
		//
		if (currentPage>0){
			currentPage--;
			holder.GetComponent<Image>().sprite = pages[currentPage];
		}

	}
	public void goToTutorial(){
		SceneManager.LoadScene ("catDog-Tutorial");
		GlobalControl.Instance.tutorial = true;
	}

    IEnumerator waitPlease() //wait then allow touch
    {
        float i = 0;
        while (i <= 1)
        {
            i += 1f;
            //Debug.Log ("wait...");
            yield return new WaitForSeconds(1);
        }
        this.GetComponentInParent<Main>().touchEnabled = true;
        this.GetComponentInParent<mainPassive>().touchEnabled = true;
    }
}
