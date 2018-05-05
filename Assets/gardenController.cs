using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.XR.iOS;

public class gardenController : MonoBehaviour {
	public RawImage popUp;
	public InputField numberOfFlowers;
	public int gardenFlowers;
	public int gardenBees;
	public int level;
    public GameObject shovel;

	public GameObject maker;
	public GameObject mover;

    public GameObject arSessionHolder;
    private bool drawerInOut = true;
	// Use this for initialization
	void Start () {
		level = keeper.Instance.currentGardenLevel;
		gardenBees = keeper.Instance.gardenBeesTwo [level];

        //ARKitWorldTrackingSessionConfiguration sessionConfig = new ARKitWorldTrackingSessionConfiguration(UnityARAlignment.UnityARAlignmentCamera, UnityARPlaneDetection.Horizontal);
        //UnityARSessionNativeInterface.GetARSessionNativeInterface().RunWithConfigAndOptions(sessionConfig, UnityARSessionRunOption.ARSessionRunOptionRemoveExistingAnchors | UnityARSessionRunOption.ARSessionRunOptionResetTracking);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void resetTracker()
    {
        //arSessionHolder.d
    }
	public void plusButton(){
		//
		Debug.Log("plus button clicked");
		popUp.gameObject.SetActive(true);
	}

	public void createHowMany(){
		//

		int temp;
		int.TryParse( numberOfFlowers.text, out temp);
		gardenFlowers = gardenFlowers + temp;
		Debug.Log("Total Flowers: "+gardenFlowers);
		popUp.gameObject.SetActive (false);
	}

	public void goNextPlease(){
		//
		if (keeper.Instance.currentGardenLevel != 4) {
			//SceneManager.LoadScene ("Garden");
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		} 
		else if (keeper.Instance.currentGardenLevel==4){
			//
			SceneManager.LoadScene("Beez");
		}
		else {
			Debug.Log ("ERROR: go next please isn't working"+keeper.Instance.currentGardenLevel);
		}
	}

	public void resetfloScene(){
		//
		keeper.Instance.currentGardenLevel = keeper.Instance.currentGardenLevel-1;
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
	}
	public void goToCollection(){
		//
		SceneManager.LoadScene("Beez");
	}
    public void pullOutDrawer()
    {
        if (drawerInOut == false)
        {
            iTween.MoveTo(shovel, iTween.Hash("position", new Vector3(shovel.transform.position.x + 100, shovel.transform.position.y, shovel.transform.position.z), "time", 3.0f, "oncomplete", "setPosAndSpeed", "oncompletetarget", gameObject));
        }
        else
        {
            iTween.MoveTo(shovel, iTween.Hash("position", new Vector3(shovel.transform.position.x - 100, shovel.transform.position.y, shovel.transform.position.z), "time", 3.0f, "oncomplete", "setPosAndSpeed", "oncompletetarget", gameObject));
        }
        drawerInOut = !drawerInOut;
    }
}
