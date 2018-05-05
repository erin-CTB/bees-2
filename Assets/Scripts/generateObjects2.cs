using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class generateObjects2 : MonoBehaviour
{
	public Text beeCaught;
	//TouchPhase touchPhase = TouchPhase.Ended;
	Vector3 touchPosWorld;

	void Start()
	{
		

	}
	// Update is called once per frame
	void Update()
	{
		
		if (Input.GetMouseButtonDown (0)) {
			//gameObject temp[] = Input.touches;
			Ray ray = Camera.main.ScreenPointToRay (Input.GetTouch(0).position);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit, 100f,LayerMask.GetMask("BEES"))) 
			{
				if (hit.transform.tag == "bee") {
					//Debug.Log ("");
					beeCaught.text = "Bee tapped! ";
					GameObject tempy = hit.collider.gameObject;
					Destroy (tempy);
				}
			}
		}
		 /*
		if (Input.touchCount > 0 && Input.GetTouch(0).phase == touchPhase) {
			//We transform the touch position into word space from screen space and store it.
			touchPosWorld = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);

			Vector2 touchPosWorld2D = new Vector2(touchPosWorld.x, touchPosWorld.y);

			//We now raycast with this information. If we have hit something we can process it.
			RaycastHit2D hitInformation = Physics2D.Raycast(touchPosWorld2D, Camera.main.transform.forward);

			if (hitInformation.collider != null) {
				//We should have hit something with a 2D Physics collider!
				GameObject touchedObject = hitInformation.transform.gameObject;
				//touchedObject should be the object someone touched.
				beeCaught.text = "Touched " + touchedObject.transform.name;
			}
		}
		*/

	}


}