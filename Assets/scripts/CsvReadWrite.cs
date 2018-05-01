using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System;

public class CsvReadWrite : MonoBehaviour {

	private List<string[]> rowData = new List<string[]>();
	public string[] holder;
	private int line = 0;
	public string currentPath;
    private string tempName;
	//public GameObject global;


	// Use this for initialization
	void Start () {
		inity ();
        currentPath = Application.dataPath + "/CSV/" + "Saved_data.csv";
        tempName = GlobalControl.Instance.timeName;
		//Save();
		//rowDataTemp = new string[3];
	}

	void inity(){
		// Creating First row of titles manually..
		string[] rowDataTemp = new string[14];
        rowDataTemp[0] = "Start Time";//"Child ID";
        rowDataTemp[1] = "End Time";//"Block";
        rowDataTemp[2] = "Child ID";//"Sampling Trial 1";
        rowDataTemp[3] = "Assessor ID";//"Sampling Trial 2";
        rowDataTemp[4] = "School ID";//"Classification Guess";
        rowDataTemp[5] = "Card Set";//"Category Boundary";
		rowDataTemp [6] = "Condition";
        rowDataTemp[7] = "Boundary";//"Card Set";
        rowDataTemp[8] = "Sampling Trial #";//"Date";
        rowDataTemp[9] = "Sampling Trial 1";//"Assessor ID";
        rowDataTemp[10] = "Sampling Trial 2";//"School ID";
        rowDataTemp[11] = "Boundary Choice";
        rowDataTemp[12] = "Pre-test # of selections";
        rowDataTemp[13] = "Pre-test Accuracy";
		rowData.Add(rowDataTemp);
	}

	public void conCat(List<string[]> zara){
		//
		rowData.AddRange(zara);
		Debug.Log ("concatted");
	}
	public void Save(){



		// You can add up the values in as many cells as you want.
		//for(int i = line; i < line+1; i++){
			
			rowData.Add(holder);
		//}

		string[][] output = new string[rowData.Count][];

		for(int i = 0; i < output.Length; i++){
			output[i] = rowData[i];
		}

		int     length         = output.GetLength(0);
		string     delimiter     = ",";

		StringBuilder sb = new StringBuilder();

		for (int index = 0; index < length; index++)
			sb.AppendLine(string.Join(delimiter, output[index]));


		string filePath = getPath();

		StreamWriter outStream = System.IO.File.CreateText(filePath);
		outStream.WriteLine(sb);
		outStream.Close();
	}

	// Following method is used to retrive the relative path as device platform
	public string getPath(){
        String tempPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop).ToString();
        Debug.Log("PRINTED PATH" + tempPath);


              
		#if UNITY_STANDALONE_OSX
        currentPath =  tempPath+"/CSV/"+tempName+".csv";
		GlobalControl.Instance.path = currentPath;
        return currentPath;
        #elif UNITY_EDITOR
        currentPath =  tempPath+"/CSV/"+tempName+".csv";
        GlobalControl.Instance.path = currentPath;
        return currentPath;
        #elif UNITY_STANDALONE_WIN
        currentPath =  tempPath+"/CSV/"+tempName+".csv";
        GlobalControl.Instance.path = currentPath;
        return currentPath;
		#elif UNITY_ANDROID
		currentPath = Application.persistentDataPath+"Saved_data.csv";
		GlobalControl.Instance.path = currentPath;
		return Application.persistentDataPath+"Saved_data.csv";

		#elif UNITY_IPHONE
		currentPath = Application.persistentDataPath+"/"+"Saved_data.csv";
		GlobalControl.Instance.path = currentPath;
		return Application.persistentDataPath+"/"+"Saved_data.csv";
		#else
		currentPath = Application.dataPath +"/"+"Saved_data.csv";
		GlobalControl.Instance.path = currentPath;
		return Application.dataPath +"/"+"Saved_data.csv";
		#endif
		}
		}
