using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fence : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		
		//this.GetComponent<MeshRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
		float r = gameObject.GetComponent<Renderer> ().material.color.r;
		float b = gameObject.GetComponent<Renderer> ().material.color.b;
		float g = gameObject.GetComponent<Renderer> ().material.color.g;
		gameObject.GetComponent<Renderer> ().material.color = new Color(r,b,g,0.4f);
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void selectedFence() {
		float r = gameObject.GetComponent<Renderer> ().material.color.r;
		float b = gameObject.GetComponent<Renderer> ().material.color.b;
		float g = gameObject.GetComponent<Renderer> ().material.color.g;
		gameObject.GetComponent<Renderer> ().material.color = new Color(r,b,g,1f);
	}
}
