using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class controller : MonoBehaviour {
	public GameObject sphere;
	public GameObject cube;
	public GameObject cylinder;
	public GameObject flower1;
	public GameObject island;
	private Vector3 diff;
	private Vector3 pos;
	public Button buttball;
	// Use this for initialization
	void Start () {
		cylinder.transform.SetParent (cube.transform);
		diff = new Vector3(.1f,0,0);
		pos = new Vector3 (0, 0, 0);
		//cylinder.transform.localPosition= (new Vector3 (0, 0, 0));
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                StartCoroutine(ScaleMe(hit.transform));
                Debug.Log("You selected the " + hit.transform.name); // ensure you picked right object
            }
        }

	}
    IEnumerator ScaleMe(Transform objTr)
    {
        objTr.localScale *= 1.2f;
        yield return new WaitForSeconds(0.1f);
        objTr.localScale /= 1.2f;
    }

	public void go(){
		//
		iTween.MoveTo(sphere,cube.transform.position,2);
	}

	public void setter(){
		//
		GameObject parental =  cylinder.transform.parent.gameObject;
		parental.transform.SetParent (sphere.transform);
		parental.transform.localPosition= new Vector3 (0, 0, 0);
	}
	void CreateBall(Vector3 atPosition, GameObject dingdong)
	{
		//GameObject ballGO = Instantiate (dingdong, atPosition, Quaternion.identity);

	} 
	public void createThings(){
		//
		Vector3 flowerPos= pos+ new Vector3(0,0.1f,0);
		CreateBall(pos, island);
		flowerPos = flowerPos - new Vector3 (.1f, 0, -.05f);
		for (int i = 0; i < 6/3; i++) {
			for (int p=0; p<3; p++){
				CreateBall(flowerPos, flower1);;
				flowerPos = flowerPos + diff;
			}
			flowerPos = flowerPos-new Vector3(.3f, 0, .2f);

		}

		buttball.gameObject.SetActive (true);
	}  

    public void scaleMe(){
        iTween.PunchScale(flower1, iTween.Hash("position", new Vector3(3, 3, 3), "time", 3.0f, gameObject));
        Debug.Log("shakey"); 
    }
}
