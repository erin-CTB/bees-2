using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
//using System.Net.Security.Cryptography.X509Certificates;

public class GlobalControl : MonoBehaviour 
{
	public static GlobalControl Instance;
	public string userIDNumber="";
    public string assessorIDNumber="";
    public string schoolIDNumber="";
	public int block;   //current question in block
	public int round=0;  //active or passive, how many have you gone through?
	public bool passive=false;
	public bool active=false;
    public bool tutorial;
	public int activeFirst;
	public List<string[]> rowData = new List<string[]>();
	private int counter=0;
	public int firstScene=0;
	public int cardSetUsed;
	public string path;
    public string timeName;
    public string endTime;
    public string startTime;
    public int currentGame; //0 is active, 1 is passive

	//[SerializePrivateVariables]
	//private string BASE_URL="https://docs.google.com/forms/d/e/1FAIpQLScyOwSviuHBOwGwlYV_sSD7Tsx-M1NzUbTXwUgu6qArhaRhPg/formResponse";


	void Start(){
        activeFirst = UnityEngine.Random.Range (0, 2);
        //cardSetUsed=UnityEngine.Random.Range(0, 2);
        cardSetUsed = 3;
        Debug.Log("active first:" + activeFirst);
        Debug.Log("card first: " + cardSetUsed);
        startTime = DateTime.Now.ToString("MM-dd-yyyy_HH:mm:ss");
        timeName = DateTime.Now.ToString("MM-dd-yyyy_HH-mm-ss");
        //activeFirst = 0;

        //0 is active
        //1 is passive
		this.tag = "global";
		//path = Application.dataPath +"/CSV/"+"Saved_data.csv";

	}

	void Awake ()   
	{
		
		//Debug.Log ("random scene: " + activeFirst);
		if (Instance == null)
		{
			DontDestroyOnLoad(gameObject);
			Instance = this;
		}
		else if (Instance != this)
		{
			Destroy (gameObject);
		}
	}

    public void setEndTime(){
        endTime = DateTime.Now.ToString("MM-dd-yyyy_HH:mm:ss");
        Debug.Log("end time: " + endTime);
        int lastEightRows = rowData.Count - 11;
        for (int i = lastEightRows; i < rowData.Count;i++){
            rowData[i][1]=endTime;
            Debug.Log(rowData[i][2]);
        }
    }
	public void addRowData(string[] row){
		//
		rowData.Add(row);
		Debug.Log ("PRINT: "+rowData[counter][8]);
		counter++;
	}
	public void saveData(GameObject controller){
		//
		controller.GetComponent<CsvReadWrite> ().conCat (rowData);
		controller.GetComponent<CsvReadWrite> ().Save ();
	}

	public void SendEmail ()
	{
		string email = "erin@cognitivetoybox.com";
		string subject = MyEscapeURL("My Subject");
		string body = MyEscapeURL("Body of email.");
		Application.OpenURL("mailto:" + email + "?subject=" + subject + "&body=" + body);

		/*
		MailMessage mail = new MailMessage ();
		mail.From = new MailAddress ("erin@cognitivetoybox.com");
		mail.To.Add ("erin@cognitivetoybox.com");
		mail.Subject = "Subject";
		mail.Body = "body";

		Attachment attachment = null;
		attachment = new Attachment (path);
		mail.Attachments.Add (attachment);

		SmtpClient smtpServer= new SmtpClient ("smtp.gmail.com");
		smtpServer.Port = 587;
		smtpServer.Credentials = new System.Net.NetworkCredential ("erin@cognitivetoybox.com", "Me2D22010") as ICredentialsByHost;
		smtpServer.EnableSsl = true;
		ServicePointManager.ServerCertificateValidationCallback = delegate(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certificate, System.Security.Cryptography.X509Certificates.X509Chain chain, SslPolicyErrors sslPolicyErrors) {
			return true;
		};
		smtpServer.Send (mail);
		Debug.Log ("EMAIL SENT");
		*/




	}

	/*

	IEnumerator Post(string name, string email, string phone) {
		WWWForm form = new WWWForm();
		form.AddField("entry.967696707", "userID");
		form.AddField("entry.1595584798", "block");
		form.AddField("entry.1018439600", "st1");
		form.AddField("entry.1823237675", "st2");
		form.AddField("entry.981311843", "classificationGuess");
		form.AddField("entry.1039817331", "classificationBoundary");
		form.AddField("entry.2036938431", "condition");
		form.AddField("entry.568142891", "cardSet");
		form.AddField("entry.404600014", "date");
		Debug.Log ("fields added");
		byte[] rawData = form.data;
		Debug.Log ("base_url: ");
		WWW www = new WWW(BASE_URL, rawData);
		Debug.Log ("base_url: "+BASE_URL);
		yield return www;
	}
	public void Send() {
		string Name = "erin";
		string Email = "erin@cognitivetoybox.com";
		string Phone = "poop";
		StartCoroutine(Post(Name, Email, Phone));
	}
	*/

	string MyEscapeURL (string url)
	{
		return WWW.EscapeURL(url).Replace("+","%20");
	}


}
