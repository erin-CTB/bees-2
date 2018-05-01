using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class catController : MonoBehaviour {
	private Animator anim;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		//anim ["play"].wrapMode = WrapMode.Once;
		gameObject.transform.localScale = new Vector3(10, 10, 10);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
