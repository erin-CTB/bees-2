using UnityEngine;
using System.Collections;
public class generateObjects : MonoBehaviour
{
	public Terrain terrain;
	public int numberOfObjects; // number of objects to place
	private int currentObjects; // number of placed objects
	public GameObject objectToPlace; // GameObject to place
	private int terrainWidth; // terrain size (x)
	private int terrainLength; // terrain size (z)
	private int terrainPosX; // terrain position x
	private int terrainPosZ; // terrain position z
	public GameObject[] gems;
	public int num2; //array num

	void Start()
	{
		// terrain size x
		terrainWidth = (int)terrain.terrainData.size.x;
		// terrain size z
		terrainLength = (int)terrain.terrainData.size.z;
		// terrain x position
		terrainPosX = (int)terrain.transform.position.x;
		// terrain z position
		terrainPosZ = (int)terrain.transform.position.z;
		placement (numberOfObjects);

	}
	// Update is called once per frame
	void Update()
	{
		

	}

	public void placement(int boop){
		// generate objects
		while(currentObjects <= boop)
		{
			// generate random x position
			int posx = Random.Range(terrainPosX-terrainWidth, terrainPosX + terrainWidth);
			// generate random z position
			int posz = Random.Range(terrainPosZ+terrainLength, terrainPosZ + terrainLength);
			// get the terrain height at the random position
			//float posy = Terrain.activeTerrain.SampleHeight(new Vector3(posx, 20, posz));
			// create new gameObject on random position
			GameObject newObject = (GameObject)Instantiate(objectToPlace, new Vector3(posx, 20, posz), Quaternion.identity );
			newObject.transform.RotateAround (newObject.transform.position, new Vector3 (0, 1, 0), Random.Range (0, 360));
			newObject.tag = "bee";
			Debug.Log("bee created");
			currentObjects += 1;
		}
		Debug.Log("All bees created!");
		/*if(currentObjects == numberOfObjects)
		{
			
			lineUp ();
		}*/
	
	}

	void lineUp()
	{
		num2 = 12;
		gems = GameObject.FindGameObjectsWithTag ("bee");
		for (int i = 0; i < num2; i++) {
			//gems [i] = GameObject.FindGameObjectWithTag ("gem");
			Debug.Log (gems[i].name);
		}
	}

	public static void testFunction(int temp){
		Debug.Log (temp + " bees pleeeze");
	}
}