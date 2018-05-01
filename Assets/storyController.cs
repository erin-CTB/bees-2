using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class storyController : MonoBehaviour {
    public Sprite p1;
    public Sprite p2;
    public Sprite p3;
    public Sprite p4;
    public Sprite p5;
    public Sprite p6;
    public Sprite p7;
    public Sprite p8;
    public Sprite p9;
    public Sprite p10;
    public GameObject holder;
    
    public Sprite[] pages;
    public int currentPage;
    
    public GameObject imReady;
    public GameObject nextBTN;
    public GameObject lastBTN;

	public GameObject tappers;
    
    public GameObject rightArrowBTN;
    
	// Use this for initialization
	void Start () {
        currentPage = 0;
        pages = new Sprite[] {p1, p2, p3, p4, p5, p6, p7};
        
		
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
        if (currentPage<6){
            currentPage++;
            holder.GetComponent<Image>().sprite = pages[currentPage];
        }
        else {
            SceneManager.LoadScene ("catDog-Tutorial");
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
}
