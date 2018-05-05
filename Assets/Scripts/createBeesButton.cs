using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class createBeesButton : MonoBehaviour {

	public Button yourButton;
	public InputField textField;
	public GameObject beeCreator;
	public Text createdBees;
	public Text beesCaught;
	public Text stopGrabbing;

	void Start()
	{
		Button btn = yourButton.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
	}

	void TaskOnClick()
	{
		//Debug.Log("You have clicked the button!");
		string numOfBees = textField.text;
		int temp = int.Parse (numOfBees);
		//Debug.Log ("you will create " + temp + " bees");
		temp = temp - 1;
		beeCreator.GetComponent<ModeSwitcher2> ().placement (temp);
		createdBees.text = (temp+1).ToString () + " bees created";

		//beesCaught.text = "0 bees caught";
		stopGrabbing.enabled = false;
		//textField.text

	}
}
