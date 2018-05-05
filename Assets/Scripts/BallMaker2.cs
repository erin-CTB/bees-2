using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.iOS;
using UnityEngine.UI;

public class BallMaker2 : MonoBehaviour {

	public GameObject flower1;
	public GameObject island;
	//public float createHeight;
	public GameObject beeCreator;
	public Vector3 diff;
	public int maxTaps;
	public int totalFlowers;
	public Vector3 levelPosition;
	public GameObject mover;
	private int currentFlower;
	public 	GameObject[] tempArray;
	//public GameObject DND;
	public Text notifier;
	private float islandHeight;



	// Use this for initialization
	void Start () {
		diff = new Vector3(.1f,0,0);
		islandHeight = -.3f;
		levelPosition = new Vector3 (0, islandHeight, 0);
		totalFlowers = 6;
	}

	public void CreateBall(Vector3 atPosition, GameObject dingdong)
	{
		GameObject ballGO = Instantiate (dingdong, atPosition, Quaternion.identity);
		if (dingdong == flower1) {
			
			ballGO.tag = "flower";
			ballGO.transform.RotateAround (ballGO.transform.position, new Vector3 (0, 1, 0), Random.Range (0, 360));
			//Debug.Log (atPosition.x + " " + atPosition.y + " " + atPosition.z);

		}
		else if (dingdong == island){
			//
			ballGO.tag = "island";
		}

	}

	// Update is called once per frame
	void Update () {

		if (Input.touchCount > 0 )
		{
			var touch = Input.GetTouch(0);
			if (touch.phase == TouchPhase.Began)
			{
				var screenPosition = Camera.main.ScreenToViewportPoint(touch.position);
				ARPoint point = new ARPoint {
					x = screenPosition.x,
					y = screenPosition.y
				};
						
				List<ARHitTestResult> hitResults = UnityARSessionNativeInterface.GetARSessionNativeInterface ().HitTest (point, 
					ARHitTestResultType.ARHitTestResultTypeExistingPlaneUsingExtent);
				
				if (hitResults.Count > 0) {
					foreach (var hitResult in hitResults) {
						Vector3 position = UnityARMatrixOps.GetPosition (hitResult.worldTransform);
						//add createheight to the y below
						Vector3 placeLevel = new Vector3 (position.x, islandHeight, position.z);
						levelPosition = placeLevel;
						beeCreator.GetComponent<ModeSwitcher2> ().setLevel ();
						break;
					}

				}
			}
		}
	}
}

