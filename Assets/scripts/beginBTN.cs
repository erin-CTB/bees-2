using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class beginBTN : MonoBehaviour {
	public InputField userIDField;
    public InputField assessorIDField;
    public InputField schoolIDField;
    public GameObject splash;
	//public GameObject global;
	//public Button yourButton;
	// Use this for initialization
	void Start () {
		//Button btn = yourButton.GetComponent<Button>();
		//btn.onClick.AddListener(BeginBlock);

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void BeginBlock(){
		GlobalControl.Instance.tutorial = false;
		Debug.Log (userIDField.text);
		if (userIDField.text != "" && assessorIDField.text != "" && schoolIDField.text != "") {
			Debug.Log ("random scene: " + GlobalControl.Instance.activeFirst);
			GlobalControl.Instance.userIDNumber = userIDField.text.ToString ();
            GlobalControl.Instance.assessorIDNumber = assessorIDField.text.ToString ();
			Debug.Log(assessorIDField.text.ToString ());

            GlobalControl.Instance.schoolIDNumber = schoolIDField.text.ToString ();
			Debug.Log(schoolIDField.text.ToString ());
            
			//global.GetComponent<GlobalControl> ().userIDNumber = userIDField.text.ToString();
			SceneManager.LoadScene("TorG");
		} else {
		}
	}

	public void runTutorial(){
		//
		//GlobalControl.Instance.tutorial = false;
		Debug.Log (userIDField.text);
		if (userIDField.text != "" && assessorIDField.text != "" && schoolIDField.text != "") {
			Debug.Log ("random scene: " + GlobalControl.Instance.activeFirst);
			GlobalControl.Instance.userIDNumber = userIDField.text.ToString ();
			GlobalControl.Instance.assessorIDNumber = assessorIDField.text.ToString ();
			Debug.Log(assessorIDField.text.ToString ());

			GlobalControl.Instance.schoolIDNumber = schoolIDField.text.ToString ();
			Debug.Log(schoolIDField.text.ToString ());
			SceneManager.LoadScene ("IntroStory");
			GlobalControl.Instance.tutorial = true;

			//global.GetComponent<GlobalControl> ().userIDNumber = userIDField.text.ToString();
			//SceneManager.LoadScene("TorG");
		} else {
		}


	}

    public void hideSplash(){
        splash.SetActive(false);
    }
}
