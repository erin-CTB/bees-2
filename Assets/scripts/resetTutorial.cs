using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class resetTutorial : MonoBehaviour {

	private Button myButton;
	public GameObject controller;
	private int block;
    public GameObject rooster;
    public bool roosterSeen;
    public Image header;
    public GameObject successScreen;
	// Use this for initialization
	void Start () {
		myButton = GetComponent<Button>();
		myButton.onClick.AddListener(onClick);
        roosterSeen=false;
        GlobalControl.Instance.startTime = DateTime.Now.ToString("MM-dd-yyyy_HH:mm:ss");
	}

	// Update is called once per frame
	void Update () {

	}

	void onClick() {
		int temp = controller.GetComponent<mainTutorial> ().round;
		Debug.Log ("round: "+temp);
		if (temp < 3) {
            if (!rooster.activeSelf && roosterSeen==false){
                rooster.SetActive(true);
            }
            else if (rooster.activeSelf && roosterSeen==false){
                rooster.SetActive(false);
                controller.GetComponent<mainTutorial>().header.sprite = controller.GetComponent<mainTutorial>().intro2;
                roosterSeen = true;
            }
            
            else if (temp<3 && roosterSeen==true){
                    roosterSeen=false;
                    successScreen.SetActive(true);
                    //controller.GetComponent<mainTutorial> ().resetScene ();
                    
                    //myButton.GetComponentInChildren<Text> ().text = "Wind blows...";
                    //GameObject[] selector = GameObject.FindGameObjectsWithTag("selected");
                    //Destroy(selector[0]) ;
                

		      }
            
            }
        else {
			if (controller.GetComponent<mainTutorial> ().match == true) {
				//
				GlobalControl.Instance.round++;
				Debug.Log ("Round increased: " + GlobalControl.Instance.round);
                //set first animal set
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
                GameObject[] selector = GameObject.FindGameObjectsWithTag("selected");
                Destroy(selector[0]) ;
			} else {
				SceneManager.LoadScene ("Home");
			}

		}
	}
}
