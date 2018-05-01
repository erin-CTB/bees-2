using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buttonClick : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void clicked(Button clicker){
		//
		Color temp = clicker.GetComponent<Image>().color;
		clicker.GetComponent<Image> ().color = new Color (temp.r, temp.g, temp.b, .5f);
	}
}
