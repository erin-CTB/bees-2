using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class reset : MonoBehaviour {

	private Button myButton;
	public GameObject controller;
	private int block;
	public Image holder;
	public Sprite[] windBlows;

	// Use this for initialization
	void Start () {
		myButton = GetComponent<Button>();
		myButton.onClick.AddListener(onClick);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void onClick() {
		//
		controller.GetComponent<Main> ().printInfo ();
		int temp = controller.GetComponent<Main> ().block;
		Debug.Log ("Block: " + temp);
		Debug.Log ("Round: " + GlobalControl.Instance.round);

		if (temp < 4) {
			holder.gameObject.SetActive (true);
			holder.GetComponent<Image> ().sprite = windBlows [0];
            controller.GetComponent<Main>().touchEnabled = false;
            controller.GetComponent<Main>().storyButton.SetActive(false);
            controller.GetComponent<Main>().rightButton.SetActive(false);
			controller.GetComponent<Main> ().resetScene ();
			//myButton.GetComponentInChildren<Text> ().text = "Wind blows "+(temp+1).ToString ()+" time(s)";
			//myButton.GetComponentInChildren<Text> ().text = "Wind blows...";
		}
		else {
			
			GlobalControl.Instance.active = true;
			if (GlobalControl.Instance.passive == true && GlobalControl.Instance.round==2) {
                GlobalControl.Instance.setEndTime(); 
				GlobalControl.Instance.saveData (controller);
                GlobalControl.Instance.active = false;
                GlobalControl.Instance.passive = false;
				SceneManager.LoadScene ("End");
			} else if (GlobalControl.Instance.round==1){
				GlobalControl.Instance.round++;
                Debug.Log("Round increased: " + GlobalControl.Instance.round);
                //SceneManager.LoadScene ("catDog-Passive");
                //set cards to next set, then load story
                if (GlobalControl.Instance.cardSetUsed == 0)
                {
                    GlobalControl.Instance.cardSetUsed = 1;
                }
                else
                {
                    GlobalControl.Instance.cardSetUsed = 0;
                }
                GlobalControl.Instance.currentGame = 1; //set next game to passive
                //holder.GetComponent<Image>().sprite = greatJob;
                //nextBTN.SetActive(true);
                toAnimalStory();
			}

		}
		//Debug.Log (temp);
		//
	}
    public void toAnimalStory(){
        SceneManager.LoadScene("DogStory");
    }
}
