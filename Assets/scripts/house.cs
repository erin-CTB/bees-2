using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class house : MonoBehaviour {
	private Animator anim;
	public bool clicked;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		clicked = false;
	}
	
	// Update is called once per frame
	void Update () {
		//anim.SetBool ("play", false);
	}
}
