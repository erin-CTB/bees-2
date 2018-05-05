using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class introScript : MonoBehaviour {
	Camera m_MainCamera;
	public int currentPanel;
	public RawImage panel1;
	public RawImage panel2;
	public RawImage panel3;
	public RawImage panel4;
	public RawImage panel5;
	public RawImage panel6;
	public RawImage panel7;
	public RawImage panel8;
	public RawImage panel9;

	// Use this for initialization
	void Start () {
		currentPanel = 1;
		//panelHolder = new RawImage[9];
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.L))
		{
			//
			//m_MainCamera.transform.position(
		}
	}

	public void next(){
		//
		Debug.Log("clicked");
		m_MainCamera= Camera.main;

		switch(currentPanel){
		//

		case 1:
			panel1.gameObject.SetActive (false);
			panel2.gameObject.SetActive (true);
			//panel1.enabled = false;
			//panel2.enabled = true;
			break;
		case 2:
			panel2.gameObject.SetActive (false);
			panel3.gameObject.SetActive (true);
			//panel2.enabled = false;
			//panel3.enabled = true;
			break;
		case 3:
			panel3.gameObject.SetActive (false);
			panel4.gameObject.SetActive (true);
			//panel3.enabled = false;
			//panel4.enabled = true;
			break;
		case 4:
			panel4.gameObject.SetActive (false);
			panel5.gameObject.SetActive (true);
			break;
		case 5:
			panel5.gameObject.SetActive (false);
			panel6.gameObject.SetActive (true);
			break;
		case 6:
			panel6.gameObject.SetActive (false);
			panel7.gameObject.SetActive (true);
			break;
		case 7:
			panel7.gameObject.SetActive (false);
			panel9.gameObject.SetActive (true);
			break;
		case 8:
			SceneManager.LoadScene ("Beez");
			//SceneManager.LoadScene ("Garden");
			break;
		default:
			break;
		}
		currentPanel++;
	}
}
