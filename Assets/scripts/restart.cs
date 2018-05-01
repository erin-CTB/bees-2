using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class restart : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void restartPlease(){
		//
		//GlobalControl.Instance.rowData.Clear();
		//DestroyObject(
		//Destroy (GameObject.Find("GlobalObject"));
        //GlobalControl.Instance.setEndTime();
		GlobalControl.Instance.round = 0;
        GlobalControl.Instance.cardSetUsed = 3;
        GlobalControl.Instance.activeFirst = Random.Range(0, 2);
        GlobalControl.Instance.currentGame = 3;
		SceneManager.LoadScene ("Home");

	}
}
