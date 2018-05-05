using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class fedThem : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void next(){
		Debug.Log (keeper.Instance.currentGardenLevel);
		if (keeper.Instance.currentGardenLevel <4) {
			//SceneManager.LoadScene ("Garden");
			Debug.Log("garden please");
			SceneManager.LoadScene("Garden");
		} 
		else if (keeper.Instance.currentGardenLevel==4){
			//
			Debug.Log("beez please");
			SceneManager.LoadScene("Beez");
		}
		else {
			Debug.Log ("ERROR: go next please isn't working"+keeper.Instance.currentGardenLevel);
		}
	}

}
