using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.iOS;
using UnityEngine.UI;
using DG.Tweening;

public class BallMover2 : MonoBehaviour {

	public GameObject collBallPrefab;
	private GameObject collBallGO;
	public Text label;
	public GameObject controller;
	public GameObject BallMaker;
	private int maxTaps;
	//private int totalFlowers;
	public GameObject[] flowers;
	public int[] flowerTaps;
	private float[] height;
	//private Vector3[] flowerLOC;

	// Use this for initialization
	void Start () {
		keeper.Instance.numTotalTaps = 0;
		flowers =  GameObject.FindGameObjectsWithTag ("flower");
		flowerTaps = new int[] { 0, 0, 0, 0, 0, 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 };
		height = new float[100];
		height [0] = .05f;
		for (int i=1; i<100;i++){
			//
			height[i] = height[i-1]+.01f;
		}

	}
	
	// Update is called once per frame
	void Update () {
		maxTaps = controller.GetComponent<ModeSwitcher2> ().maxTaps;


		if ((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Began))
		{
			Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
			RaycastHit raycastHit;

			if (Physics.Raycast(raycast, out raycastHit))
			{
				var hitPoint = raycastHit.point;
				Vector3 temp = raycastHit.transform.position;
				GameObject selectedObject = raycastHit.collider.gameObject;

				if (raycastHit.collider.gameObject.CompareTag("flower")) {
					
					int flowerIndex = System.Array.IndexOf (flowers, selectedObject);
					//label.text ="flower: "+flowerIndex + " taps "+(flowerTaps[flowerIndex]) + " height: "+height[(flowerTaps[flowerIndex])] ;
					//Debug.Log ("FLOWER TAPS: "+flowerTaps [flowerIndex]);
					keeper.Instance.numTotalTaps++;

					if (flowerTaps [flowerIndex] < maxTaps) {
						//get a bee 
						GameObject topBee = controller.GetComponent<ModeSwitcher2> ().beeStack.Pop ();
						GameObject parental = topBee.transform.parent.gameObject;
						topBee.transform.parent = parental.transform;

						//scale bee down, parent bee to flower, set position to flower
						parental.transform.localScale = new Vector3 (.4f, .4f, .4f);
						parental.transform.SetParent (selectedObject.transform);
						switch(maxTaps){
						//
						case 2:
							parental.transform.localPosition = keeper.Instance.twoBees [flowerTaps [flowerIndex]];
							break;
						case 5:
							parental.transform.localPosition = keeper.Instance.fiveBees [flowerTaps [flowerIndex]];
							break;
						case 10:
							parental.transform.localPosition = keeper.Instance.tenBees [flowerTaps [flowerIndex]];
							break;
						default:
							parental.transform.localPosition = new Vector3 (0, height[(flowerTaps[flowerIndex])], 0);
							break;
						}
						//parental.transform.localPosition = new Vector3 (0, height[(flowerTaps[flowerIndex])], 0);
						Destroy (topBee.GetComponent<Rigidbody> ());
						flowerTaps [flowerIndex]++;
						//DND.Instance.getComponent<AudioSource> ().Play ();
						//keeper.Instance.GetComponent<AudioSource> ().Play ();
						controller.GetComponent<AudioSource>().Play();
						Handheld.Vibrate ();


						//Rigidbody beeRigid = parental.GetComponent<Rigidbody>();
						//beeRigid.angularVelocity = Vector3.zero;



					} 
					else if (raycastHit.collider.gameObject.CompareTag("shovel")){
						//
						Debug.Log("shovel tapped");
						if (selectedObject.transform.position.x == 31) {
							selectedObject.transform.DOMove (new Vector3 (-118, -173, 0), 2);
						} else {
							selectedObject.transform.DOMove (new Vector3 (31, -173, 0), 2);
						}
					}
					else {
						this.GetComponent<AudioSource> ().Play ();
						//label.text ="FLOWER MAXED OUT TAPPED";

					}
				} 
			}
		}
	}
}
