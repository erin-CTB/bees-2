using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class storyControllerDog : MonoBehaviour {
    public Sprite p1;
    public Sprite p2;
    public Sprite p3;
    public Sprite p4;
    public Sprite c1;
    public Sprite c2;
    public Sprite c3;
    public Sprite c4;
    //public Sprite p9;
    //public Sprite p10;
    public GameObject holder;
    
    public Sprite[] dogs;
    public Sprite[] cats;
    public Sprite[] pages;
    public int currentPage;
    
    public GameObject imReady;
    public GameObject nextBTN;
    public GameObject lastBTN;

	public GameObject tappers;
    
    public GameObject rightArrowBTN;
    public string nextScene;
    public int animalSet;
    public int condition;
    
	// Use this for initialization
	void Start () {
        currentPage = 0;
        rightArrowBTN.SetActive(true);
        nextBTN.SetActive(false);
        lastBTN.SetActive(false);
        animalSet = GlobalControl.Instance.cardSetUsed;
        Debug.Log("CARDSETUSED: " + animalSet);
        dogs = new Sprite[] {p1, p2, p3, p4,};
        cats = new Sprite[] { c1, c2, c3, c4};
        if (animalSet==0){
            pages = cats;
        }
        else {
            pages = dogs;
        }
        holder.GetComponent<Image>().sprite = pages[currentPage];
	}
	
	// Update is called once per frame
	void Update () {
		if (currentPage==6){
            nextBTN.gameObject.SetActive(false);
            lastBTN.gameObject.SetActive(false);
			tappers.gameObject.SetActive(true);
            imReady.gameObject.SetActive(true);
            Debug.Log("current 6");
        }
        else if (currentPage>6){
			tappers.gameObject.SetActive(false);
            imReady.gameObject.SetActive(false);
            rightArrowBTN.gameObject.SetActive(true);
            Debug.Log("current >6");
        }
        else {
			tappers.gameObject.SetActive(false);
             imReady.gameObject.SetActive(false);
            Debug.Log("else");
        }
	}
    
    public void next(){
        //
        if (currentPage<3){
            currentPage++;
            holder.GetComponent<Image>().sprite = pages[currentPage];
        }
        else {
            //SceneManager.LoadScene ("catDog-Tutorial");
            goToNextScene();
		  GlobalControl.Instance.tutorial = true;
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

    public void goToNextScene(){
        if (GlobalControl.Instance.currentGame==3){
            if (GlobalControl.Instance.activeFirst == 0)
            {
                SceneManager.LoadScene("catDog-Active");
                GlobalControl.Instance.firstScene = 1;
            }
            else if (GlobalControl.Instance.activeFirst == 1)
            {
                //
                SceneManager.LoadScene("catDog-Passive");
                GlobalControl.Instance.firstScene = 2;
            }
        }
        else if (GlobalControl.Instance.currentGame == 0){
            SceneManager.LoadScene("catDog-Active");
        }
        else if (GlobalControl.Instance.currentGame == 1){
            SceneManager.LoadScene("catDog-Passive");
        }
    }
}
