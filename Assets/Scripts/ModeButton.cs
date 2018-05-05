using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModeButton : MonoBehaviour {
	public GameObject controller;
	public Button yourButton;
	private int appMode = 0;

	public GameObject ballMake;
	public GameObject ballMove;

	// Use this for initialization
	void Start () {
		Button btn = yourButton.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void TaskOnClick(){
		//Debug.Log ("clicked!");
		string modeString = appMode == 0 ? "BREAK" : "MAKE";
		appMode = (appMode + 1) % 2;
		bool temp = (appMode == 0);
		//yourButton.GetComponent<Text>().text = modeString;
		yourButton.gameObject.GetComponentInChildren<Text>().text = modeString;
		Debug.Log("app mode: "+ modeString+" "+temp);
		//controller.GetComponent<ModeSwitcher2> ().placement (10);
		EnableBallCreation (temp);

	}

	public void EnableBallCreation(bool enable)
	{
		//Debug.Log ("SUCCESS: enable ball creation called");
		ballMake.SetActive (enable);
		ballMove.SetActive (!enable);

	}
}
