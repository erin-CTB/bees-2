using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TorB : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void clickBegin(){
		if (GlobalControl.Instance.activeFirst == 0) {
			SceneManager.LoadScene ("catDog-Active");
		} else if (GlobalControl.Instance.activeFirst == 1) {
			SceneManager.LoadScene ("catDog-Passive");
		}
	}

	public void clickTutorial (){
		SceneManager.LoadScene ("catDog-Tutorial");
	}

}
