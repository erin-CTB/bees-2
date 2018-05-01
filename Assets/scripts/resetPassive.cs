using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class resetPassive : MonoBehaviour {

	private Button myButton;
	public GameObject controller;
	private int block;
	// Use this for initialization
	void Start () {
		//myButton = GetComponent<Button>();
		//myButton.onClick.AddListener(onClick);
	}

	// Update is called once per frame
	void Update () {

	}

	public void onClick() {
		//
		controller.GetComponent<mainPassive> ().printInfo ();
		int temp = controller.GetComponent<mainPassive> ().block;
		Debug.Log ("Block: " + temp);
		if (temp < 4) {
			controller.GetComponent<mainPassive> ().resetScene ();
			//myButton.GetComponentInChildren<Text> ().text = "Wind Blows...";
		}
		else {
			//
			//myButton.GetComponentInChildren<Text>().text = "Finish";
			GlobalControl.Instance.passive = true;
			if (GlobalControl.Instance.active == true && GlobalControl.Instance.round==2) {
				GlobalControl.Instance.saveData (controller);
				SceneManager.LoadScene ("End");
			} else if (GlobalControl.Instance.round==1){
				GlobalControl.Instance.round++;
				Debug.Log ("Round increased: " + GlobalControl.Instance.round);
				SceneManager.LoadScene ("catDog-Active");
			}
		}
		//Debug.Log (temp);
		//
	}

    public void GO(){
        controller.GetComponent<mainPassive>().printInfo();
        int temp = controller.GetComponent<mainPassive>().block;
        Debug.Log("Block: " + temp);
        if (temp < 4)
        {
            controller.GetComponent<mainPassive>().resetScene();
            //myButton.GetComponentInChildren<Text>().text = "Wind Blows...";
        }
        else
        {
            //
            //myButton.GetComponentInChildren<Text>().text = "Finish";
            GlobalControl.Instance.passive = true;
            if (GlobalControl.Instance.active == true && GlobalControl.Instance.round == 2)
            {
                GlobalControl.Instance.setEndTime();
                GlobalControl.Instance.saveData(controller);
                GlobalControl.Instance.active = false;
                GlobalControl.Instance.passive = false;
                SceneManager.LoadScene("End");
            }
            else if (GlobalControl.Instance.round == 1)
            {
                GlobalControl.Instance.round++;
                Debug.Log("Round increased: " + GlobalControl.Instance.round);
                //SceneManager.LoadScene("catDog-Active");
                //set cards to next set, then load story
                if (GlobalControl.Instance.cardSetUsed == 0)
                {
                    GlobalControl.Instance.cardSetUsed = 1;
                }
                else
                {
                    GlobalControl.Instance.cardSetUsed= 0;
                }
                GlobalControl.Instance.currentGame = 0; //set next game to active
                SceneManager.LoadScene("DogStory");
            }
            else {
                GlobalControl.Instance.setEndTime(); 
                GlobalControl.Instance.saveData(controller);
                GlobalControl.Instance.active = false;
                GlobalControl.Instance.passive = false;
                SceneManager.LoadScene("End");
            }
        }
        //Debug.Log (temp);
        //
    }
}
