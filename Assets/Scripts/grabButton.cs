using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class grabButton : MonoBehaviour {

	public Button yourButton;
	public Slider slider;
	public GameObject beeCreator;
	public Text label;
	public Text beesCaught;
	public Text beesLeft;
	//private float temp;
	public Text stopGrabbing;

	void Start()
	{
		Button btn = yourButton.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
		//beesLeft.text = "0 bees created";
		//beesCaught.text = "0 bees caught";
		stopGrabbing.enabled = false;
	}
	void Update(){
		//temp = slider.value;
		//label.text = temp.ToString();
		//beesLeft.text = beeCreator.GetComponent<ModeSwitcher2> ().beeStack.Count + " left";

	}

	void TaskOnClick()
	{
		//Debug.Log("You have clicked the button!");
		//int numToGrab = Mathf.RoundToInt (temp);
		//Debug.Log ("you will group " + numToGrab + " bees");
		//beeCreator.GetComponent<ModeSwitcher2> ().lineUp (numToGrab);
		/*
		beesCaught.text = (beeCreator.GetComponent<ModeSwitcher2> ().totalBees - beeCreator.GetComponent<ModeSwitcher2> ().beeStack.Count)+" bees caught";
		int holder = (beeCreator.GetComponent<ModeSwitcher2> ().totalBees+1) - beeCreator.GetComponent<ModeSwitcher2> ().currentBee;
		//beesLeft.text = holder.ToString ()+" bees left";
		if (holder == 0) {
			stopGrabbing.text = "You caught all the bees!";
			stopGrabbing.enabled = true;
		}
		else if ((beeCreator.GetComponent<ModeSwitcher2> ().totalBees+1) - beeCreator.GetComponent<ModeSwitcher2> ().currentBee <Mathf.RoundToInt (temp)) {
			stopGrabbing.text = "Try a smaller number";
			stopGrabbing.enabled = true;
		}else {
			stopGrabbing.enabled = false;
		}
		*/

	}
}
